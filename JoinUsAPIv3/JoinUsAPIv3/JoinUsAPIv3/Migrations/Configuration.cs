namespace JoinUsAPIv3.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<JoinUsAPIv3.Models.JoinUsAPIv3Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(JoinUsAPIv3.Models.JoinUsAPIv3Context context)
        {
            var studyCategory = new Category() { Id = 1, Name = "Etude" };
            var videoGamesCategory = new Category() { Id = 2, Name = "Jeux vidéo" };
            var languagesCategory = new Category() { Id = 3, Name = "Langues" };
            var sportCategory = new Category() { Id = 4, Name = "Sport" };
            var dinnerCategory = new Category() { Id = 5, Name = "Dîner" };
            var partyCategory = new Category() { Id = 6, Name = "Soirée" };
            var cultureCategory = new Category() { Id = 7, Name = "Culture" };
            var musicCategory = new Category() { Id = 8, Name = "Musique" };
            var boardGamesCategory = new Category() { Id = 9, Name = "Jeux de société" };
            context.Categories.AddOrUpdate(x => x.Id,
                studyCategory,
                videoGamesCategory,
                languagesCategory,
                sportCategory,
                dinnerCategory,
                partyCategory,
                cultureCategory,
                musicCategory,
                boardGamesCategory
                );
            var tagLG = new Tag() { Id = 1, Name = "#LoupGarou" };
            var tagChess = new Tag() { Id = 2, Name = "#Echecs" };
            var tagPicasso = new Tag() { Id = 3, Name = "#Picasso" };
            var tagHS = new Tag() { Id = 4, Name = "#Hearthstone" };
            var tagEnglish = new Tag() { Id = 5, Name = "#English" };
            var tagAth = new Tag() { Id = 6, Name = "#Athlétisme" };
            var tagResto = new Tag() { Id = 7, Name = "#RestoChinois" };
            var tagMaths = new Tag() { Id = 8, Name = "#Maths" };
            var tagMetallica = new Tag() { Id = 9, Name = "#Metallica" };
            var tagContest = new Tag() { Id = 10, Name = "#Concours" };
            var tagExpo = new Tag() { Id = 11, Name = "#Expo" };
            context.Tags.AddOrUpdate(x => x.Id,
                tagLG,
                tagChess,
                tagPicasso,
                tagHS,
                tagEnglish,
                tagAth,
                tagResto,
                tagMaths,
                tagMetallica,
                tagContest,
                tagExpo
                );
            var userRelthar = new User() { Id = 1, FirstName = "Nicolas", LastName = "Bauthier", Birthdate = new DateTime(1995, 2, 19)};
            var userBrodylive = new User() { Id = 2, FirstName = "Jennifer", LastName = "Denis", Birthdate = new DateTime(1994, 11, 4)};
            context.UserProfiles.AddOrUpdate(x => x.Id,
                userRelthar,
                userBrodylive
                );

            var eventLG = new Event() { Id = 1, Title = "Soirée Loup-garou aux 3D", Address = "Place Abbé Joseph, 11, 5000 Namur", CreatorId = 1, Date = new DateTime(2017, 9, 14), Description = "Notre café vous invite une fois encore à disputer une partie des \"Loups-Garou de Thiercellieux\". Venez nombreux!",Longitude = 4.861084, Latitude = 50.47008 };
            eventLG.Categories.Add(boardGamesCategory);
            eventLG.Tags.Add(tagLG);
            var eventChess = new Event() { Id = 2, Title = "Concour d'échecs ChessMaster 2.0", Address = "Rue des Carmes, 77, 5000 Namur", CreatorId = 1, Date = new DateTime(2017, 8, 18), Description = "Notre tournoi d'échecs par catégories ELO est ouvert. Venez disputer les meilleures parties d'échecs!",Longitude = 4.863521,Latitude = 50.467489 };
            eventChess.Categories.Add(boardGamesCategory);
            eventChess.Tags.Add(tagChess);
            eventChess.Tags.Add(tagContest);
            var eventHS = new Event() { Id = 3, Title = "Hearthstone Arena", Address = "Rue de Bruxelles, 51, 5000 Namur", CreatorId = 2, Date = new DateTime(2017, 8, 18), Description = "Le petit bitu vous invite à participer à son tournoi de Hearthstone! Ce tournoi est à élimination directe. jusqu'à 200 euros de chèque pour du matériel informatique reviendront au vainqueur!",Longitude = 50.465562,Latitude= 4.862317 };
            eventHS.Categories.Add(videoGamesCategory);
            eventHS.Tags.Add(tagHS);
            eventHS.Tags.Add(tagContest);
            var eventExpo = new Event() { Id = 4, Title = "Exposition Picasso", Address = "Place du manège, 1, 6000 Charleroi", CreatorId = 1, Date = new DateTime(2017, 8, 21), Description = "Le Palais des Beaux-Arts vous invite à venir découvrir sa nouvelle exposition concernant Pablo Picasso!",Latitude = 50.413369,Longitude = 4.444334 };
            eventExpo.Categories.Add(cultureCategory);
            eventExpo.Tags.Add(tagPicasso);
            eventExpo.Tags.Add(tagExpo);
            var eventEng = new Event() { Id = 5, Title = "TED talk:\"How new Technologies affect social relationships\"", Address = "Rue de Bruxelles, 61,5000 Namur", CreatorId = 1, Date = new DateTime(2017, 10, 17), Description = "TED Talk about how new technologies affect our behavior towards other people",Latitude= 50.466649,Longitude= 4.859927 };
            eventEng.Categories.Add(cultureCategory);
            eventEng.Categories.Add(languagesCategory);
            eventEng.Tags.Add(tagEnglish);
            var eventAth = new Event() { Id = 6, Title = "Petite course dans Namur", Address = "Rue des Carmes, 77, 5000 Namur", CreatorId = 1, Date = new DateTime(2017, 11, 18), Description = "J'organise une course dans Namur. Point de départ chez moi (voir adresse). Contactez-moi pour plus d'infos",Longitude = 4.863521, Latitude = 50.467489 };
            eventAth.Categories.Add(sportCategory);
            eventAth.Tags.Add(tagAth);
            var eventMaths = new Event() { Id = 7, Title = "Remédiation Maths", Address = "Rue des Carmes, 77, 5000 Namur", CreatorId = 1, Date = new DateTime(2017, 6, 7), Description = "Vous avez des problèmes dans le domaine des mathématiques? Je vous invite à me rendre visite, je me ferai un plaisir de vous aider", Longitude = 4.863521, Latitude = 50.467489 };
            eventMaths.Categories.Add(studyCategory);
            eventMaths.Tags.Add(tagMaths);
            var eventResto = new Event() { Id = 8, Title = "Petite sortie resto chinois", Address = "Rue Borget, 8, 5000 Namur", CreatorId = 2, Date = new DateTime(2017, 11, 26), Description = "Je souhaite rencontrer des gens sur Namur. Je vous attends nombreux au restaurant \"Chez Chen\"!",Latitude= 50.46826,Longitude = 4.866322 };
            eventResto.Categories.Add(dinnerCategory);
            eventResto.Tags.Add(tagResto);
            var eventConcert = new Event() { Id = 9, Title = "Concert de Metallica", Address = "Rue des Carmes, 77, 5000 Namur", CreatorId = 1, Date = new DateTime(2016, 12, 18), Description = "Je cherche des gens qui seraient intéressés à venir avec moi au concert de Metallica du 18 décembre.", Longitude = 4.863521, Latitude = 50.467489 };
            eventConcert.Categories.Add(partyCategory);
            eventConcert.Categories.Add(musicCategory);
            eventConcert.Tags.Add(tagMetallica);
            context.Events.AddOrUpdate(x => x.Id,
                eventLG,
                eventChess,
                eventHS,
                eventExpo,
                eventEng,
                eventAth,
                eventMaths,
                eventResto,
                eventConcert
                );
        }
    }
}
