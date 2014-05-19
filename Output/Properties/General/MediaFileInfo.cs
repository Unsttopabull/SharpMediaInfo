using System;

namespace Frost.SharpMediaInfo.Output.Properties.General {
    public class MediaFileInfo {
        private readonly Media _media;

        public MediaFileInfo(Media media) {
            FileSizeInfo = new FileSizeInfo(media);
            _media = media;
        }

        /// <summary>The time that the file was created on the file system</summary>
        public DateTime? CreatedDate { get { return _media.TryParseDateTime("File_Created_Date", true); } }
        /// <summary>The time that the file was created on the file system (Warning: this field depends of local configuration, do not use it in an international database)</summary>
        public DateTime? CreatedDateLocal { get { return _media.TryParseDateTime("File_Created_Date_Local", false); } }

        /// <summary>The time that the file was modified on the file system</summary>
        public DateTime? ModifiedDate { get { return _media.TryParseDateTime("File_Modified_Date", true); } }
        /// <summary>The time that the file was modified on the file system (Warning: this field depends of local configuration, do not use it in an international database)</summary>
        public DateTime? ModifiedDateLocal { get { return _media.TryParseDateTime("File_Modified_Date_Local", false); } }

        /// <summary>File size in bytes</summary>
        public long? FileSize { get { return _media.TryParseLong("FileSize"); } }
        /// <summary>Gets the file size information.</summary>
        public FileSizeInfo FileSizeInfo { get; private set; }

        /// <summary>Complete name (Folder+Name+Extension)</summary>
        public string FullPath { get { return _media["CompleteName"]; } }
        /// <summary>Complete name (Folder+Name+Extension) of the last file (in the case of a sequence of files)</summary>
        public string FullPathLast { get { return _media["CompleteName_Last"]; } }

        /// <summary>Folder name only</summary>
        public string FolderPath { get { return _media["FolderName"]; } }
        /// <summary>Folder name only of the last file (in the case of a sequence of files)</summary>
        public string FolderPathLast { get { return _media["FolderName_Last"]; } }

        /// <summary>File name without extension</summary>
        public string FileName { get { return _media["FileName"]; } }
        /// <summary>File name without extension of the last file (in the case of a sequence of files)</summary>
        public string FileNameLast { get { return _media["FileName_Last"]; } }

        /// <summary>File extension only</summary>
        public string Extension { get { return _media["FileExtension"]; } }
        /// <summary>File extension only of the last file (in the case of a sequence of files)</summary>
        public string ExtensionLast { get { return _media["FileExtension_Last"]; } }
    }
}