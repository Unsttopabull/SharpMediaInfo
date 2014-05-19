using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Frost.SharpMediaInfo.Collections;

namespace Frost.SharpMediaInfo {

    public class MediaInfoList : IDisposable, IEnumerable<MediaListFile> {

        private readonly IntPtr _handle;
        private readonly MediaFileCollection _files;
        private readonly bool _noDll;

        #region Constructors
        public MediaInfoList() {
            bool is64 = Environment.Is64BitProcess;
            try {
                _handle = is64 ? MediaInfoList_New_x64() : MediaInfoList_New();
            }
            catch (BadImageFormatException) {
                _handle = is64 ? MediaInfoList_New() : MediaInfoList_New_x64();
            }
            catch (DllNotFoundException e) {
                _noDll = true;
                throw e;
            }
            _files = new MediaFileCollection();
        }

        public MediaInfoList(string fileOrFolderPath, InfoFileOptions options = InfoFileOptions.Nothing, bool cacheInfom = true, bool allInfoCache = true) : this() {
            int count = Open(fileOrFolderPath, options);
            InitFiles(count, cacheInfom, allInfoCache);
        }

        public MediaInfoList(IEnumerable<string> filePaths, InfoFileOptions options = InfoFileOptions.Nothing, bool cacheInfom = true, bool allInfoCache = true) : this() {
            int count = 0;
            //opening multiple files in a single go currently not supported (even if documentation says otherwise)
            foreach (string filePath in filePaths) {
                Open(filePath, options);
                count++;
            }

            InitFiles(count, cacheInfom, allInfoCache);
        }

        //Should only be called by a constructor
        private void InitFiles(int count, bool cacheInform, bool allInfoCache) {
            for (int fileNum = 0; fileNum < count; fileNum++) {
                _files.Add(new MediaListFile(_handle, fileNum, cacheInform, allInfoCache));
            }
        }

        #endregion

        #region P/Invoke C Functions x86

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_Open(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName, IntPtr options);

        [DllImport("x86/MediaInfo.dll")]
        private static extern IntPtr MediaInfoList_New();

        [DllImport("x86/MediaInfo.dll")]
        private static extern void MediaInfoList_Delete(IntPtr handle);

        [DllImport("x86/MediaInfo.dll")]
        private static extern void MediaInfoList_Close(IntPtr handle, IntPtr filePos);

        #endregion

        #region P/Invoke C Functions x64

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Open")]
        private static extern IntPtr MediaInfoList_Open_x64(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName, IntPtr options);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_New")]
        private static extern IntPtr MediaInfoList_New_x64();

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Delete")]
        private static extern void MediaInfoList_Delete_x64(IntPtr handle);

        [DllImport("x64/MediaInfo.dll", EntryPoint = "MediaInfoList_Close")]
        private static extern void MediaInfoList_Close_x64(IntPtr handle, IntPtr filePos);

        #endregion

        #region Open Files

        /// <summary>Add the file to the list and collects information about it (technical information and tags). The Inform() call can be optionaly cached to reduce further calls to the DLL.</summary>
        /// <param name="fileName">Full name of the file to open.</param>
        /// <param name="cacheInform">If set to <c>true</c> caches the Inform() call.</param>
        /// <param name="allInfoCache">if set to <c>true</c> ShowAllInfo ("Complete_Get") is set to <c>true</c> before the Inform() call and reset afterwards.</param>
        /// <returns>Returns <c>true</c> if sucessfull, otherwise <c>false</c></returns>
        public MediaListFile Add(string fileName, bool cacheInform, bool allInfoCache) {
            if (Open(fileName) > 0) {
                MediaListFile file = new MediaListFile(_handle, _files.Count, cacheInform, allInfoCache);
                _files.Add(file);
                return file;
            }
            return null;
        }
        
        #endregion

        #region Close / Remove

        public void Remove(int filePos) {
            _files.Close(filePos);
        }

        public bool Remove(string filePath) {
            if (_files.Contains(filePath)) {
                _files.Close(_files[filePath].FileIndex);
                return true;
            }
            return false;
        }

        public bool Remove(MediaListFile file) {
            int idx = _files.IndexOf(file);

            if (idx != -1) {
                _files.Close(idx);
                return true;
            }
            return false;
        }
        
