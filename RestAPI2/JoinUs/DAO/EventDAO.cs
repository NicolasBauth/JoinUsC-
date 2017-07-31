using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
    }
}
