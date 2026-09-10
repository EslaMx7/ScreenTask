(() => {
  // Elements
  const canvas = document.getElementById('screenCanvas');
  const ctx = canvas.getContext('2d', { alpha: false });
  const overlay = document.getElementById('overlayMessage');
  const overlayTitle = document.getElementById('overlayTitle');
  const overlayDesc = document.getElementById('overlayDesc');
  const fpsDisplay = document.getElementById('fpsValue');
  const resolutionDisplay = document.getElementById('resValue');
  const latencyDisplay = document.getElementById('latencyValue');
  const btnFullscreen = document.getElementById('btnFullscreen');
  const btnAudio = document.getElementById('btnAudio');
  const audioSlider = document.getElementById('audioSlider');
  const btnResetZoom = document.getElementById('btnResetZoom');
  const zoomHud = document.getElementById('zoomHud');
  const pinModal = document.getElementById('pinModal');
  const pinInput = document.getElementById('pinInput');
  const btnSubmitPin = document.getElementById('btnSubmitPin');
  const themeToggle = document.getElementById('themeToggle');

  // State
  let ws = null;
  let audioCtx = null;
  let audioGain = null;
  let isAudioMuted = true;
  let audioSampleRate = 48000;
  let audioChannels = 2;
  let frameCount = 0;
  let lastFpsTime = performance.now();
  let currentFps = 0;
  let isConnected = false;
  let pin = localStorage.getItem('st_pin') || '';

  // Zoom and Pan State
  let scale = 1;
  let panX = 0;
  let panY = 0;
  let isDragging = false;
  let startX = 0;
  let startY = 0;
  let initialPinchDistance = null;
  let initialScale = 1;

  // Theme Initializer
  const savedTheme = localStorage.getItem('st-theme') || (window.matchMedia('(prefers-color-scheme: light)').matches ? 'light' : 'dark');
  document.documentElement.setAttribute('data-theme', savedTheme);

  themeToggle?.addEventListener('click', () => {
    const current = document.documentElement.getAttribute('data-theme') || 'dark';
    const next = current === 'dark' ? 'light' : 'dark';
    document.documentElement.setAttribute('data-theme', next);
    localStorage.setItem('st-theme', next);
  });

  // Audio Context Setup
  function initAudio() {
    if (!audioCtx) {
      const AudioContext = window.AudioContext || window.webkitAudioContext;
      audioCtx = new AudioContext({ sampleRate: audioSampleRate });
      audioGain = audioCtx.createGain();
      audioGain.gain.value = isAudioMuted ? 0 : parseFloat(audioSlider.value);
      audioGain.connect(audioCtx.destination);
    }
    if (audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
  }

  btnAudio?.addEventListener('click', () => {
    initAudio();
    isAudioMuted = !isAudioMuted;
    if (audioGain) {
      audioGain.gain.value = isAudioMuted ? 0 : parseFloat(audioSlider.value);
    }
    updateAudioBtn();
  });

  audioSlider?.addEventListener('input', (e) => {
    initAudio();
    if (isAudioMuted) {
      isAudioMuted = false;
      updateAudioBtn();
    }
    if (audioGain) {
      audioGain.gain.value = parseFloat(e.target.value);
    }
  });

  function updateAudioBtn() {
    if (isAudioMuted) {
      btnAudio.innerHTML = `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="1" y1="1" x2="23" y2="23"></line><path d="M9 9v3a3 3 0 0 0 5.12 2.12M15 9.34V4a3 3 0 0 0-5.94-.6"></path><path d="M17 16.95A7 7 0 0 1 5 12v-2m14 0v2a7 7 0 0 1-.11 1.23"></path><line x1="12" y1="19" x2="12" y2="23"></line><line x1="8" y1="23" x2="16" y2="23"></line></svg>`;
      btnAudio.title = "Unmute Audio";
    } else {
      btnAudio.innerHTML = `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polygon points="11 5 6 9 2 9 2 15 6 15 11 19 11 5"></polygon><path d="M19.07 4.93a10 10 0 0 1 0 14.14M15.54 8.46a5 5 0 0 1 0 7.07"></path></svg>`;
      btnAudio.title = "Mute Audio";
    }
  }

  // Connect WebSocket
  function connect() {
    const protocol = location.protocol === 'https:' ? 'wss:' : 'ws:';
    const wsUrl = `${protocol}//${location.host}/stream/ws${pin ? `?pin=${encodeURIComponent(pin)}` : ''}`;

    ws = new WebSocket(wsUrl);
    ws.binaryType = 'arraybuffer';

    ws.onopen = () => {
      isConnected = true;
      overlay.classList.add('hidden');
      pinModal.classList.add('hidden');
    };

    ws.onmessage = async (event) => {
      if (!(event.data instanceof ArrayBuffer)) return;

      const data = new Uint8Array(event.data);
      if (data.length === 0) return;

      const packetType = data[0];

      // 0x01 = Video Frame (JPEG)
      if (packetType === 0x01) {
        const frameBytes = data.subarray(1);
        const blob = new Blob([frameBytes], { type: 'image/jpeg' });
        try {
          const bitmap = await createImageBitmap(blob);
          if (canvas.width !== bitmap.width || canvas.height !== bitmap.height) {
            canvas.width = bitmap.width;
            canvas.height = bitmap.height;
            resolutionDisplay.textContent = `${bitmap.width}x${bitmap.height}`;
          }
          ctx.drawImage(bitmap, 0, 0);
          bitmap.close();

          frameCount++;
          const now = performance.now();
          if (now - lastFpsTime >= 1000) {
            currentFps = frameCount;
            frameCount = 0;
            lastFpsTime = now;
            fpsDisplay.textContent = `${currentFps} FPS`;
          }
        } catch (err) {
          // Ignore transient decode errors
        }
      }
      // 0x02 = Audio PCM Chunk
      else if (packetType === 0x02) {
        if (!isAudioMuted && audioCtx && audioCtx.state === 'running') {
          playPcmChunk(data.subarray(1));
        }
      }
      // 0x03 = Audio Format
      else if (packetType === 0x03 && data.length >= 9) {
        const view = new DataView(event.data);
        audioSampleRate = view.getInt32(1, true);
        audioChannels = view.getInt16(5, true);
      }
    };

    ws.onclose = (e) => {
      isConnected = false;
      if (e.code === 1008 || e.code === 4001) {
        // PIN required or invalid
        pinModal.classList.remove('hidden');
        pinInput.focus();
      } else {
        overlay.classList.remove('hidden');
        overlayTitle.textContent = "Host Disconnected";
        overlayDesc.textContent = "ScreenTask server was stopped or connection was lost. Reconnecting...";
        setTimeout(connect, 2000);
      }
    };

    ws.onerror = () => {
      ws.close();
    };
  }

  // Audio Playback
  function playPcmChunk(pcmBytes) {
    try {
      // Typically 32-bit Float or 16-bit Int PCM from WASAPI
      const numSamples = pcmBytes.length / 4; // Assuming 32-bit float stereo or 16-bit
      const float32Data = new Float32Array(pcmBytes.buffer, pcmBytes.byteOffset, numSamples);

      const buffer = audioCtx.createBuffer(1, float32Data.length, audioSampleRate);
      buffer.copyToChannel(float32Data, 0);

      const source = audioCtx.createBufferSource();
      source.buffer = buffer;
      source.connect(audioGain);
      source.start();
    } catch (e) { }
  }

  // Fullscreen
  btnFullscreen?.addEventListener('click', () => {
    if (!document.fullscreenElement) {
      document.documentElement.requestFullscreen().catch(() => {});
    } else {
      document.exitFullscreen().catch(() => {});
    }
  });

  // Touch Zoom & Pan Handlers
  function applyTransform() {
    canvas.style.transform = `translate(${panX}px, ${panY}px) scale(${scale})`;
    if (scale > 1) {
      zoomHud.style.display = 'block';
      zoomHud.textContent = `${Math.round(scale * 100)}%`;
      btnResetZoom.style.display = 'inline-flex';
    } else {
      zoomHud.style.display = 'none';
      btnResetZoom.style.display = 'none';
    }
  }

  btnResetZoom?.addEventListener('click', () => {
    scale = 1;
    panX = 0;
    panY = 0;
    applyTransform();
  });

  // Mouse wheel zoom
  canvas.addEventListener('wheel', (e) => {
    e.preventDefault();
    const zoomDelta = e.deltaY < 0 ? 1.15 : 0.85;
    scale = Math.min(Math.max(1, scale * zoomDelta), 5);
    if (scale === 1) {
      panX = 0;
      panY = 0;
    }
    applyTransform();
  }, { passive: false });

  // Mouse Drag to Pan
  canvas.addEventListener('mousedown', (e) => {
    if (scale > 1) {
      isDragging = true;
      startX = e.clientX - panX;
      startY = e.clientY - panY;
    }
  });

  window.addEventListener('mousemove', (e) => {
    if (isDragging) {
      panX = e.clientX - startX;
      panY = e.clientY - startY;
      applyTransform();
    }
  });

  window.addEventListener('mouseup', () => {
    isDragging = false;
  });

  // Touch Pinch-to-Zoom
  canvas.addEventListener('touchstart', (e) => {
    if (e.touches.length === 2) {
      initialPinchDistance = getDistance(e.touches[0], e.touches[1]);
      initialScale = scale;
    } else if (e.touches.length === 1 && scale > 1) {
      isDragging = true;
      startX = e.touches[0].clientX - panX;
      startY = e.touches[0].clientY - panY;
    }
  });

  canvas.addEventListener('touchmove', (e) => {
    if (e.touches.length === 2 && initialPinchDistance) {
      e.preventDefault();
      const currentDistance = getDistance(e.touches[0], e.touches[1]);
      scale = Math.min(Math.max(1, initialScale * (currentDistance / initialPinchDistance)), 5);
      if (scale === 1) {
        panX = 0;
        panY = 0;
      }
      applyTransform();
    } else if (e.touches.length === 1 && isDragging) {
      panX = e.touches[0].clientX - startX;
      panY = e.touches[0].clientY - startY;
      applyTransform();
    }
  }, { passive: false });

  canvas.addEventListener('touchend', () => {
    initialPinchDistance = null;
    isDragging = false;
  });

  function getDistance(t1, t2) {
    const dx = t1.clientX - t2.clientX;
    const dy = t1.clientY - t2.clientY;
    return Math.sqrt(dx * dx + dy * dy);
  }

  // PIN modal submission
  btnSubmitPin?.addEventListener('click', () => {
    pin = pinInput.value.trim();
    if (pin) {
      localStorage.setItem('st_pin', pin);
      connect();
    }
  });

  pinInput?.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
      btnSubmitPin.click();
    }
  });

  // Start connection
  connect();
})();
