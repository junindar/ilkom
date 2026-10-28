
window.docCamera = {
  _stream: null,

  async start(videoId) {
    const video = document.getElementById(videoId);
    if (!video) throw new Error("Video element not found.");
    this._stream = await navigator.mediaDevices.getUserMedia({
      video: { width: { ideal: 1920 }, height: { ideal: 1080 } },
      audio: false
    });
    video.srcObject = this._stream;
    await video.play();
  },


  capture(videoId) {
    const video = document.getElementById(videoId);
    if (!video || !video.videoWidth) throw new Error("The camera is not ready yet.");
    const canvas = document.createElement("canvas");
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    canvas.getContext("2d").drawImage(video, 0, 0);
    return canvas.toDataURL("image/jpeg", 0.9).split(",")[1];
  },

  stop() {
    if (this._stream) {
      this._stream.getTracks().forEach(t => t.stop());
      this._stream = null;
    }
  }
};
