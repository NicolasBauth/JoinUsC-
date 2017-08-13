using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using JoinUs.StaticServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace JoinUs.DAO
{
    public static class EventDAO
    {
        public static async Task<bool> CreateEventAsync(Event eventToSend, AuthenticatedUser currentUser)
        {
            EventCreationDTO creationForm = new EventCreationDTO();
            creationForm.Address = eventToSend.Address;
            List<long> categoriesId = new List<long>();
            foreach (var category in eventToSend.Categories)
            {
                categoriesId.Add(category.DbId);
            }
            creationForm.CategoriesId = categoriesId;
            creationForm.CreatorUsername = currentUser.UserName;
            creationForm.Date = eventToSend.Date;
            creationForm.Description = eventToSend.Description;
            creationForm.FacebookUrl = eventToSend.UrlFacebook;
            creationForm.Latitude = eventToSend.Latitude;
            creationForm.Longitude = eventToSend.Longitude;
            List<long> tagsId = new List<long>();
            tagsId.Add(1);
            creationForm.TagsId = tagsId;
            creationForm.Title = eventToSend.Title;
            HttpClient client = HttpClientSingleton.Client;
            var formContent = JsonConvert.SerializeObject(creationForm);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/Events/PostEvent", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public static async Task<List<EventShortDTO>> GetAllEventsCreatedByUser(string username)
        {
            HttpClient client = HttpClientSingleton.Client;
            var response = await client.GetAsync("api/Events/GetEventsCreatedByUser?username=" + username);
            if(!response.IsSuccessStatusCode)
            {
                return new List<EventShortDTO>();
            }
            var result = await response.Content.ReadAsStringAsync();
            List<EventShortDTO> parsedResult = JsonConvert.DeserializeObject<List<EventShortDTO>>(result);
            return parsedResult;
        }

        public static async Task<List<EventShortDTO>> GetAllEventsUserParticipates(string username)
        {
            HttpClient client = HttpClientSingleton.Client;
            var response = await client.GetAsync("api/Events/GetEventsUserParticipates?username=" + username);
            if (!response.IsSuccessStatusCode)
            {
                return new List<EventShortDTO>();
            }
            var result = await response.Content.ReadAsStringAsync();
            List<EventShortDTO> parsedResult = JsonConvert.DeserializeObject<List<EventShortDTO>>(result);
            return parsedResult;
        }

        public static async Task<Event> GetFullEventDescription(long eventId)
        {
            HttpClient client = HttpClientSingleton.Client;
            var response = await client.GetAsync("api/Events/GetEventById?id=" + eventId);
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            EventPresentationDTO parsedResult = JsonConvert.DeserializeObject<EventPresentationDTO>(result);
            Event parsedEvent = EventService.ParseEventPresentationDTOToEvent(parsedResult);
            return parsedEvent;
        }
        public static async Task<bool> DeleteEventAsync(long DbId)
        {
            HttpClient client = HttpClientSingleton.Client;
            var response = await client.DeleteAsync("api/Events/DeleteEvent?id=" + DbId);
            return response.IsSuccessStatusCode;
        }

        public static async Task<List<EventShortDTO>> GetEventsAroundPoint(Geopoint scanCenter, int radius,List<string> categoryNamesFilter)
        {
            HttpClient client = HttpClientSingleton.Client;
            EventSearchDTO formToSend = new EventSearchDTO();
            BasicGeoposition reprensentedPosition = scanCenter.Position;
            formToSend.CenterLatitude = reprensentedPosition.Latitude;
            formToSend.CenterLongitude = reprensentedPosition.Longitude;
            formToSend.Radius = radius;
            var formContent = JsonConvert.SerializeObject(formToSend);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/Events/GetEventsAroundPoint", httpContent);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return new List<EventShortDTO>();
            }
            var result = await httpResponse.Content.ReadAsStringAsync();
            List<EventScanDTO> parsedResult = JsonConvert.DeserializeObject<List<EventScanDTO>>(result);
            List<EventShortDTO> listOfEvents = new List<EventShortDTO>();
            if (!(categoryNamesFilter.Count == 0))
            {
                foreach (var foundEvent in parsedResult)
                {
                    foreach (var categoryName in foundEvent.CategoriesNames)
                    {
                        if (categoryNamesFilter.Contains(categoryName))
                        {
                            EventShortDTO eventToAdd = new EventShortDTO();
                            eventToAdd.Address = foundEvent.Address;
                            eventToAdd.Date = foundEvent.Date;
                            eventToAdd.Id = foundEvent.Id;
                            eventToAdd.Title = foundEvent.Title;
                            listOfEvents.Add(eventToAdd);
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach(var foundEvent in parsedResult)
                {
                    EventShortDTO eventToAdd = new EventShortDTO();
                    eventToAdd.Address = foundEvent.Address;
                    eventToAdd.Date = foundEvent.Date;
                    eventToAdd.Id = foundEvent.Id;
                    eventToAdd.Title = foundEvent.Title;
                    listOfEvents.Add(eventToAdd);
                }
            }
            return listOfEvents;
        }
    }
}
