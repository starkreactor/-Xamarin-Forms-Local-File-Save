
using LocalFileSave.Droid;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace LocalFileSave.Droid
{
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Android.Content;
using Android.Graphics;

using LocalFileSave;

    /// <summary>
    /// class: FileService
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
            {
                await sw.WriteLineAsync(text);
            }
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
            {
                var result = await sr.ReadToEndAsync();
                return result;
            }

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
            var bmp = this.GetImageBitmapFromUrl(url);
            var storedPath = this.SaveToInternalSorage(bmp, fileName);
            return Task.FromResult(storedPath);
        }

        /// <summary>
        /// Reads the image asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public string ReadImageAsync(string fileName)
        {
            var cw = new ContextWrapper(Android.App.Application.Context);
            Java.IO.File directory = cw.GetDir("ImageFolder", FileCreationMode.Private);
            var mypath = new Java.IO.File(directory, fileName);
            string path = mypath.AbsolutePath;
            return path;
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

        /// <summary>
        /// The get image bitmap from url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        /// <summary>
        /// The save to internal sorage.
        /// </summary>
        /// <param name="bitmapImage">
        /// The bitmap image.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string SaveToInternalSorage(Bitmap bitmapImage, string id)
        {
            var contentWrapper = new ContextWrapper(Android.App.Application.Context);

            // path to /data/data/yourapp/app_data/imageDir
            var directory = contentWrapper.GetDir("ImageFolder", FileCreationMode.Private);
            var mypath = new Java.IO.File(directory, id);
            var path = mypath.AbsolutePath;
            try
            {
                using (var os = new FileStream(path, FileMode.Create))
                {
                    bitmapImage.Compress(Bitmap.CompressFormat.Png, 100, os);
                }
            }
            catch (Exception ex)
            {
            }

            return path;
        }

        #endregion
    }
}