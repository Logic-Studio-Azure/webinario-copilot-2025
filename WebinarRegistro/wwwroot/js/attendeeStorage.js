window.attendeeStorage = (function(){
  const KEY = 'webinar_attendees_v1';

  function load(){
    try {
      const raw = localStorage.getItem(KEY);
      if(!raw) return [];
      return JSON.parse(raw);
    } catch(e){
      console.error('Error loading attendees', e);
      return [];
    }
  }

  function save(list){
    try {
      localStorage.setItem(KEY, JSON.stringify(list));
      return true;
    } catch(e){
      console.error('Error saving attendees', e);
      return false;
    }
  }

  function add(attendee){
    const list = load();
    list.push(attendee);
    save(list);
    return attendee;
  }

  function clear(){
    localStorage.removeItem(KEY);
  }

  function exportCsv(){
    const list = load();
    if(!list.length) return '';
    const headers = ['Id','FechaUTC','Nombre','Empresa','Telefono','Correo'];
    const lines = [headers.join(',')];
    list.forEach(a => {
      const row = [a.id, a.createdUtc, esc(a.nombre), esc(a.empresa), esc(a.telefono), esc(a.correo)];
      lines.push(row.join(','));
    });
    return lines.join('\n');
  }

  function esc(v){
    if(v == null) return '';
    const needsQuote = /[",\n]/.test(v);
    let s = String(v).replace(/"/g,'""');
    return needsQuote ? `"${s}"` : s;
  }

  return { load, add, clear, exportCsv };
})();
