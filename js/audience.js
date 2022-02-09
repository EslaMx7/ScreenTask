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

document.getElementById("start").addEventListener("click", () => {
  var video = document.querySelector("video");
  video.play();
});
