var peers = [];
var socket = io("http://192.168.1.3:7071");
var stream;

socket.on("answer-to-caller", function (msg) {
  peers.find((x) => x.id === msg.id).peer.signal(msg.data);
});

socket.on("start-broadcast-signal", function (data) {
  if (socket.id !== data.id) {
    peers.push({ id: data.id, peer: createNewPeer(data.id) });
  }
});

socket.on("start-broadcast-stream", function (sources) {
  helpers.useSources(sources, getVideoStream);
});

function createNewPeer(audienceId) {
  var peer = new SimplePeer({ initiator: true });
  peer.on("connect", () => {
    // wait for 'connect' event before using the data channel
    console.log("speaker: connected");
    peer.send(`hey I'm the speaker, ${audienceId} how is it going?`);
  });

  peer.on("signal", (data) => {
    // when peer1 has signaling data, give it to peer2 somehow
    console.log("speaker signaling", data);
    socket.emit("signaling", { audienceId, data });
  });

  peer.on("data", (data) => {
    // got a data channel message
    console.log("data");
    console.log("got a message from peer2: " + data);
    if (stream !== undefined) {
      peer.addStream(stream);
    }
  });

  peer.on("close", () => {
    console.log("disconnected");
    peers.splice(
      peers.findIndex((x) => x.peer === peer),
      1
    );
  });

  peer.on("error", (err) => {
    console.log("error", err);
  });

  return peer;
}

function getVideoStream(vStream) {
  stream = vStream;
  console.log("vStream: ", vStream);
  console.log("got stream");
  const video = document.querySelector("video");
  video.srcObject = vStream;
  video.onloadedmetadata = (e) => video.play();

  console.log("addedStream");
  peers.forEach((peer) => {
    peer.addStream(vStream);
  });
  console.log("addedStreamEnd");
}
