using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORCEGuideApp.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string?> SaveImageLocaclyAsync(FileResult fileResult)
        {
            if ( fileResult == null )
                return null;

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileResult.FileName)}";
            var newFilePath = Path.Combine(FileSystem.AppDataDirectory, newFileName);
            using var sourceStream = await fileResult.OpenReadAsync();
            using var destinationStream = File.OpenWrite(newFilePath);
            await sourceStream.CopyToAsync(destinationStream);
            return newFilePath;
        }
    }
}
