using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public static class EventDAO
    {
        public static bool CommitEvent(Event eventToCommit)
        {
            return true;
        }

        public static List<Event> GetEventsAround(int radius, IEnumerable<Category> selectedCategories)
        {
            List<Event> eventsAround = new List<Event>();
            List<Category> monopolyCategories = new List<Category>();
            List<Category> werewolfCategories = new List<Category>();
            List<Category> expoCategories = new List<Category>();
            List<Category> restaurantCategories = new List<Category>();
            Category boardGame = new Category("Jeux de société", "Assets/BoardGame.jpg");
            Category dinner = new Category("Dîner", "Assets/Dinner.png");
            Category videoGames = new Category("Jeux vidéo", "Assets/VideoGame.jpg");
            Category party = new Category("Soirée", "Assets/Party.jpg");
            Category culture = new Category("Culture", "Assets/Culture.jpg");
            monopolyCategories.Add(boardGame);
            werewolfCategories.Add(boardGame);
            werewolfCategories.Add(party);
            expoCategories.Add(culture);
            restaurantCategories.Add(dinner);
            restaurantCategories.Add(party);
            Event monopoly = new Event("Monopoly chez moi", "petit Monopoly oklm", "113,rue des jeux de société", new DateTime(2017, 01, 20), monopolyCategories);
            Event werewolf = new Event("Les loups Garou de Thiercellieux", "Le 3D vous invite une fois encore à participer à une partie des \"Loups garou de Thiercellieux\". Venez nombreux. Explication des règles possible en début de partie", "77, Rue des Carmes", new DateTime(2016, 12, 21), werewolfCategories);
            Event expo = new Event("Expo d'art contemporain", "Venez découvrir de purs chefs-d'oeuvre", "3,Place de la Station", new DateTime(2017, 02, 19), expoCategories);
            Event restaurant = new Event("Restaurant chinois", "Je cherche des gens pour se joindre à moi lors d'une soirée au restaurant chinois", "18, Place de la station", new DateTime(2017, 03, 05), restaurantCategories);
            User user1 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Relthar", "henachallenge");
            User user2 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Brodylive", "henachallenge");
            User user3 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Jon", "henachallenge");
            User user4 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Olikity", "henachallenge");
            User user5 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Yenju", "henachallenge");
            monopoly.Creator = user1;
            werewolf.Creator = user3;
            expo.Creator = user1;
            restaurant.Creator = user2;
            List<User> participants1 = new List<User>();
            List<User> participants2 = new List<User>();
            participants1.Add(user3);
            participants1.Add(user4);
            participants1.Add(user1);
            participants2.Add(user2);
            participants2.Add(user3);
            participants2.Add(user5);
            participants2.Add(user4);
            monopoly.Participants = participants1;
            werewolf.Participants = participants2;
            expo.Participants = participants1;
            restaurant.Participants = participants2;
            List<Tag> werewolfTags = new List<Tag>();
            Tag tagLoupGarou = new Tag("#loupsgarou");
            Tag tag3D = new Tag("#Aux3D");
            werewolfTags.Add(tagLoupGarou);
            werewolfTags.Add(tag3D);
            werewolf.Tags = werewolfTags;
            werewolf.UrlFacebook = "http://www.google.com";
            eventsAround.Add(monopoly);
            eventsAround.Add(werewolf);
            eventsAround.Add(expo);
            eventsAround.Add(restaurant);
            return eventsAround;
        }

        public static List<Event> GetAllEventsOfUser(User user)
        {
            List<Event> eventsAround = new List<Event>();
            List<Category> monopolyCategories = new List<Category>();
            List<Category> werewolfCategories = new List<Category>();
            List<Category> expoCategories = new List<Category>();
            List<Category> restaurantCategories = new List<Category>();
            Category boardGame = new Category("Jeux de société", "Assets/BoardGame.jpg");
            Category dinner = new Category("Dîner", "Assets/Dinner.png");
            Category videoGames = new Category("Jeux vidéo", "Assets/VideoGame.jpg");
            Category party = new Category("Soirée", "Assets/Party.jpg");
            Category culture = new Category("Culture", "Assets/Culture.jpg");
            monopolyCategories.Add(boardGame);
            werewolfCategories.Add(boardGame);
            werewolfCategories.Add(party);
            expoCategories.Add(culture);
            restaurantCategories.Add(dinner);
            restaurantCategories.Add(party);
            Event monopoly = new Event("Monopoly chez moi", "petit Monopoly oklm", "113,rue des jeux de société", new DateTime(2017, 01, 20), monopolyCategories);
            Event werewolf = new Event("Les loups Garou de Thiercellieux", "Le 3D vous invite une fois encore à participer à une partie des \"Loups garou de Thiercellieux\". Venez nombreux. Explication des règles possible en début de partie", "77, Rue des Carmes", new DateTime(2016, 12, 21), werewolfCategories);
            Event expo = new Event("Expo d'art contemporain", "Venez découvrir de purs chefs-d'oeuvre", "3,Place de la Station", new DateTime(2017, 02, 19), expoCategories);
            Event restaurant = new Event("Restaurant chinois", "Je cherche des gens pour se joindre à moi lors d'une soirée au restaurant chinois", "18, Place de la station", new DateTime(2017, 03, 05), restaurantCategories);
            User user1 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Relthar", "henachallenge");
            User user2 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Brodylive", "henachallenge");
            User user3 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Jon", "henachallenge");
            User user4 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Olikity", "henachallenge");
            User user5 = new User("Nicolas", "Bauthier", "nicolas.bauthier@hotmail.be", new DateTime(1995, 02, 19), "77, Rue des carmes", "Assets/PhotoProfilNicolas.jpg", "Yenju", "henachallenge");
            monopoly.Creator = user1;
            werewolf.Creator = user3;
            expo.Creator = user1;
            restaurant.Creator = user2;
            List<User> participants1 = new List<User>();
            List<User> participants2 = new List<User>();
            participants1.Add(user3);
            participants1.Add(user4);
            participants1.Add(user1);
            participants2.Add(user2);
            participants2.Add(user3);
            participants2.Add(user5);
            participants2.Add(user4);
            monopoly.Participants = participants1;
            werewolf.Participants = participants2;
            expo.Participants = participants1;
            restaurant.Participants = participants2;
            List<Tag> werewolfTags = new List<Tag>();
            Tag tagLoupGarou = new Tag("#loupsgarou");
            Tag tag3D = new Tag("#Aux3D");
            werewolfTags.Add(tagLoupGarou);
            werewolfTags.Add(tag3D);
            werewolf.Tags = werewolfTags;
            werewolf.UrlFacebook = "http://www.google.com";
            eventsAround.Add(monopoly);
            eventsAround.Add(werewolf);
            eventsAround.Add(expo);
            eventsAround.Add(restaurant);
            return eventsAround;
        }

        public static void KickEvent(User userToKick, Event targetedEvent)
        {
            if(targetedEvent.Participants!=null && !(targetedEvent.Participants.Count() == 0))
            {
                int index = 0;
                foreach(var user in targetedEvent.Participants)
                {
                    if(user.UserName == userToKick.UserName)
                    {
                        break;
                    }
                    index++;
                }
                if(userToKick.UserName == targetedEvent.Participants.ElementAt(index).UserName)
                {
                    targetedEvent.Participants.RemoveAt(index);
                    //update the database
                }
            }
        }

        public static void JoinEvent(User userToAdd, Event targetedEvent)
        {
            targetedEvent.Participants.Add(userToAdd);
            //update the database;
        }
    }
}
