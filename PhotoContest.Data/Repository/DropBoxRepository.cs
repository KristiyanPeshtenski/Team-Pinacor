namespace PhotoContest.Data.Repository
{
    using System;
    using System.IO;
    using DropNet;

    public static class DropBoxRepository
    {
        private static readonly DropNetClient client;

        // Add keys to separate configuration file 

        private const string AppKey = "6kq6aqvcbhv1sqb";
        private const string AppSecret = "03grazystjvzsc4";
        private const string AppTocken = "HIR8Jpj4AQAAAAAAAAAAGR_uid0jtLCinFe1ewJ2WUz6qHSON48vDZPvG6BIxeKj";

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
            var media = client.GetMedia(path);
            
            return client.GetMedia(path).Url;
        }
    }
}
