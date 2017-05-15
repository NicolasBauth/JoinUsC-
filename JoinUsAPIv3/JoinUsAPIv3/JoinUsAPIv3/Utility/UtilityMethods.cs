using JoinUsAPIv3.Models;
using System.Collections.Generic;

namespace JoinUsAPIv3.Utility
{
    public static class UtilityMethods
    {
        public static List<string> ParseCategoryListToCategoryNamesList(ICollection<Category> categoryList)
        {
            List<string> parsedList = new List<string>();
            foreach (var category in categoryList)
            {
                parsedList.Add(category.Name);
            }
            return parsedList;
        }
    }
}