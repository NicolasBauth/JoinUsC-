using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.Model
{
    public class Category
    {
        public string Title { get; set; }

        public long DbId { get; set; }

        public string ImagePath { get; set; }
        public Category(string title, long dbId)
        {
            Title = title;
            DbId = dbId;
            //Ids:Etude = 1, Jeux video = 2, langues = 3, Sport = 4,Dîner = 5, Soirée = 6, Culture = 7, Musique = 8, Jeux de societe = 9 
            switch(DbId)
            {
                case 1:
                    ImagePath = "Assets/study.jpg";
                    break;
                case 2:
                    ImagePath = "Assets/VideoGame.jpg";
                    break;
                case 3:
                    ImagePath = "Assets/languages.jpg";
                    break;
                case 4:
                    ImagePath = "Assets/sports.jpg";
                    break;
                case 5:
                    ImagePath = "Assets/Dinner.png";
                    break;
                case 6:
                    ImagePath = "Assets/Party.jpg";
                    break;
                case 7:
                    ImagePath = "Assets/Culture.jpg";
                    break;
                case 8:
                    ImagePath = "Assets/music.jpg";
                    break;
                case 9:
                    ImagePath = "Assets/BoardGame.jpg";
                    break;
                default:
                    ImagePath = "Assets/questionMark.jpg";
                    break;
            }
        }

    }
}
