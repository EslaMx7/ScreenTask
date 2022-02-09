var peer2;
var socket = io("http://192.168.1.3:7071");
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
    var video = AddVideo();

    if ("srcObject" in video) {
      video.srcObject = stream;
    } else {
      video.src = window.URL.createObjectURL(stream); // for older browsers
    }

    setTimeout(() => {
      startScreenSharePreview(video);
    }, 3000);
  });

  peer.on("error", (err) => {
    console.log("error", err);
  });

  return peer;
}

/* Menu click Handlers */
/* Menu Button */
function startScreenSharePreview(video) {
  video.play();

  document.getElementsByClassName("loading")[0].style.display = "none";
  document.getElementsByClassName("shared-screen")[0].style.display = "flex";
}

function StartStream(screenId) {}

function AddScreen() {
  let div = document.createElement("div");
  div.className = "shared-screen col d-flex align-items-center";
  screenShareCount = document.querySelectorAll(".shared-screen").length;
  div.id = `screen-share-${screenShareCount}`;
  div.style.display = "flex";

  div.insertAdjacentHTML(
    "beforeend",
    `<div class="col-12 d-flex">
      <input
        type="text"
        class="form-control"
        placeholder="https://screentask.com:7070"
        aria-label="Server link"
        aria-describedby="basic-addon2"
      />
      <div class="input-group-append">
        <span class="input-group-text" id="basic-addon2"
          >/audience.html</spans
        >
      </div>
      <div class="input-group-append">
        <button class="btn btn-outline-primary" click="StartStream(${screenShareCount})" type="button">Start</button>
      </div>
    </div>`
  );
  document.querySelector("#screens").appendChild(div);
}

function AddVideo() {
  let div = document.createElement("div");
  div.className = "shared-screen col d-flex align-items-center";
  screenShareCount = document.querySelectorAll(".shared-screen").length;
  div.id = `screen-share-${screenShareCount}`;
  div.style.display = "flex";

  let video = document.createElement("video");
  video.className = "w-100";
  video.id = `screen-share-video-${screenShareCount}`;
  video.autoplay = true;
  video.muted = true;
  div.appendChild(video);

  document.querySelector("#screens").appendChild(div);

  return video;
}
document
  .getElementById("start")
  .addEventListener("click", startScreenSharePreview);

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

document.getElementById("add-screen").addEventListener("click", AddScreen);

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
