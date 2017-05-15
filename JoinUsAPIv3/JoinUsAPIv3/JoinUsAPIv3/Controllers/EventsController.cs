
using DTOModels.EventDTOs;
using JoinUsAPIv3.Models;
using JoinUsAPIv3.Utility;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace JoinUsAPIv3.Controllers
{
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {

        private JoinUsAPIv3Context db = new JoinUsAPIv3Context();
        [HttpGet]
        //this method returns all the events from the database, formatted as EventPresentationDTO.
        //call: api/Events
        public IQueryable<EventPresentationDTO> GetEvents()
        {
            var events = from e in db.Events.Include(b => b.Creator).Include(b =>b.Participants).Include(b =>b.Categories)

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
                             //Cette ligne pose problème.
                             //CategoriesNames = UtilityMethods.ParseCategoryListToCategoryNamesList(e.Categories)
                         };

            return events;

        }
        //This method returns all the database events in the "EventShortDTO" format.
        //call: api/Events/GetEventsShort
        [Route("GetEventsShort")]
        public IQueryable<EventShortDTO> GetEventsShort()
        {
            var eventsShorts = from e in db.Events
                               select new EventShortDTO
                               {
                                   Address = e.Address,
                                   Date = e.Date,
                                   Title = e.Title,
                                   Id = e.Id
                               };
            return eventsShorts;
        }

        //call:api/Events/GetEventById?id=5
        //returns a raw event entity corresponding to the id provided as parameter
        //call:api/Events/GetEventById?id=5
        [Route("GetEventById")]
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(long id)
        {
            //FindAsync not available here because it's a method from the DbSet type, and Include returns a DbQuery.
            //SingleOrDefaultAsync is a DbQuery method. It goes straight to the database
            //and doesn't look first in the context to see if the entity exists.           
            Event @event = await db.Events.Include("Creator").SingleOrDefaultAsync(i => i.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
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
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(@event);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
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