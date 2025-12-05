using CORCEGuideApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Repositories
{
    public class AnnouncementRepository(DataContext dataContext)
    {
        public async Task<List<Announcement>> GetAllActiveAnnouncementsAsync()
        {
            return await dataContext.Announcements.AsNoTracking().ToListAsync();
        }

        public async Task<Announcement?> GetAnnouncementByIdAsync(int announcementId)
        {
            return await dataContext.Announcements
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AnnouncementId == announcementId);
        }

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            await dataContext.Announcements.AddAsync(announcement);
            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            var existingAnnouncement = await dataContext.Announcements
                .FirstOrDefaultAsync(a => a.AnnouncementId == announcement.AnnouncementId);

            if ( existingAnnouncement == null )
            {
                await Shell.Current.DisplayAlert("Error", "Announcement not found.", "OK");
                return;
            }

            existingAnnouncement.Title = announcement.Title;
            existingAnnouncement.Content = announcement.Content;
            existingAnnouncement.DatePosted = announcement.DatePosted;
            existingAnnouncement.ImagePath = announcement.ImagePath;
            existingAnnouncement.Category = announcement.Category;

            dataContext.Announcements.Update(existingAnnouncement);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAnnouncementAsync(int announcementId)
        {
            var announcement = await dataContext.Announcements.FindAsync(announcementId);

            if ( announcement == null )
            {
                await Shell.Current.DisplayAlert("Error", "Announcement not found.", "OK");
                return;
            }
            
            announcement.IsActive = false;
            
            dataContext.Announcements.Update(announcement);
            await dataContext.SaveChangesAsync();
        }
    }
}
