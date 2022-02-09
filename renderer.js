// This file is required by the index.html file and will
// be executed in the renderer process for that window.
// No Node.js APIs are available in this process because
// `nodeIntegration` is turned off. Use `preload.js` to
// selectively enable features needed in the rendering
// process.

const { ipcRenderer } = require("electron");
const helpers = require("./js/helpers");

const el = (selector) => document.querySelector(selector);
var startServerButton = el("#startServerButton");
var ipSelect = el("#ipSelect");
var screenSelect = el("#screenSelect");
var iframe = el("#speakerIframe");
var portNumber = el("#portNumber");



const desktopCapturer = {
  getSources: (opts) =>
    ipcRenderer.invoke("DESKTOP_CAPTURER_GET_SOURCES", opts),
};

//ipcRenderer.invoke("start-server", {});

desktopCapturer.getSources();

startServerButton.addEventListener("click", () => {
  if(startServerButton.innerHTML === "Start Server") {
  ipcRenderer.invoke("start-server", {
    selectedIP: ipSelect.value,
    selectedSourceId: screenSelect.value,
    selectedPort : portNumber.value,
  });
  startServerButton.innerText = "Stop Server";
} else {
  startServerButton.innerText = "Start Server";
}
});

ipSelect.addEventListener("change", (e) => {
  ipcRenderer.invoke("change-ip", {
    address: e.target.value,
  });
});

screenSelect.addEventListener("change", (e) => {
  ipcRenderer.invoke("change-source", {
    id: e.target.value,
  });
});

ipcRenderer.on("navigate-speaker-iframe", (event, arg) => {
  console.log("renderer navigate-speaker-iframe: ", arg);
  iframe.src = arg.url;
});

ipcRenderer.on("ips-list", (event, arg) => {
  console.log("ips-list",arg);
  var ips = arg.ips;
  var ipSelectOptions = ipSelect.options;
  ipSelectOptions.length = 0;
  ips.forEach((ip) => {
    var option = document.createElement("option");
    option.text = ip.address + "  (" + ip.name + ")";
    option.value = ip.address;
    ipSelect.add(option);
  });
});

ipcRenderer.on("port-number", (event, arg) => {
  console.log("port-number",arg);
  portNumber.value = arg.port;
});

ipcRenderer.on("sources-list", (event, arg) => {
  console.log("sources-list",arg);
  var sources = arg.sources;
  var sourcesSelectOptions = screenSelect.options;
  sourcesSelectOptions.length = 0;
  sources.forEach((source) => {
    var option = document.createElement("option");
    option.text = source.name;
    option.value = source.id;
    screenSelect.add(option);
  });
  screenSelect.dispatchEvent(new Event('change'));
});

ipcRenderer.on("start-speaker-video", (event, arg) => {
  useselectedSource(arg.selectedSource, getVideoStream);
});

function getVideoStream(vStream) {
  console.log("got stream");
  const video = document.querySelector("video");
  video.srcObject = vStream;
  video.onloadedmetadata = (e) => video.play();
  console.log("stream played");
}


async function useselectedSource(source, cb) {

    console.log("source: ", source);
      try {
        const stream = await navigator.mediaDevices.getUserMedia({
          audio: false,
          video: {
            mandatory: {
              chromeMediaSource: "desktop",
              chromeMediaSourceId: source.id,
              minWidth: 1280,
              maxWidth: 1280,
              minHeight: 720,
              maxHeight: 720,
            },
          },
        });
        cb(stream);
        //window.streamX = stream;
      } catch (e) {
        console.log(e);
      }
}


ipcRenderer.invoke("get-ips-list");
ipcRenderer.invoke("get-port-number");
ipcRenderer.invoke("get-sources-list");