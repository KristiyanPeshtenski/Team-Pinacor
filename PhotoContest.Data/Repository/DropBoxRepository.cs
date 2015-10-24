namespace PhotoContest.Data.Repository
{
    using System;
    using System.IO;
    using DropNet;

    public static class DropBoxRepository
    {
        private static readonly DropNetClient client;

        // Add keys to separate configuration file 

        private const string AppKey = "8dy76y5l7xr08gf";
        private const string AppSecret = "j1vl9k97bndq34o";
        private const string AppTocken = "XN09yhu0CMAAAAAAAAAAEXpffl-GWa7HUS0whyPpM17W-ayUHb22waXiTccRw2D8";

        static DropBoxRepository()
        {
            client = new DropNetClient(AppKey, AppSecret, AppTocken);     
        }

        public static string Upload(string fileName, string authorUsername, Stream fileStram)
        {
            string fullFileName = authorUsername + "_" + fileName;
            client.UploadFile("/", fullFileName, fileStram);
            return fullFileName;
        }

        public static string Download(string path)
        {
            return client.GetMedia(path).Url;
        }
    }
}
