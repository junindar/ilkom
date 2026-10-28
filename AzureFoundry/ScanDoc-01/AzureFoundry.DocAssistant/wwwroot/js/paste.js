
window.docPaste = {
  _handler: null,

  register(dotnetRef) {
    this.unregister();
    this._handler = (e) => {
      if (!(e.ctrlKey || e.metaKey) || e.key.toLowerCase() !== "v") return;
      const t = e.target;
      const sedangMengetik = t && (t.tagName === "INPUT" || t.tagName === "TEXTAREA" || t.isContentEditable);
      if (sedangMengetik) return;
      e.preventDefault();
      dotnetRef.invokeMethodAsync("OnCtrlV");
    };
    document.addEventListener("keydown", this._handler);
  },

  unregister() {
    if (this._handler) {
      document.removeEventListener("keydown", this._handler);
      this._handler = null;
    }
  }
};
