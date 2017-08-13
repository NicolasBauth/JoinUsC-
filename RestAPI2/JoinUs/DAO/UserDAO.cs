using DTOModels.UserDTOs;
using JoinUs.AppToastCenter;
using JoinUs.Model;
using JoinUs.Model.UserDTOs;
using JoinUs.StaticServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public static class UserDAO
    {
        public static async Task<AuthenticatedUser> AuthenticateUser(string requestedLogin, string requestedPassword)
        {

            HttpClient client = HttpClientSingleton.Client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("Username",requestedLogin),
                new KeyValuePair<string, string>("Password", requestedPassword),
                new KeyValuePair<string,string>("grant_type","password")
            });
            formContent.Headers.Clear();
            formContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await client.PostAsync("token", formContent);

            if (!response.IsSuccessStatusCode)
            {
                ToastCenter.InformativeNotify("Echec de la connexion", "La connexion a échoué. Code d'erreur:" + response.StatusCode);
                return null;
            }
            AuthenticatedUser currentUser = new AuthenticatedUser();
            var result = await response.Content.ReadAsStringAsync();
            var parsedResult = JsonConvert.DeserializeObject<TokenPackage>(result);
            currentUser.AccessToken = parsedResult.Access_Token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentUser.AccessToken);
            var profileResponse = await client.GetAsync("api/UserProfiles/UserProfileByUsername?username=" + parsedResult.UserName);
            var stringResult = await profileResponse.Content.ReadAsStringAsync();
            var parsedProfileResult = JsonConvert.DeserializeObject<UserProfileDTO>(stringResult);
            currentUser.BirthDate = parsedProfileResult.BirthDate;
            currentUser.FirstName = parsedProfileResult.FirstName;
            currentUser.FollowersCount = 0;
            currentUser.Followings = new List<User>();
            currentUser.Interests = CategoryService.parseCategoryNameListToCategoryList(parsedProfileResult.Interests);
            currentUser.LastName = parsedProfileResult.LastName;
            currentUser.UserName = parsedResult.UserName;
            currentUser.ProfileImagePath = "Assets/userIcon.jpg";
            return currentUser;
        }

        public static async Task<bool> RegisterUser(RegisterFormDTO form)
        {
            HttpClient client = HttpClientSingleton.Client;
            var formContent = JsonConvert.SerializeObject(form);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/Account/Register", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                ToastCenter.InformativeNotify("Succès de l'enregistrement!", "L'utilisateur a été créé avec succès!");
                return true;
            }
            return false;
        }
        public static async Task<User> LoadUserProfileAsync(string username)
        {
            HttpClient client = HttpClientSingleton.Client;
            var profileResponse = await client.GetAsync("api/UserProfiles/UserProfileByUsername?username=" + username);
            var stringResult = await profileResponse.Content.ReadAsStringAsync();
            var parsedProfileResult = JsonConvert.DeserializeObject<UserProfileDTO>(stringResult);
            User profile = new Model.User();
            profile.Birthdate = parsedProfileResult.BirthDate;
            profile.FirstName = parsedProfileResult.FirstName;
            profile.LastName = parsedProfileResult.LastName;
            profile.UserName = username;
            profile.ProfileImagePath = "Assets/userIcon.jpg";
            profile.Interests = CategoryService.parseCategoryNameListToCategoryList(parsedProfileResult.Interests);
            return profile;
        }

        public static async Task<bool> IsUserParticipating(string username, long eventId)
        {
            HttpClient client = HttpClientSingleton.Client;
            EventParticipationForm form = new EventParticipationForm();
            form.Username = username;
            form.EventId = eventId;
            var formContent = JsonConvert.SerializeObject(form);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/Events/IsUserParticipatingToEvent", httpContent);
            var stringResult = await httpResponse.Content.ReadAsStringAsync();
            var parsedProfileResult = JsonConvert.DeserializeObject<bool>(stringResult);
            return parsedProfileResult;
        }
        public static async Task<bool> ParticipateToEvent(string username, long eventId)
        {
            HttpClient client = HttpClientSingleton.Client;
            EventParticipationForm form = new EventParticipationForm();
            form.Username = username;
            form.EventId = eventId;
            var formContent = JsonConvert.SerializeObject(form);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/Events/ParticipateToEvent", httpContent);
            if(!httpResponse.IsSuccessStatusCode)
            {
                ToastCenter.InformativeNotify("Impossible de participer", "Une erreur est survenue. Vérifiez votre connexion internet, et tentez de relancer l'application");
                return false;
            }
            else
            {
                ToastCenter.InformativeNotify("Participation réussie", "Vous avec bien été inscrit à l'évènement.");
                return true;
            }
        }

        public static async Task<bool> CancelParticipationToEvent(string username, long eventId)
        {
            HttpClient client = HttpClientSingleton.Client;
            EventParticipationForm form = new EventParticipationForm();
            form.Username = username;
            form.EventId = eventId;
            var formContent = JsonConvert.SerializeObject(form);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/Events/CancelParticipationToEvent", httpContent);
            if (!httpResponse.IsSuccessStatusCode)
            {
                ToastCenter.InformativeNotify("Impossible d'annuler", "Une erreur est survenue. Vérifiez votre connexion internet, et tentez de relancer l'application");
                return false;
            }
            else
            {
                ToastCenter.InformativeNotify("Annulation réussie", "Vous ne participez plus à l'évènement.");
                return true;
            }
        }
    }
}
