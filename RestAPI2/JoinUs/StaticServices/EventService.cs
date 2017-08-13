using JoinUs.Model;
using JoinUs.Model.EventDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace JoinUs.StaticServices
{
    public static class EventService
    {
        public static async Task<string> ReverseGeocodePoint(Geopoint eventPoint)
        {
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(eventPoint);
            string eventAddress;
            if (!(result.Status == MapLocationFinderStatus.Success))
            {
                eventAddress = "Pas d'adresse";
            }
            else
            {
                try
                {
                    MapAddress address = result.Locations[0].Address;
                    eventAddress = address.StreetNumber;
                    eventAddress += ", ";
                    eventAddress += address.Street;
                    eventAddress += ", ";
                    eventAddress += address.PostCode;
                    eventAddress += ", ";
                    eventAddress += address.Town;
                }
                catch (Exception)
                {
                    eventAddress = "Pas d'adresse";
                }
            }
            return eventAddress;
        }
        public static Event ParseEventPresentationDTOToEvent(EventPresentationDTO presentation)
        {
            Event parsedEvent = new Event();
            parsedEvent.Address = presentation.Address;
            parsedEvent.Categories = new List<Category>();
            foreach(var categoryName in presentation.CategoriesNames)
            {
                Category categoryToAdd = new Category(categoryName);
                parsedEvent.Categories.Add(categoryToAdd);
            }
            parsedEvent.CreatorFirstName = presentation.CreatorFirstName;
            parsedEvent.CreatorLastName = presentation.CreatorLastName;
            parsedEvent.CreatorUsername = presentation.CreatorUsername;
            parsedEvent.Date = presentation.Date;
            parsedEvent.DbId = presentation.Id;
            parsedEvent.Latitude = presentation.Latitude;
            parsedEvent.Longitude = presentation.Longitude;
            parsedEvent.Description = presentation.Description;
            parsedEvent.ParticipantsCount = presentation.UserCount;
            parsedEvent.Title = presentation.Title;
            parsedEvent.UrlFacebook = presentation.UrlFacebook;
            return parsedEvent;
        }
    }
}
