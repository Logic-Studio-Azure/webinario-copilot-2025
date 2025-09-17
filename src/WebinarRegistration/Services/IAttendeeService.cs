using WebinarRegistration.Models;

namespace WebinarRegistration.Services;

public interface IAttendeeService
{
    Task<IReadOnlyList<Attendee>> GetAllAsync();
    Task AddAsync(Attendee attendee);
    Task<int> CountAsync();
    Task ExportCsvAsync();
    Task ExportJsonAsync();
    Task ClearAllAsync();
}