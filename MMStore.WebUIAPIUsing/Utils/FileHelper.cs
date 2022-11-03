using Microsoft.AspNetCore.Routing.Constraints;

namespace MMStore.WebUIAPIUsing.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile,string filePath="/wwwroot/img/")
        {
            string fileName = "";
            fileName = formFile.FileName;
            string directory=Directory.GetCurrentDirectory()+filePath+fileName;
            using var stream=new FileStream(directory, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return fileName;
        }
        public static bool FileRemover(string fileName, string filePath = "/wwwroot/img/")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;
            if (File.Exists(directory)==true) 
            {
                File.Delete(directory); 
                return true;
            }
            return false;
        }
    } 
}
