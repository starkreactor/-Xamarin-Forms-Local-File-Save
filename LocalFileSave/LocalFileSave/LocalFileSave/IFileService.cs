// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileService.cs" company="Microsoft">
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF 
// OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace LocalFileSave
{
    using System.Threading.Tasks;

    /// <summary>
    /// interface : IFileService
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="text">The text.</param>
        /// <param name="append">if set to <c>true</c> [append].</param>
        /// <returns></returns>
        Task SaveAsync(string filename, string text, bool append = true);

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        Task<string> ReadAsync(string filename);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        Task DeleteAsync(string filename);

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        bool FileExists(string filename);

        /// <summary>
        /// Saves the image asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task<string> SaveImageAsync(string url, string fileName);

        /// <summary>
        /// Reads the image asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        string ReadImageAsync(string fileName);

    }
}
