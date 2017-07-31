using DTOModels.UserDTOs;
using JoinUs.AppToastCenter;
using JoinUs.Model;
using JoinUs.Model.CategoryDTOs;
using JoinUs.Model.UserDTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public static class CategoryDAO
    {
        //Ids:Etude = 1, Jeux video = 2, langues = 3, Sport = 4,Dîner = 5, Soirée = 6, Culture = 7, Musique = 8, Jeux de societe = 9
        public static async Task<List<Category>> GetAllCategories()
        {
            HttpClient client = HttpClientSingleton.Client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("api/Categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            List<CategoryDTO> parsedResult = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);
            List<Category> allCategoriesList = new List<Category>();
            foreach (var categoryDTO in parsedResult)
            {
                Category representedCategory = new Category(categoryDTO.Title);
                allCategoriesList.Add(representedCategory);
            }
            return allCategoriesList;
        }
        public static async Task<AuthenticatedUser> UpdateUserInterests(AuthenticatedUser currentUser, List<Category> newInterests)
        {
            HttpClient client = HttpClientSingleton.Client;
            UpdateUserInterestForm form = new UpdateUserInterestForm();
            form.Username = currentUser.UserName;
            form.NewInterestsIds = new List<int>();
            foreach (var category in newInterests)
            {
                form.NewInterestsIds.Add(category.DbId);
            }
            var formContent = JsonConvert.SerializeObject(form);
            var httpContent = new StringContent(formContent, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("api/UserProfiles/UpdateUserInterests", httpContent);
            if (!httpResponse.IsSuccessStatusCode)
            {
                ToastCenter.InformativeNotify("Erreur d'édition", "Erreur d'édition des intérêts. Veuillez réessayer. Si le problème persiste, vérifiez votre connexion internet ou relancez l'application.");
            }
            var stringResult = await httpResponse.Content.ReadAsStringAsync();
            var parsedProfileResult = JsonConvert.DeserializeObject<UserProfileDTO>(stringResult);
            List<Category> updatedInterests = new List<Category>();
            foreach (var interest in parsedProfileResult.Interests)
            {
                Category currentCategory = new Category(interest);
                updatedInterests.Add(currentCategory);
            }
            currentUser.Interests = updatedInterests;
            return currentUser;
        }
    }
}
