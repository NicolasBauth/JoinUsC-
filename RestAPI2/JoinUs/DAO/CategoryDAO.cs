using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public static class CategoryDAO
    {
        public static IEnumerable<Category> GetAllCategories()
        {
            return new List<Category>
            {
                new Category("Jeux de société","Assets/BoardGame.jpg"),
                new Category("Dîner","Assets/Dinner.png"),
                new Category("Jeux vidéo","Assets/VideoGame.jpg"),
                new Category("Soirée","Assets/Party.jpg"),
                new Category("Culture","Assets/Culture.jpg")
            };
        }
        public static IEnumerable<Category> GetInterestsOfUserByUserName(string userName)
        {
            return new List<Category>
            {
                new Category("Jeux de société","Assets/BoardGame.jpg"),
                new Category("Dîner","Assets/Dinner.png"),
                new Category("Jeux vidéo","Assets/VideoGame.jpg"),
                new Category("Soirée","Assets/Party.jpg"),
                new Category("Culture","Assets/Culture.jpg")
            };
        }
    }
}
