﻿using DTOModels.UserDTOs;
using JoinUsAPIv3.Models;
using JoinUsAPIv3.Utility;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace JoinUsAPIv3.Controllers
{
    [Authorize]
    [RoutePrefix("api/UserProfiles")]
    public class UserProfilesController : ApiController
    {
        private JoinUsAPIv3Context db = new JoinUsAPIv3Context();

        
        [HttpGet]
        public IEnumerable<UserFullDTO> GetUsers()
        {
            var profiles = from e in db.UserProfiles.Include(b => b.CreatedEvents).Include(b => b.Followers).Include(b => b.Following).Include(b => b.Interests).Include(b => b.JoinedEvents).ToList()
                           select new UserFullDTO
                           {
                               Birthdate = e.Birthdate,
                               FirstName = e.FirstName,
                               LastName = e.LastName,
                               Interests = UtilityMethods.ParseCategoryListToCategoryNamesList(e.Interests.ToList()),
                               CreatedEvents = UtilityMethods.ParseEventListToEventNamesList(e.CreatedEvents.ToList()),
                               JoinedEvents = UtilityMethods.ParseEventListToEventNamesList(e.JoinedEvents.ToList()),
                               FollowerNames = UtilityMethods.ParseUserListtoUserNamesList(e.Followers.ToList()),
                               FollowingNames = UtilityMethods.ParseUserListtoUserNamesList(e.Following.ToList())
                           };
            return profiles;
        }
        [Route("UserProfileById")]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> GetUserProfileById(long id)
        {
            User user = await db.UserProfiles.Include(b => b.Interests).SingleOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            List<string> interestNames = UtilityMethods.ParseCategoryListToCategoryNamesList(user.Interests);
            UserProfileDTO userProfile = new UserProfileDTO { BirthDate = user.Birthdate, FirstName = user.FirstName, LastName = user.LastName, Interests = interestNames };
            return Ok(userProfile);
        }

        [Route("UserProfileByUsername")]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> GetUserProfileByUsername(string username)
        {
            User user = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            List<string> interestNames = UtilityMethods.ParseCategoryListToCategoryNamesList(user.Interests);
            UserProfileDTO userProfile = new UserProfileDTO { BirthDate = user.Birthdate, FirstName = user.FirstName, LastName = user.LastName, Interests = interestNames };
            return Ok(userProfile);
        }

        [HttpPut]
        [Route("UpdateUserInterests")]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> UpdateUserProfile(UpdateUserInterestForm updateForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = await db.UserProfiles.SingleOrDefaultAsync(i => i.ReferencedApplicationUser.UserName == updateForm.Username);
            if (user == null)
            {
                return NotFound();
            }
            List<Category> oldUserInterests = user.Interests.ToList();
            foreach (var interest in oldUserInterests)
            {
                interest.Users.Remove(user);
            }
            List<Category> newUserInterests = new List<Category>();
            foreach (var categoryId in updateForm.NewInterestsIds)
            {
                Category foundCategory = await db.Categories.SingleOrDefaultAsync(i => i.Id == categoryId);
                if (foundCategory == null)
                {
                    return NotFound();
                }
                foundCategory.Users.Add(user);
                newUserInterests.Add(foundCategory);
            }

            foreach (var interest in newUserInterests)
            {
                interest.Users.Add(user);
                db.Entry(interest).State = EntityState.Modified;
            }

            user.Interests = newUserInterests;
            db.Entry(user).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            List<string> updatedProfileInterestNames = new List<string>();
            foreach (var interest in user.Interests)
            {
                updatedProfileInterestNames.Add(interest.Name);
            }
            UserProfileDTO updatedProfile = new UserProfileDTO();
            updatedProfile.BirthDate = user.Birthdate;
            updatedProfile.FirstName = user.FirstName;
            updatedProfile.LastName = user.LastName;
            updatedProfile.Interests = updatedProfileInterestNames;
            return Ok(updatedProfile);
        }
        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(long id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfiles.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(long id)
        {
            User user = await db.UserProfiles.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.UserProfiles.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(long id)
        {
            return db.UserProfiles.Count(e => e.Id == id) > 0;
        }
    }
}