window.storageInterop = {
  get: function (key) {
    return window.localStorage.getItem(key);
  },
  set: function (key, value) {
    window.localStorage.setItem(key, value);
  }
};

window.downloadInterop = {
  downloadBytes: function (fileName, contentType, bytes) {
    // bytes es un Uint8Array que llega desde .NET
    const blob = new Blob([bytes], { type: contentType });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
  }
};