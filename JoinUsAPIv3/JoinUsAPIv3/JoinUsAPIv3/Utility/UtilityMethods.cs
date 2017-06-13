
using DTOModels.EventDTOs;
using JoinUsAPIv3.Models;
using System;
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
        public static List<string> ParseTagsListToTagsNamesList(ICollection<Tag> tagsList)
        {
            List<string> parsedList = new List<string>();
            foreach (var tag in tagsList)
            {
                parsedList.Add(tag.Name);
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

        public static List<EventShortDTO> EventListToEventShortDTOList(ICollection<Event> eventList)
        {
            List<EventShortDTO> parsedList = new List<EventShortDTO>();
            foreach(var eventToParse in eventList)
            {
                parsedList.Add(EventToEventShort(eventToParse));
            }
            return parsedList;
        }
        public static EventShortDTO EventToEventShort(Event eventToParse)
        {
            EventShortDTO parsedEvent = new EventShortDTO {Id = eventToParse.Id, Address = eventToParse.Address, Date = eventToParse.Date, Title = eventToParse.Title };
            return parsedEvent;
        }

        public static double degreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static double distanceInKmBetweenEarthCoordinates(double lat1,double lon1,double lat2,double lon2)
        {
            int earthRadiusKm = 6371;

            double dLat = degreesToRadians(lat2 - lat1);
            double dLon = degreesToRadians(lon2 - lon1);

            lat1 = degreesToRadians(lat1);
            lat2 = degreesToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return earthRadiusKm * c;
        }
    }
}