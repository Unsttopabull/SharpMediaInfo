using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Linq;

namespace Frost.SharpMediaInfo.Collections {

    internal class MediaFileCollection : KeyedCollection<string, MediaListFile> {

        #region Get

        /// <summary>Gets the value associated with the specified key.</summary>
        /// <returns>Is <c>true</c> if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="file">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="file"/> parameter. This parameter is passed uninitialized.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool TryGetValue(string key, out MediaListFile file) {
            if (Dictionary == null) {
                file = null;
                return false;
            }
            return Dictionary.TryGetValue(key, out file);
        }

        /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
        /// <returns>The key for the specified element.</returns>
        /// <param name="item">The element from which to extract the key.</param>
        protected override string GetKeyForItem(MediaListFile item) {
            return item.General.FileInfo.FullPath;
        }

        public MediaListFile GetFirstFileWithPattern(Regex regex) {
            return Dictionary != null
                ? Dictionary.FirstOrDefault(kvp => regex.IsMatch(kvp.Key)).Value
                : null;
        }

        /// <summary>Gets the file paths of files in the list.</summary>
        /// <returns>Filepaths of files in the list.</returns>
        public IEnumerable<string> GetFilePaths() {
            return Dictionary == null
                ? Enumerable.Empty<string>()
                : Dictionary.Keys;
        }

        public IEnumerable<MediaListFile> GetFilesWithPattern(Regex regex) {
            if (Dictionary == null) {
                return Enumerable.Empty<MediaListFile>();
            }
            return Dictionary.Where(kvp => regex.IsMatch(kvp.Key))
                             .Select(kvp => kvp.Value);
        }

        #endregion

        public void Close(int filePos) {
            this[filePos].Close(); //Dispose the file

            RemoveAt(filePos);

            for (int i = filePos; i < Count; i++) {
                this[i].FileIndex--;
            }
        }
    }

}