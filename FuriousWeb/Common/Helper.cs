namespace FuriousWeb
{
    public class Helper
    {
        public static string GetRelativePathForResource(string resourceName)
        {
            return Globals.PathToProductImagesFolder + '/' + resourceName;
        }
    }
}