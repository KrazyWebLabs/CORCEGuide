using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public DateTime DatePosted { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }   // 👈 Clave foránea obligatoria
        public ICollection<Favorite>? Favorites { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
