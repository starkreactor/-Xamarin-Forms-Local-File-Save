
using LocalFileSave.iOS;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace LocalFileSave.iOS
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using LocalFileSave;

    /// <summary>
    /// class : FileService
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="text">The text.</param>
        /// <param name="append">if set to <c>true</c> [append].</param>
        /// <returns></returns>
        public async Task SaveAsync(string filename, string text, bool append = true)
        {
            var path = this.CreatePathToFile(filename);
            using (var sw = new StreamWriter(path, append))
                await sw.WriteLineAsync(text);
        }

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public async Task<string> ReadAsync(string filename)
        {
            var path = this.CreatePathToFile(filename);
            using (var sr = File.OpenText(path))
                return await sr.ReadToEndAsync();
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool FileExists(string filename)
        {
            return File.Exists(this.CreatePathToFile(filename));
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task DeleteAsync(string fileName)
        {
            var path = this.CreatePathToFile(fileName);
            await Task.Run(() => { File.Delete(path); });
        }

        /// <summary>
        /// Saves the image asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public Task<string> SaveImageAsync(string url, string fileName)
        {
            var webClient = new WebClient();
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var localFilename = fileName;
            var localPath = Path.Combine(documentsPath, localFilename);

            webClient.DownloadDataCompleted += (s, e) =>
            {
                var bytes = e.Result; // get the downloaded data                
                File.WriteAllBytes(localPath, bytes); // writes to local storage   
            };
            var imageurl = new Uri(url);
            if (!File.Exists(localPath))
            {
                webClient.DownloadDataAsync(imageurl);
            }

            return Task.FromResult(localPath);
        }

        /// <summary>
        /// Reads the image asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public string ReadImageAsync(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localFilename = fileName;
            string localPath = Path.Combine(documentsPath, localFilename);
            return localPath;
        }

        #region Private Methods

        /// <summary>
        /// Creates the path to file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private string CreatePathToFile(string filename)
        {
            var docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(docsPath, filename);
        }
        #endregion
    }
}
