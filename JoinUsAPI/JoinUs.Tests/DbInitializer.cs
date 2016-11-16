using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinUsClassLibrary.Models;
using System.Data.Entity;

namespace JoinUs.Tests
{
    public class DbInitializer : DropCreateDatabaseAlways<JoinUsContext>
    {
        protected override void Seed(JoinUsContext context)
        {
            User admin = new User()
            {
                Birthdate = new DateTime(),
                Email = "nicolas.bauthier@hotmaiL.be",
                FirstName="Nicolas",
                LastName="Bauthier",
                LastLoc="77,Rue Des carmes",
                UserName="Relthar"
            };
            Category jeuxSociete = new Category()
            {
                Name = "Jeux de société"
            };

            User modo = new User()
            {
                Birthdate = new DateTime(),
                Email = "brodylive@gmail.com",
                FirstName = "Jennifer",
                LastName = "Denis",
                LastLoc = "13, Rue Joseph Calozet",
                UserName = "BrodyLive",
            };
            Event monopoly = new Event()
            {
                Address = "77, Rue des Carmes, 5000 Namur",
                Creator = admin,
                Date = new DateTime(),
                Description = "Partie de monopoly chez moi",
                Title = "Monopoly rue des Carmes",
                UrlFacebook = "www.facebook.com/monopoly"
            };
            Tag tagMonopoly = new Tag()
            {
                Name = "Monopoly"
            };
            context.Users.Add(admin);
            context.Users.Add(modo);
            context.Categories.Add(jeuxSociete);
            context.Events.Add(monopoly);
            context.Tags.Add(tagMonopoly);
            context.SaveChanges();
        }
    }
}
