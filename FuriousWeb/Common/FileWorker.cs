using System;
using System.IO;
using System.Web;

namespace FuriousWeb
{
    public class FileWorker
    {
        public static void DownloadImage(HttpPostedFileBase image)
        {
            if (image == null)
                throw new FileDownloadException();

            string imagesFolderPath = HttpContext.Current.Server.MapPath(Globals.PathToProductImagesFolder);
            string fullImagePath = Path.Combine(imagesFolderPath, Path.GetFileName(image.FileName));

            try
            {
                image.SaveAs(fullImagePath);
            }
            catch (Exception ex)
            {
                throw new FileDownloadException(ex.Message, ex.InnerException);
            }
        }

        public static void DeleteFile(string relativePath)
        {
            string fullImagePath = HttpContext.Current.Server.MapPath(relativePath);

            try
            {
                if (File.Exists(fullImagePath))
                    File.Delete(fullImagePath);
            }
            catch (Exception) {}       
        }
    }
}