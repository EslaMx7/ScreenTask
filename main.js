// Modules to control application life and create native browser window
const { app, BrowserWindow, ipcMain, desktopCapturer } = require("electron");
const path = require("path");
const express = require("express");
const expressApp = express();

const http = require("http");
const helpers = require("./js/helpers");
var fs = require("fs");
const { Server } = require("socket.io");

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
});

// Quit when all windows are closed, except on macOS. There, it's common
// for applications and their menu bar to stay active until the user quits
// explicitly with Cmd + Q.
app.on("window-all-closed", function () {
  if (process.platform !== "darwin") app.quit();
});

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and require them here.
server = http.createServer(expressApp);
const io = new Server(server);
expressApp.use(express.static("."));
expressApp.get("/", (req, res) => {
  res.sendFile(__dirname + "/index.html");
});

state = {
  config: {
    ips: [{ address: "192.168.1.3", selected: true }],
    port: 7071,
  },
};

ipcMain.handle("start-server", (event, arg) => {
  // state = arg;

  console.log(state.config.ips.filter((x) => x.selected));
  var selectedIp = state.config.ips.filter((x) => x.selected)[0].address;
  server.listen(state.config.port, selectedIp, () => {
    console.log(`Server running at http://${selectedIp}:${state.config.port}/`);
  });

  io.on("connection", (socket) => {
    console.log("a user connected");

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
  console.log("DESKTOP_CAPTURER_GET_SOURCES");
  helpers.getSources().then(
    (opts) => {
      io.emit("start-broadcast-stream", opts);
      //ev.sender.send("DESKTOP_CAPTURER_GET_SOURCES_RESULT", opts);
    },
    (err) => {
      console.log("Error: ", err);
    }
  );
});
