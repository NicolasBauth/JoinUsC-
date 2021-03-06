﻿
using DTOModels.EventDTOs;
using DTOModels.UserDTOs;
using JoinUsAPIv3.Models;
using JoinUsAPIv3.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace JoinUsAPIv3.Controllers
{
    [Authorize]
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {

        private JoinUsAPIv3Context db = new JoinUsAPIv3Context();
        [HttpGet]
        //this method returns all the events from the database, formatted as EventPresentationDTO.
        //call: api/Events
        public IEnumerable<EventPresentationDTO> GetEvents()
        {
            var events = from e in db.Events.Include(b => b.Creator).Include(b => b.Participants).Include(b => b.Categories).ToList()

                         select new EventPresentationDTO
                         {
                             Address = e.Address,
                             CreatorFirstName = e.Creator.FirstName,
                             CreatorLastName = e.Creator.LastName,
                             Date = e.Date,
                             Description = e.Description,
                             Title = e.Title,
                             UrlFacebook = e.UrlFacebook,
                             Id = e.Id,
                             UserCount = e.Participants.Count,
                             CategoriesNames = UtilityMethods.ParseCategoryListToCategoryNamesList(e.Categories.ToList()),
                             Latitude = e.Latitude,
                             Longitude = e.Longitude
                         };

            return events;

        }
        [Route("GetEventsOfCategory")]
        public IEnumerable<EventShortDTO> GetEventsOfCategory(long categoryId)
        {
            var events = from e in db.Events.Include(b => b.Categories)
                         select new EventShortDTO
                         {
                             Address = e.Address,
                             Date = e.Date,
                             Id = e.Id,
                             Title = e.Title
                         };
            return events;
        }
        //This method returns all the database events in the "EventShortDTO" format.
        //call: api/Events/GetEventsShort
        [Route("GetEventsShort")]
        public IQueryable<EventShortDTO> GetEventsShort()
        {
            var eventsShorts = from e in db.Events
                               where (DateTime.Compare(e.Date, DateTime.Now) >= 0)
                               select new EventShortDTO
                               {
                                   Address = e.Address,
                                   Date = e.Date,
                                   Title = e.Title,
                                   Id = e.Id
                               };
            return eventsShorts;
        }

        //returns a raw event entity corresponding to the id provided as parameter
        //call:api/Events/GetEventById?id=5
        [Route("GetEventById")]
        [ResponseType(typeof(EventPresentationDTO))]
        public async Task<IHttpActionResult> GetEvent(long id)
        {
            //FindAsync not available here because it's a method from the DbSet type, and Include returns a DbQuery.
            //SingleOrDefaultAsync is a DbQuery method. It goes straight to the database
            //and doesn't look first in the context to see if the entity exists.           
            Event @event = await db.Events.Include(b => b.Creator).Include(b => b.Categories).Include(b => b.Participants).SingleOrDefaultAsync(i => i.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            EventPresentationDTO foundEvent = UtilityMethods.EventToEventPresentation(@event);
            User creatorProfile = await db.UserProfiles.Include(b => b.ReferencedApplicationUser).SingleOrDefaultAsync(i => i.Id == @event.CreatorId);
            foundEvent.CreatorUsername = creatorProfile.ReferencedApplicationUser.UserName;
            return Ok(foundEvent);
        }

        [Route("GetEventsCreatedByUser")]
        [ResponseType(typeof(IEnumerable<EventShortDTO>))]
        public async Task<IHttpActionResult> GetEventsCreatedByUser(string username)
        {
            User foundUser = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == username);
            if (foundUser == null)
            {
                return NotFound();
            }
            List<EventShortDTO> createdEvents = UtilityMethods.EventListToEventShortDTOList(foundUser.CreatedEvents);
            return Ok(createdEvents);
        }
        [Route("GetEventsUserParticipates")]
        [ResponseType(typeof(IEnumerable<EventShortDTO>))]
        public async Task<IHttpActionResult> GetEventsUserParticipates(string username)
        {
            
            User foundUser = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == username);
            if (foundUser == null)
            {
                return NotFound();
            }
            List<EventShortDTO> joinedEvents = UtilityMethods.EventListToEventShortDTOList(foundUser.JoinedEvents);
            return Ok(joinedEvents);
        }

        [HttpPost]
        [Route("GetEventsAroundPoint")]
        public IEnumerable<EventScanDTO> GetEventsAroundPoint(EventSearchDTO form)
        {

            var foundEvents = from e in db.Events.Include(b => b.Categories).Include(b => b.Tags).ToList()
                              where ((UtilityMethods.distanceInKmBetweenEarthCoordinates(form.CenterLatitude, form.CenterLongitude, e.Latitude, e.Longitude) <= form.Radius)
                                    && (DateTime.Compare(e.Date, DateTime.Now) >= 0))
                              select new EventScanDTO
                              {
                                  Address = e.Address,
                                  Date = e.Date,
                                  Title = e.Title,
                                  Id = e.Id,
                                  CategoriesNames = UtilityMethods.ParseCategoryListToCategoryNamesList(e.Categories.ToList()),
                                  TagsNames = UtilityMethods.ParseTagsListToTagsNamesList(e.Tags.ToList())
                              };
            return foundEvents;
        }
        [Route("ParticipateToEvent")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> ParticipateToEvent(EventParticipationForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Event foundEvent = await db.Events.Include(b => b.Participants).SingleOrDefaultAsync(i => i.Id == form.EventId);
            if(foundEvent == null)
            {
                return NotFound();
            }
            User user = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == form.Username);
            if (user == null)
            {
                return NotFound();
            }
            if(user.JoinedEvents.Contains(foundEvent) || foundEvent.Participants.Contains(user))
            {
                return BadRequest("User already participates");
            }
            user.JoinedEvents.Add(foundEvent);
            foundEvent.Participants.Add(user);
            db.Entry(user).State = EntityState.Modified;
            db.Entry(foundEvent).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();

        }
        [ResponseType(typeof(void))]
        [Route("CancelParticipationToEvent")]
        [HttpPut]
        public async Task<IHttpActionResult> CancelParticipationToEvent(EventParticipationForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Event foundEvent = await db.Events.Include(b => b.Participants).SingleOrDefaultAsync(i => i.Id == form.EventId);
            if (foundEvent == null)
            {
                return NotFound();
            }
            User user = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == form.Username);
            if (user == null)
            {
                return NotFound();
            }
            if ((!user.JoinedEvents.Contains(foundEvent)) || (!foundEvent.Participants.Contains(user)))
            {
                return BadRequest("User doesnt participate to event");
            }
            user.JoinedEvents.Remove(foundEvent);
            foundEvent.Participants.Remove(user);
            db.Entry(user).State = EntityState.Modified;
            db.Entry(foundEvent).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
        [Route("IsUserParticipatingToEvent")]
        [ResponseType(typeof(bool))]
        [HttpPut]
        public async Task<IHttpActionResult> IsUserParticipatingToEvent (EventParticipationForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Event foundEvent = await db.Events.Include(b => b.Participants).SingleOrDefaultAsync(i => i.Id == form.EventId);
            if (foundEvent == null)
            {
                return NotFound();
            }
            User user = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == form.Username);
            if (user == null)
            {
                return NotFound();
            }
            if (user.JoinedEvents.Contains(foundEvent) || foundEvent.Participants.Contains(user))
            {
                return Ok(true);
            }
            return Ok(false);

        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvent(long id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Events
        [HttpPost]
        [ResponseType(typeof(Event))]
        [Route("PostEvent")]
        public async Task<IHttpActionResult> PostEvent(EventCreationDTO eventToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == eventToAdd.CreatorUsername);
            if (user == null)
            {
                return NotFound();
            }
            Event createdDbEvent = new Event
            {
                Address = eventToAdd.Address,
                CreatorId = user.Id,
                Date = eventToAdd.Date,
                Description = eventToAdd.Description,
                Title = eventToAdd.Title,
                UrlFacebook = eventToAdd.FacebookUrl,
                Latitude = eventToAdd.Latitude,
                Longitude = eventToAdd.Longitude
            };
            string categorySearchCondition = "";
            int count = 0;
            foreach (var categoryId in eventToAdd.CategoriesId)
            {
                if (count != 0)
                {
                    categorySearchCondition += "||";
                }
                categorySearchCondition += "Id ==" + categoryId;
                count++;
            }
            //using string as a condition for Where is allowed by the dynamic LINQ library
            var foundCategories = from c in db.Categories.Where(categorySearchCondition) select c;
            //Any() is a method that returns a bool telling whether the IQueryable list is empty or not.
            if (foundCategories.Any())
            {
                foreach (var cat in foundCategories)
                {
                    createdDbEvent.Categories.Add(cat);
                }
            }
            string tagSearchCondition = "";
            count = 0;
            foreach (var tagId in eventToAdd.TagsId)
            {
                if (count != 0) tagSearchCondition += "||";
                tagSearchCondition += "Id ==" + tagId;
                count++;
            }
            var foundTags = from t in db.Tags.Where(tagSearchCondition) select t;
            if (foundTags.Any())
            {
                foreach (var tag in foundTags)
                {
                    createdDbEvent.Tags.Add(tag);
                }
            }
            var firstParticipant = await db.UserProfiles.FindAsync(user.Id);
            createdDbEvent.Participants.Add(firstParticipant);
            //require testing for an unexisting category or tag
            db.Events.Add(createdDbEvent);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = createdDbEvent.Id }, createdDbEvent);
            return Ok(eventToAdd);
        }
        [Route("DeleteEvent")]
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(long id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            await db.SaveChangesAsync();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(long id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}