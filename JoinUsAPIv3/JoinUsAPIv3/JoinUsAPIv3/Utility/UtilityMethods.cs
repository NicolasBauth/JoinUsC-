
using DTOModels.EventDTOs;
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
        public static EventPresentationDTO EventToEventPresentation(Event eventToParse)
        {
            EventPresentationDTO parsedEvent = new EventPresentationDTO
            {
                Address = eventToParse.Address,
                CategoriesNames = ParseCategoryListToCategoryNamesList(eventToParse.Categories),
                CreatorFirstName = eventToParse.Creator.FirstName,
                CreatorLastName = eventToParse.Creator.LastName,
                Date = eventToParse.Date,
                Description = eventToParse.Description,
                Id = eventToParse.Id,
                Title = eventToParse.Title,
                UrlFacebook = eventToParse.UrlFacebook,
                UserCount = eventToParse.Participants.Count
                
            };
            return parsedEvent;
        }
        public static List<string> ParseEventListToEventNamesList(ICollection<Event> eventList)
        {
            List<string> parsedList = new List<string>();
            foreach(var e in eventList)
            {
                parsedList.Add(e.Title);
            }
            return parsedList;
        }

        public static List<string> ParseUserListtoUserNamesList(ICollection<User> userList)
        {
            List<string> parsedList = new List<string>();
            foreach(var user in userList)
            {
                parsedList.Add(user.FirstName + " " + user.LastName);
            }
            return parsedList;
        }
    }
}