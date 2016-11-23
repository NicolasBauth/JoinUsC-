namespace JoinUsAPI.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JoinUsAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "JoinUsAPI.Models.ApplicationDbContext";
        }
        
        protected override void Seed(JoinUsAPI.Models.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var admin = new ApplicationUser()
            {
                FirstName = "Nicolas",
                LastName = "Bauthier",
                UserName = "Relthar",
                Email = "nicolas.bauthier@hotmaiL.be",
                Birthdate = new DateTime(),
                LastLoc = "77,Rue Des carmes"
               
            };
            manager.Create(admin, "42");


            Category jeuxSociete = new Category()
            {
                Name = "Jeux de soci�t�"
            };
            context.Categories.Add(jeuxSociete);

            var modo = new ApplicationUser()
            {
                Birthdate = new DateTime(),
                Email = "brodylive@gmail.com",
                FirstName = "Jennifer",
                LastName = "Denis",
                LastLoc = "13, Rue Joseph Calozet",
                UserName = "BrodyLive"
            };
            manager.Create(modo,"millenium");
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
