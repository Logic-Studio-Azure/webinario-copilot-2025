window.storage = {
  get: function (key) {
    return localStorage.getItem(key);
  },
  set: function (key, value) {
    localStorage.setItem(key, value);
  },
  remove: function (key) {
    localStorage.removeItem(key);
  },
  downloadCsv: function (base64, filename) {
    const blob = new Blob([atob(base64)], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    a.remove();
    setTimeout(() => URL.revokeObjectURL(url), 1000);
  }
};
