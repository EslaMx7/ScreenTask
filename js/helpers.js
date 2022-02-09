const { desktopCapturer, screen, shell } = require("electron");

var screens = [];



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

 // useSources: useSources,
  getSources: getSources,
};
