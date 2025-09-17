using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using WebinarRegistration.Models;

namespace WebinarRegistration.Services;

public class LocalStorageAttendeeService : IAttendeeService
{
    private const string StorageKey = "attendees:v1";
    private readonly IJSRuntime _js;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = false
    };

    public LocalStorageAttendeeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<IReadOnlyList<Attendee>> GetAllAsync()
    {
        var json = await _js.InvokeAsync<string?>("storageInterop.get", StorageKey);
        if (string.IsNullOrWhiteSpace(json)) return Array.Empty<Attendee>();
        try
        {
            var list = JsonSerializer.Deserialize<List<Attendee>>(json, _jsonOptions) ?? new();
            return list.OrderByDescending(a => a.RegisteredAt).ToList();
        }
        catch
        {
            return Array.Empty<Attendee>();
        }
    }

    public async Task AddAsync(Attendee attendee)
    {
        var list = (await GetAllAsync()).ToList();
        list.Add(attendee);
        var json = JsonSerializer.Serialize(list, _jsonOptions);
        await _js.InvokeVoidAsync("storageInterop.set", StorageKey, json);
    }

    public async Task<int> CountAsync() => (await GetAllAsync()).Count;

    public async Task ExportCsvAsync()
    {
        var attendees = await GetAllAsync();
        var sb = new StringBuilder();
        sb.AppendLine("Id,Name,Company,Phone,Email,RegisteredAt");
        foreach (var a in attendees.OrderBy(a => a.RegisteredAt))
        {
            sb.AppendLine($"{a.Id},\"{Escape(a.Name)}\",\"{Escape(a.Company)}\",\"{Escape(a.Phone)}\",{a.Email},{a.RegisteredAt:o}");
        }
        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        await _js.InvokeVoidAsync("downloadInterop.downloadBytes", "attendees.csv", "text/csv", bytes);
    }

    public async Task ExportJsonAsync()
    {
        var attendees = await GetAllAsync();
        var json = JsonSerializer.Serialize(attendees, _jsonOptions);
        var bytes = Encoding.UTF8.GetBytes(json);
        await _js.InvokeVoidAsync("downloadInterop.downloadBytes", "attendees.json", "application/json", bytes);
    }

    public async Task ClearAllAsync()
    {
        // Persistir lista vacía
        await _js.InvokeVoidAsync("storageInterop.set", StorageKey, "[]");
    }

    // Escape double quotes for CSV
    private static string Escape(string input) => input.Replace("\"", "\"\"");
}