using Google.Apis.Calendar.v3.Data;

namespace Sample.GoogleCalendarApi.Services
{
    public interface IGoogleCalendarService
    {
        Task<Event> CreateEvent(Event request, CancellationToken cancellationToken);
    }
}
