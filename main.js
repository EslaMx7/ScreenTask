// Modules to control application life and create native browser window
const { app, BrowserWindow, ipcMain, desktopCapturer } = require("electron");
const os = require('os');
const path = require("path");
const express = require("express");
const expressApp = express();
const http = require("http");
const helpers = require("./js/helpers");
var fs = require("fs");
const { Server } = require("socket.io");
const e = require("express");

var GLOBAL_STATE = {
  ips: [],
  ip: null,
  port: 7171,
  state: "stopped",
  sources: [],
  sourceId: null,
};

function createWindow() {
  // Create the browser window.
  const mainWindow = new BrowserWindow({
    width: 1024,
    height: 768,
    webPreferences: {
      preload: path.join(__dirname, "preload.js"),
      nodeIntegration: true,
      contextIsolation: false,
    },
    //autoHideMenuBar: true,
  });

  // and load the index.html of the app.
  mainWindow.loadFile("index.html");

  // Open the DevTools.
  mainWindow.webContents.openDevTools();
}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.whenReady().then(() => {
  createWindow();

  app.on("activate", function () {
    // On macOS it's common to re-create a window in the app when the
    // dock icon is clicked and there are no other windows open.
    if (BrowserWindow.getAllWindows().length === 0) createWindow();
  });

  GLOBAL_STATE.ips = getIPs();
  GLOBAL_STATE.sources = getSources();
  if (GLOBAL_STATE.sources.length > 0)
    GLOBAL_STATE.sourceId = GLOBAL_STATE.sources[0].id;

});

// Quit when all windows are closed, except on macOS. There, it's common
// for applications and their menu bar to stay active until the user quits
// explicitly with Cmd + Q.
app.on("window-all-closed", function () {
  if (process.platform !== "darwin") app.quit();
});

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and require them here.


function getIPs() {
  var ips = [];
  var interfaces = os.networkInterfaces();
  var keys = Object.keys(interfaces);
  keys.forEach(key => {
    var interface = interfaces[key].filter(x => x.family === 'IPv4');
    if (interface.length > 0) {

      ips.push({ address: interface[0].address, name: key })
    }
  });
  return ips;
}

function getSources() {
  var sources = [];
  desktopCapturer.getSources({ types: ["window", "screen"] }).then(async (sourcesList) => {
    sourcesList.forEach(async (source) => {
      sources.push(source);
    });
  });
  return sources;
}

server = http.createServer(expressApp);
const io = new Server(server);
expressApp.use(express.static("."));
expressApp.get("/", (req, res) => {
  res.sendFile(__dirname + "/index.html");
});


ipcMain.handle("start-server", (ev, arg) => {
  // state = arg;
  GLOBAL_STATE.ip = arg.selectedIP;
  GLOBAL_STATE.port = arg.selectedPort;
  GLOBAL_STATE.sourceId = arg.selectedSourceId;
  if (GLOBAL_STATE.state === "stopped") {
    server.listen(GLOBAL_STATE.port, GLOBAL_STATE.ip, () => {
      console.log(`Server running at http://${GLOBAL_STATE.ip}:${GLOBAL_STATE.port}/`);
      GLOBAL_STATE.state = "started";

      ev.sender.send("navigate-speaker-iframe", {
        url: `http://${GLOBAL_STATE.ip}:${GLOBAL_STATE.port}/speaker.html`,
      });
      // Send for speaker to start
      ev.sender.send("start-speaker-video", { selectedSource: GLOBAL_STATE.sources.find(x => x.id === GLOBAL_STATE.sourceId) });
    });
  } else {
    console.log("Server already started");
    server.close();
    console.log(`Server stopped...`);
    GLOBAL_STATE.state = "stopped";
  }

  io.on("connection", (socket) => {
    console.log("a user connected");

    // send for audience
    socket.broadcast.emit("start-broadcast-signal", { id: socket.id });



    socket.on("signaling", (data) => {
      // console.log("signaling: ", data);
      io.to(data.audienceId).emit("broadcast-signal", {
        id: socket.id,
        data: data.data,
      });
    });

    socket.on("answer-to-caller", (data) => {
      // console.log("answer-to-caller: ", data);
      socket.broadcast.emit("answer-to-caller", {
        id: socket.id,
        data: data,
      });
    });
  });

});

ipcMain.handle("stop-server", (event, res) => {
  server.close();
  console.log(`Server stopped...`);
});

ipcMain.handle("DESKTOP_CAPTURER_GET_SOURCES", function (ev, _) {
  helpers.getSources().then((opts) => {
    io.emit("start-broadcast-stream", opts);
    //ev.sender.send("DESKTOP_CAPTURER_GET_SOURCES_RESULT", opts);
  });
});

ipcMain.handle("change-ip", (event, res) => {
  console.log(`change-ip: ${res}`);
  GLOBAL_STATE.ip = res.address;
});

ipcMain.handle("change-source", (ev, res) => {
  console.log(`change-source: ${res}`);
  GLOBAL_STATE.sourceId = res.id;
  ev.sender.send("start-speaker-video", { selectedSource: GLOBAL_STATE.sources.find(x => x.id === GLOBAL_STATE.sourceId) });

});

ipcMain.handle("get-ips-list", (ev, res) => {
  ev.sender.send("ips-list", { ips: GLOBAL_STATE.ips });
});

ipcMain.handle("get-port-number", (ev, res) => {
  ev.sender.send("port-number", { port: GLOBAL_STATE.port });
});

ipcMain.handle("get-sources-list", (ev, res) => {
  console.log(GLOBAL_STATE.sources);
  if (GLOBAL_STATE.sources.length == 0) return;
  var sources = GLOBAL_STATE.sources.map(s => ({ name: s.name, id: s.id }));
  ev.sender.send("sources-list", { sources: sources });
});