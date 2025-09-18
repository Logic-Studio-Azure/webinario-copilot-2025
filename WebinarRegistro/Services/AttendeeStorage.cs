using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using WebinarRegistro.Models;

namespace WebinarRegistro.Services;

public class AttendeeStorage
{
    private readonly IJSRuntime _js;
    private const string Key = "attendees";
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = false
    };

    public event Action? OnChanged;

    public AttendeeStorage(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<List<Attendee>> GetAllAsync()
    {
        var json = await _js.InvokeAsync<string?>("storage.get", Key);
        if (string.IsNullOrWhiteSpace(json)) return new();
        try
        {
            var list = JsonSerializer.Deserialize<List<Attendee>>(json, _jsonOptions);
            return list ?? new();
        }
        catch
        {
            return new();
        }
    }

    public async Task AddAsync(Attendee attendee)
    {
        var list = await GetAllAsync();
        list.Add(attendee);
        var json = JsonSerializer.Serialize(list, _jsonOptions);
        await _js.InvokeVoidAsync("storage.set", Key, json);
        OnChanged?.Invoke();
    }

    public async Task ClearAsync()
    {
        await _js.InvokeVoidAsync("storage.remove", Key);
        OnChanged?.Invoke();
    }

    public async Task<string> ExportCsvAsync()
    {
        var list = await GetAllAsync();
        var sb = new StringBuilder();
        sb.AppendLine("Id,Nombre,Empresa,Telefono,Correo,FechaRegistroUTC");
        foreach (var a in list)
        {
            static string Esc(string v) => '"' + v.Replace('"', '\u2033') + '"';
            sb.AppendLine(string.Join(',', new[]
            {
                a.Id.ToString(),
                Esc(a.Nombre),
                Esc(a.Empresa),
                Esc(a.Telefono),
                Esc(a.Correo),
                a.FechaRegistro.ToString("o")
            }));
        }
        return sb.ToString();
    }
}