        public void RemoveAll() {
            _files.Clear();

            const int ALL_FILES = -1;

            if (Environment.Is64BitProcess) {
                MediaInfoList_Close_x64(_handle, (IntPtr) ALL_FILES);
            }
            else {
                MediaInfoList_Close(_handle, (IntPtr) ALL_FILES);
            }
        }
        #endregion

        #region Getters

        /// <summary>Gets the full paths of the files in list in order they've been added.</summary>
        /// <returns>The full paths of the files in list in order they've been added.</returns>
        public string[] GetFilePaths() {
            return _files.GetFilePaths().ToArray();
        }

        /// <summary>
        /// Gets the file in the list with the specified path.
        /// If not found it tries to add it and returns <c>null</c> if not succesfull.
        /// </summary>
        /// <param name="fileName">Name of the file to look for.</param>
        /// <param name="cacheInform">
        /// Used if the file was not found in the list.
        /// If set to <c>true</c> caches the Inform() call.
        /// </param>
        /// <param name="allInfoCache">
        /// Used if the file was not found in the list.
        /// If set to <c>true</c> ShowAllInfo ("Complete_Get") is set to <c>true</c> before the Inform() call and reset afterwards.
        /// </param>
        /// <returns></returns>
        public MediaListFile GetOrOpen(string fileName, bool cacheInform = true, bool allInfoCache = true) {
            MediaListFile mlf;
            if (_files.TryGetValue(fileName, out mlf)) {
                return mlf;
            }
            return Add(fileName, cacheInform, allInfoCache);
        }

        public MediaListFile GetFirstFileWithPattern(Regex regex) {
            return _files.GetFirstFileWithPattern(regex);
        }

        public MediaListFile GetFirstFileWithPattern(string regex) {
            return _files.GetFirstFileWithPattern(new Regex(regex));
        }
        
        public IEnumerable<MediaListFile> GetFilesWithPattern(Regex regex) {
            return _files.GetFilesWithPattern(regex);
        }

        public IEnumerable<MediaListFile> GetFilesWithPattern(string regex) {
            return _files.GetFilesWithPattern(new Regex(regex));
        }

        #endregion

        #region Contains / Count

        public int Count { get { return _files.Count; } }

        public bool Contains(string fullPath) {
            return _files.Contains(fullPath);
        }

        public bool Contains(MediaListFile file) {
            return _files.Contains(file);
        }

        #endregion

        #region Indexers

        public MediaListFile this[int filePos] {
            get { return _files[filePos]; }
        }

        public MediaListFile this[string fullPath] {
            get { return _files[fullPath]; }
        }

        #endregion

        #region MediaInfo List Interop

        /// <summary>Open one or more files and collect information about them (technical information and tags).</summary>
        /// <param name="pathNames">Full name of file(s) to open or Full name of folder(s) to open (if multiple names, names must be separated by "|").</param>
        /// <param name="options">FileOption_Recursive = Recursive mode for folders FileOption_Close = Close all already opened files before.</param>
        /// <returns>Number	of files successfuly added.</returns>
        private int Open(string pathNames, InfoFileOptions options = InfoFileOptions.Nothing) {
            if (Environment.Is64BitProcess) {
                return (int) MediaInfoList_Open_x64(_handle, pathNames, (IntPtr) options);
            }
            return (int) MediaInfoList_Open(_handle, pathNames, (IntPtr) options);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; private set; }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        void IDisposable.Dispose() {
            Dispose(false);
        }

        /// <summary>Closes all files in the list and disposes all allocated resources.</summary>
        public void Close() {
            Dispose(false);
        }

        private void Dispose(bool destructor) {
            if (!IsDisposed) {
                if (_files != null) {
                    RemoveAll();
                }

                if (!_noDll) {
                    if (Environment.Is64BitProcess) {
                        MediaInfoList_Delete_x64(_handle);
                    }
                    else {
                        MediaInfoList_Delete(_handle);
                    }
                }
                IsDisposed = true;

                if (destructor) {
                    GC.SuppressFinalize(this);
                }
            }
        }

        ~MediaInfoList() {
            Dispose(true);
        }

        #endregion

        #region IEnumerable

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<MediaListFile> GetEnumerator() {
            return _files.GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

    }

}