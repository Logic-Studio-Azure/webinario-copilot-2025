using System.Text.Json;
using Microsoft.JSInterop;
using WebinarRegistro.Models;

namespace WebinarRegistro.Services;

public class AttendeeService
{
    private readonly IJSRuntime _js;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public AttendeeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<IReadOnlyList<Attendee>> GetAllAsync()
    {
        var jsList = await _js.InvokeAsync<object?>("attendeeStorage.load");
        if(jsList is null) return Array.Empty<Attendee>();
        // serialize roundtrip to map JS objects into .NET list
        var json = JsonSerializer.Serialize(jsList, _jsonOptions);
        var list = JsonSerializer.Deserialize<List<Attendee>>(json, _jsonOptions) ?? new();
        return list.OrderByDescending(a => a.CreatedUtc).ToList();
    }

    public async Task<Attendee> AddAsync(Attendee attendee)
    {
        attendee.Id = Guid.NewGuid();
        attendee.CreatedUtc = DateTime.UtcNow;
        // pass plain object with camelCase expected by JS
        var jsObj = new {
            id = attendee.Id,
            createdUtc = attendee.CreatedUtc.ToString("o"),
            nombre = attendee.Nombre,
            empresa = attendee.Empresa,
            telefono = attendee.Telefono,
            correo = attendee.Correo
        };
        await _js.InvokeVoidAsync("attendeeStorage.add", jsObj);
        return attendee;
    }

    public async Task<string> ExportCsvAsync()
    {
        var csv = await _js.InvokeAsync<string>("attendeeStorage.exportCsv");
        return csv;
    }
}
