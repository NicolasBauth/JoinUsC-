using JoinUs.Model;
using System.Collections.Generic;

namespace JoinUs.StaticServices
{
    public static class CategoryService
    {
        //Ids:Etude = 1, Jeux video = 2, langues = 3, Sport = 4,Dîner = 5, Soirée = 6, Culture = 7, Musique = 8, Jeux de societe = 9
        public static long getIdOfCategoryName(string categoryName)
        {
            long idToReturn;
            switch (categoryName)
            {
                case "Etude":
                    idToReturn = 1;
                    break;
                case "Jeux vidéo":
                    idToReturn = 2;
                    break;
                case "Langues":
                    idToReturn = 3;
                    break;
                case "Sport":
                    idToReturn = 4;
                    break;
                case "Dîner":
                    idToReturn = 5;
                    break;
                case "Soirée":
                    idToReturn = 6;
                    break;
                case "Culture":
                    idToReturn = 7;
                    break;
                case "Musique":
                    idToReturn = 8;
                    break;
                case "Jeux de société":
                    idToReturn = 9;
                    break;
                default:
                    idToReturn = 10;
                    break;
            }
            return idToReturn;
        }
        public static List<Category> parseCategoryNameListToCategoryList(List<string> categoriesNames)
        {
            List<Category> parsedCategoryList = new List<Category>();
            foreach (string categoryName in categoriesNames)
            {
                Category parsedCategory = new Category(categoryName);
                parsedCategoryList.Add(parsedCategory);
            }
            return parsedCategoryList;
        }
    }
}
