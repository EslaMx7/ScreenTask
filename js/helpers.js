const { desktopCapturer, screen, shell } = require("electron");

var screens = [];

async function useSources(sources, cb) {
  for (const source of sources) {
    if (source.name === "Entire Screen") {
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
        handleError(e);
      }
      return;
    }
  }
}

function getSources() {
  return desktopCapturer.getSources({ types: ["window", "screen"] });
}

function getScreens() {
  console.log("Gathering screens...");
  const thumbSize = determineScreenShotSize();
  let options = { types: ["screen"], thumbnailSize: thumbSize };

  desktopCapturer.getSources(options, (error, sources) => {
    if (error) return console.log(error);
    screens = sources;
  });
}

function takeScreenshot(cb) {
  getScreens();
  screens.forEach((source) => {
    if (source.name === "Entire screen" || source.name === "Screen 1") {
      cb(source.thumbnail.toPNG());
    }
  });
}

function determineScreenShotSize() {
  const screenSize = screen.getPrimaryDisplay().workAreaSize;
  const maxDimension = Math.max(screenSize.width, screenSize.height);
  return {
    width: maxDimension * window.devicePixelRatio,
    height: maxDimension * window.devicePixelRatio,
  };
}

function handleError(e) {
  console.log(e);
}

module.exports = {
  takeScreenshot: takeScreenshot,
  getScreens: getScreens,

  useSources: useSources,
  getSources: getSources,
};
