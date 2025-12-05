using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public User? User { get; set; }
        public Announcement? Announcement { get; set; }
    }
}
