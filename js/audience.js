var peer2;
var socket = io();
socket.on("connect", () => {
  console.log(socket.id);
  if (peer2) {
    peer2.destroy();
  }
  peer2 = createNewPeer();
});

socket.on("broadcast-signal", function (msg) {
  if (peer2.destroyed) {
    peer2 = createNewPeer();
  }
  peer2.signal(msg.data);
});

function createNewPeer() {
  var peer = new SimplePeer({ initiator: false });
  peer.on("signal", (data) => {
    socket.emit("answer-to-caller", data);
    console.log(data);
  });

  peer.on("connect", () => {
    // wait for 'connect' event before using the data channel
    console.log("peer2: connected");
    peer.send("hey I'm audience, how you doing?");
  });

  peer.on("data", (data) => {
    // got a data channel message
    console.log("got a message from speaker: " + data);
  });

  peer.on("stream", (stream) => {
    console.log("got a stream from speaker: ");
    // got remote video stream, now let's show it in a video tag
    var video = document.querySelector("video");

    if ("srcObject" in video) {
      video.srcObject = stream;
    } else {
      video.src = window.URL.createObjectURL(stream); // for older browsers
    }
  });

  peer.on("error", (err) => {
    console.log("error", err);
  });

  return peer;
}

/* Menu click Handlers */
/* Menu Button */
document.getElementById("start").addEventListener("click", () => {
  var video = document.querySelector("video");
  video.play();

  document.getElementsByClassName("loading")[0].style.display = "none";
  document.getElementsByClassName("shared-screen")[0].style.display = "block";
});

[
  ...document
    .getElementsByClassName("btn-group-fab")[0]
    .getElementsByClassName("btn"),
].forEach((element) => {
  element.addEventListener("click", (e) => {
    e.stopPropagation();
    document
      .getElementsByClassName("btn-group-fab")[0]
      .classList.toggle("active");
  });
});

document.body.addEventListener("click", () => {
  document
    .getElementsByClassName("btn-group-fab")[0]
    .classList.remove("active");
});

/* Full Screen Button */
document
  .getElementById("full-screen")
  .addEventListener("click", toggleFullscreen);

function toggleFullscreen() {
  var elem = document.documentElement;
  document.fullscreenElement
    ? closeFullscreen.call(this)
    : openFullscreen.call(this);
}

function openFullscreen() {
  var elem = document.documentElement;
  if (elem.requestFullscreen) {
    elem.requestFullscreen();
  } else if (elem.webkitRequestFullscreen) {
    /* Safari */
    elem.webkitRequestFullscreen();
  } else if (elem.msRequestFullscreen) {
    /* IE11 */
    elem.msRequestFullscreen();
  }
}

function closeFullscreen() {
  var elem = document.documentElement;
  if (document.exitFullscreen) {
    document.exitFullscreen();
  } else if (document.webkitExitFullscreen) {
    /* Safari */
    document.webkitExitFullscreen();
  } else if (document.msExitFullscreen) {
    /* IE11 */
    document.msExitFullscreen();
  }
}
