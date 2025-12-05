using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public Role? Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
