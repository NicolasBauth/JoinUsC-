using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    class DbInitializer : DropCreateDatabaseAlways<JoinUsContext>
    {

        protected override void Seed(JoinUsContext context)
        {
            
            User admin = new User()
            {
                FirstName = "Nicolas",
                LastName = "Bauthier",
                UserName = "Relthar",
                Email = "nicolas.bauthier@hotmaiL.be",
                Birthdate = new DateTime(),
                LastLoc = "77,Rue Des carmes"
            };
            context.Users.Add(admin);
            
            
            Category jeuxSociete = new Category()
            {
                Name = "Jeux de société"
            };
            context.Categories.Add(jeuxSociete);
            
            User modo = new User()
            {
                Birthdate = new DateTime(),
                Email = "brodylive@gmail.com",
                FirstName = "Jennifer",
                LastName = "Denis",
                LastLoc = "13, Rue Joseph Calozet",
                UserName = "BrodyLive",
            };
            context.Users.Add(modo);
            Event monopoly = new Event()
            {
                Title = "Monopoly rue des Carmes",
                Address = "77, Rue des Carmes, 5000 Namur",
                Date = new DateTime(),
                Description = "Partie de monopoly chez moi",
                UrlFacebook = "www.facebook.com/monopoly"
            };
            context.Events.Add(monopoly);
            
            Tag tagMonopoly = new Tag()
            {
                Name = "Monopoly"
            };
            context.Tags.Add(tagMonopoly);
            context.SaveChanges();
        }
    }
}
