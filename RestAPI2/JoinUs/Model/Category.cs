namespace JoinUs.Model
{
    public class Category
    {
        public string Title { get; set; }

        public int DbId { get; set; }

        public string ImagePath { get; set; }
        public Category(string title)
        {
            Title = title;
            //Ids:Etude = 1, Jeux video = 2, langues = 3, Sport = 4,Dîner = 5, Soirée = 6, Culture = 7, Musique = 8, Jeux de societe = 9 
            switch (title)
            {
                case "Etude":
                    ImagePath = "Assets/study.jpg";
                    DbId = 1;
                    break;
                case "Jeux vidéo":
                    ImagePath = "Assets/VideoGame.jpg";
                    DbId = 2;
                    break;
                case "Langues":
                    ImagePath = "Assets/languages.jpg";
                    DbId = 3;
                    break;
                case "Sport":
                    ImagePath = "Assets/sports.jpg";
                    DbId = 4;
                    break;
                case "Dîner":
                    ImagePath = "Assets/Dinner.png";
                    DbId = 5;
                    break;
                case "Soirée":
                    ImagePath = "Assets/Party.jpg";
                    DbId = 6;
                    break;
                case "Culture":
                    ImagePath = "Assets/Culture.jpg";
                    DbId = 7;
                    break;
                case "Musique":
                    ImagePath = "Assets/music.jpg";
                    DbId = 8;
                    break;
                case "Jeux de société":
                    ImagePath = "Assets/BoardGame.jpg";
                    DbId = 9;
                    break;
                default:
                    ImagePath = "Assets/questionMark.jpg";
                    DbId = 10;
                    break;
            }
        }

    }
}
