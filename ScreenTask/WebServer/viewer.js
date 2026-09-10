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
  const btnAudioDiag = document.getElementById('btnAudioDiag');
  const audioDiagModal = document.getElementById('audioDiagModal');
  const btnCloseAudioDiag = document.getElementById('btnCloseAudioDiag');
  const btnPlayChime = document.getElementById('btnPlayChime');
  const btnToggleAudioModal = document.getElementById('btnToggleAudioModal');
  const audioStreamStatus = document.getElementById('audioStreamStatus');
  const audioFormatInfo = document.getElementById('audioFormatInfo');
  const audioLevelPct = document.getElementById('audioLevelPct');
  const audioMeterBar = document.getElementById('audioMeterBar');
  const btnResetZoom = document.getElementById('btnResetZoom');
  const zoomHud = document.getElementById('zoomHud');
  const pinModal = document.getElementById('pinModal');
  const pinInput = document.getElementById('pinInput');
  const pinError = document.getElementById('pinError');
  const btnSubmitPin = document.getElementById('btnSubmitPin');
  const audioBanner = document.getElementById('audioBanner');
  const themeToggle = document.getElementById('themeToggle');

  // State
  let ws = null;
  let audioCtx = null;
  let audioGain = null;
  let isAudioMuted = true;
  let audioSampleRate = 48000;
  let audioChannels = 2;
  let audioBitsPerSample = 16;
  let nextAudioPlayTime = 0;
  let lastAudioPacketTime = 0;
  let meterDecayTimeout = null;
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
      const AudioContextClass = window.AudioContext || window.webkitAudioContext;
      if (AudioContextClass) {
        audioCtx = new AudioContextClass();
        audioGain = audioCtx.createGain();
        const currentVol = audioSlider ? parseFloat(audioSlider.value) : 0.8;
        audioGain.gain.value = isAudioMuted ? 0 : currentVol;
        audioGain.connect(audioCtx.destination);
      }
    }
    if (audioCtx && audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
    // iOS Safari Audio Unlock
    try {
      if (audioCtx) {
        const unlockBuf = audioCtx.createBuffer(1, 1, 22050);
        const unlockSrc = audioCtx.createBufferSource();
        unlockSrc.buffer = unlockBuf;
        unlockSrc.connect(audioCtx.destination);
        unlockSrc.start(0);
      }
    } catch (e) {}
  }

  // One-time gesture listener to unlock Web Audio on iOS/Android
  const unlockAudioGesture = () => {
    if (audioCtx && audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
    document.removeEventListener('touchstart', unlockAudioGesture);
    document.removeEventListener('touchend', unlockAudioGesture);
    document.removeEventListener('click', unlockAudioGesture);
  };
  document.addEventListener('touchstart', unlockAudioGesture, { passive: true });
  document.addEventListener('touchend', unlockAudioGesture, { passive: true });
  document.addEventListener('click', unlockAudioGesture, { passive: true });

  function playTestChime() {
    initAudio();
    if (!audioCtx) return;
    if (audioCtx.state === 'suspended') {
      audioCtx.resume();
    }
    try {
      const now = audioCtx.currentTime;

      // Note 1: E5 (659.25 Hz)
      const osc1 = audioCtx.createOscillator();
      const gain1 = audioCtx.createGain();
      osc1.type = 'sine';
      osc1.frequency.setValueAtTime(659.25, now);
      gain1.gain.setValueAtTime(0.35, now);
      gain1.gain.exponentialRampToValueAtTime(0.0001, now + 0.35);
      osc1.connect(gain1);
      gain1.connect(audioCtx.destination);
      osc1.start(now);
      osc1.stop(now + 0.35);

      // Note 2: A5 (880 Hz)
      const osc2 = audioCtx.createOscillator();
      const gain2 = audioCtx.createGain();
      osc2.type = 'sine';
      osc2.frequency.setValueAtTime(880, now + 0.2);
      gain2.gain.setValueAtTime(0.35, now + 0.2);
      gain2.gain.exponentialRampToValueAtTime(0.0001, now + 0.65);
      osc2.connect(gain2);
      gain2.connect(audioCtx.destination);
      osc2.start(now + 0.2);
      osc2.stop(now + 0.65);
    } catch (e) {}
  }

  function updateAudioBtn() {
    const currentVol = audioSlider ? parseFloat(audioSlider.value) : 0.8;
    if (audioGain) {
      audioGain.gain.value = isAudioMuted ? 0 : currentVol;
    }

    if (isAudioMuted) {
      if (btnAudio) {
        btnAudio.innerHTML = `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="1" y1="1" x2="23" y2="23"></line><path d="M9 9v3a3 3 0 0 0 5.12 2.12M15 9.34V4a3 3 0 0 0-5.94-.6"></path><path d="M17 16.95A7 7 0 0 1 5 12v-2m14 0v2a7 7 0 0 1-.11 1.23"></path><line x1="12" y1="19" x2="12" y2="23"></line><line x1="8" y1="23" x2="16" y2="23"></line></svg>`;
        btnAudio.title = "Unmute Audio";
      }
      if (btnToggleAudioModal) {
        btnToggleAudioModal.textContent = "🔊 Unmute Audio";
        btnToggleAudioModal.classList.remove('btn-primary');
      }
    } else {
      if (btnAudio) {
        btnAudio.innerHTML = `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polygon points="11 5 6 9 2 9 2 15 6 15 11 19 11 5"></polygon><path d="M19.07 4.93a10 10 0 0 1 0 14.14M15.54 8.46a5 5 0 0 1 0 7.07"></path></svg>`;
        btnAudio.title = "Mute Audio";
      }
      if (btnToggleAudioModal) {
        btnToggleAudioModal.textContent = "🔇 Mute Audio";
        btnToggleAudioModal.classList.add('btn-primary');
      }
    }
  }

  function updateAudioInfoUi() {
    if (audioFormatInfo) {
      const ch = audioChannels === 1 ? 'Mono' : audioChannels === 2 ? 'Stereo' : `${audioChannels} ch`;
      const fmtName = audioBitsPerSample === 32 ? '32-bit Float' : '16-bit PCM';
      audioFormatInfo.textContent = `${audioSampleRate} Hz, ${fmtName} (${ch})`;
    }
  }

  function updateAudioMeter(peak) {
    const pct = Math.min(100, Math.round(peak * 100));
    if (audioMeterBar) audioMeterBar.style.width = `${pct}%`;
    if (audioLevelPct) audioLevelPct.textContent = `${pct}%`;
    if (audioStreamStatus) {
      audioStreamStatus.textContent = "🟢 Streaming Audio";
      audioStreamStatus.style.color = "var(--success)";
    }

    clearTimeout(meterDecayTimeout);
    meterDecayTimeout = setTimeout(() => {
      if (audioMeterBar) audioMeterBar.style.width = '0%';
      if (audioLevelPct) audioLevelPct.textContent = '0%';
      if (audioStreamStatus && (performance.now() - lastAudioPacketTime > 1500)) {
        audioStreamStatus.textContent = "⚪ Idle / Silent";
        audioStreamStatus.style.color = "var(--text-muted)";
      }
    }, 250);
  }

  audioBanner?.addEventListener('click', () => {
    initAudio();
    isAudioMuted = false;
    updateAudioBtn();
    if (audioBanner) audioBanner.style.display = 'none';
  });

  btnAudio?.addEventListener('click', () => {
    initAudio();
    isAudioMuted = !isAudioMuted;
    updateAudioBtn();
    if (!isAudioMuted && audioBanner) audioBanner.style.display = 'none';
  });

  audioSlider?.addEventListener('input', (e) => {
    initAudio();
    if (isAudioMuted) {
      isAudioMuted = false;
      if (audioBanner) audioBanner.style.display = 'none';
    }
    updateAudioBtn();
  });

  btnAudioDiag?.addEventListener('click', () => {
    initAudio();
    audioDiagModal?.classList.remove('hidden');
    updateAudioInfoUi();
    updateAudioBtn();
  });

  btnCloseAudioDiag?.addEventListener('click', () => {
    audioDiagModal?.classList.add('hidden');
  });

  btnPlayChime?.addEventListener('click', () => {
    playTestChime();
  });

  btnToggleAudioModal?.addEventListener('click', () => {
    initAudio();
    isAudioMuted = !isAudioMuted;
    updateAudioBtn();
    if (!isAudioMuted && audioBanner) audioBanner.style.display = 'none';
  });

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
        if (isAudioMuted) {
          if (audioBanner && audioBanner.style.display !== 'flex') {
            audioBanner.style.display = 'flex';
          }
        }
        playPcmChunk(data.subarray(1));
      }
      // 0x03 = Audio Format
      else if (packetType === 0x03 && data.length >= 9) {
        const view = new DataView(event.data);
        audioSampleRate = view.getInt32(1, true);
        audioChannels = view.getInt16(5, true);
        audioBitsPerSample = view.getInt16(7, true);
        updateAudioInfoUi();
      }
    };

    ws.onclose = (e) => {
      isConnected = false;
      if (e.code === 1008 || e.code === 4001) {
        showPinModal();
      } else {
        overlay.classList.remove('hidden');
        overlayTitle.textContent = "Host Disconnected";
        overlayDesc.textContent = "ScreenTask server was stopped or connection was lost. Reconnecting...";
        setTimeout(async () => {
          const ok = await checkServerStatus();
          if (ok) connect();
        }, 2000);
      }
    };

    ws.onerror = () => {
      ws.close();
    };
  }

  // Audio Playback
  function playPcmChunk(pcmBytes) {
    try {
      lastAudioPacketTime = performance.now();
      const bytesPerSample = (audioBitsPerSample === 32) ? 4 : 2;
      const sampleCount = Math.floor(pcmBytes.byteLength / bytesPerSample);
      if (sampleCount <= 0) return;

      const float32Data = new Float32Array(sampleCount);
      const view = new DataView(pcmBytes.buffer, pcmBytes.byteOffset, pcmBytes.byteLength);

      let peak = 0;
      if (audioBitsPerSample === 32) {
        for (let i = 0; i < sampleCount; i++) {
          const val = view.getFloat32(i * 4, true);
          float32Data[i] = val;
          const abs = Math.abs(val);
          if (abs > peak) peak = abs;
        }
      } else {
        // 16-bit PCM integer
        for (let i = 0; i < sampleCount; i++) {
          const val = view.getInt16(i * 2, true) / 32768.0;
          float32Data[i] = val;
          const abs = Math.abs(val);
          if (abs > peak) peak = abs;
        }
      }

      // Real-time VU meter update
      updateAudioMeter(peak);

      // If muted or audio context not active, skip playback
      if (!audioCtx || audioCtx.state !== 'running' || isAudioMuted) return;

      const numChannels = (audioChannels >= 1 && audioChannels <= 2) ? audioChannels : 1;
      const numFrames = Math.floor(sampleCount / numChannels);
      if (numFrames <= 0) return;

      const buffer = audioCtx.createBuffer(numChannels, numFrames, audioSampleRate || 48000);
      if (numChannels === 2) {
        const left = buffer.getChannelData(0);
        const right = buffer.getChannelData(1);
        for (let i = 0; i < numFrames; i++) {
          left[i] = float32Data[i * 2];
          right[i] = float32Data[i * 2 + 1];
        }
      } else {
        const mono = buffer.getChannelData(0);
        for (let i = 0; i < numFrames; i++) {
          mono[i] = float32Data[i];
        }
      }

      const source = audioCtx.createBufferSource();
      source.buffer = buffer;
      source.connect(audioGain);

      const currentTime = audioCtx.currentTime;
      if (nextAudioPlayTime < currentTime) {
        nextAudioPlayTime = currentTime + 0.03;
      } else if (nextAudioPlayTime > currentTime + 0.25) {
        nextAudioPlayTime = currentTime + 0.03;
      }

      source.start(nextAudioPlayTime);
      nextAudioPlayTime += buffer.duration;
    } catch (e) {
      console.warn('Audio playback error:', e);
    }
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

  async function submitPin() {
    const enteredPin = pinInput.value.trim();
    if (!enteredPin) return;

    if (pinError) pinError.style.display = 'none';
    btnSubmitPin.disabled = true;
    btnSubmitPin.textContent = 'Verifying...';

    try {
      const res = await fetch('/api/auth', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ pin: enteredPin })
      });
      const data = await res.json();
      if (data.success) {
        pin = enteredPin;
        localStorage.setItem('st_pin', pin);
        pinModal.classList.add('hidden');
        btnSubmitPin.disabled = false;
        btnSubmitPin.textContent = 'Connect to Stream';
        connect();
      } else {
        if (pinError) {
          pinError.textContent = data.message || 'Incorrect PIN code. Please try again.';
          pinError.style.display = 'block';
        }
        pinInput.focus();
        pinInput.select();
        btnSubmitPin.disabled = false;
        btnSubmitPin.textContent = 'Connect to Stream';
      }
    } catch (e) {
      if (pinError) {
        pinError.textContent = 'Connection error. Please try again.';
        pinError.style.display = 'block';
      }
      btnSubmitPin.disabled = false;
      btnSubmitPin.textContent = 'Connect to Stream';
    }
  }

  function showPinModal() {
    if (ws) {
      try { ws.close(); } catch (e) {}
      ws = null;
    }
    overlay.classList.add('hidden');
    pinModal.classList.remove('hidden');
    if (pinError) pinError.style.display = 'none';
    pinInput.focus();
  }

  async function checkServerStatus() {
    try {
      const res = await fetch('/api/status');
      if (!res.ok) return true;
      const status = await res.json();
      if (status.audioEnabled && isAudioMuted) {
        if (audioBanner) audioBanner.style.display = 'flex';
      }
      if (status.requirePin) {
        if (!pin) {
          showPinModal();
          return false;
        }
        const authRes = await fetch('/api/auth', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ pin })
        });
        const authData = await authRes.json();
        if (!authData.success) {
          localStorage.removeItem('st_pin');
          pin = '';
          showPinModal();
          return false;
        }
      }
    } catch (e) {}
    return true;
  }

  btnSubmitPin?.addEventListener('click', submitPin);
  pinInput?.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') submitPin();
  });

  (async () => {
    const ok = await checkServerStatus();
    if (ok) connect();
  })();
})();
