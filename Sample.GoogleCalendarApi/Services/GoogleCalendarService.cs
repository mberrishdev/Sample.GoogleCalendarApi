using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Sample.GoogleCalendarApi.Settings;

namespace Sample.GoogleCalendarApi.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IGoogleCalendarSettings _settings;

        public GoogleCalendarService(IGoogleCalendarSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        ///  Create Google Calendar Event base on out request
        /// </summary>
        /// <param name="request">Google Event model where we write information about out Event</param>
        /// <param name="cancellationToken">Google Event model where we write information about out Event</param>
        /// <returns>Google Event</returns>
        public async Task<Event> CreateEvent(Event request, CancellationToken cancellationToken)
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets()
                {
                    ClientId = _settings.ClientId,
                    ClientSecret = _settings.ClientSecret
                },
                _settings.Scope,
                _settings.User,
                cancellationToken);

            // define services
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _settings.ApplicationName,
            });

            var eventRequest = services.Events.Insert(request, _settings.CalendarId);
            var requestCreate = await eventRequest.ExecuteAsync(cancellationToken);
            return requestCreate;
        }
    }
}
