/*  Copyright (c) MediaArea.net SARL. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license that can
 *  be found in the License.html file in the root of the source tree.
 */

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
// Microsoft Visual C# wrapper for MediaInfo Library
// See MediaInfo.h for help
//
// To make it working, you must put MediaInfo.Dll
// in the executable folder
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



#pragma warning disable 1591 // Disable XML documentation warnings

namespace Frost.SharpMediaInfo {

    public enum StreamKind {
        General,
        Video,
        Audio,
        Text,
        Other,
        Image,
        Menu,
    }

    public enum InfoKind {
        /// <summary>Unique name of parameter.</summary>
        Name,
        /// <summary>Value of parameter.</summary>
        Text,
        /// <summary>Unique name of measure unit of parameter.</summary>
        Measure,
        /// <summary>See <see cref="InfoOptions"/>.</summary>
        Options,
        /// <summary>Translated name of parameter.</summary>
        NameText,
        /// <summary>Translated name of measure unit.</summary>
        MeasureText,
        /// <summary>More information about the parameter.</summary>
        Info,
        /// <summary>How this parameter is supported, could be N (No), B (Beta), R (Read only), W (Read/Write)</summary>
        HowTo,
        /// <summary>Domain of this piece of information.</summary>
        Domain
    }

    public enum InfoOptions {
        /// <summary>Show this parameter in Inform()</summary>
        ShowInInform,
        Support,
        /// <summary>Internal use only (info : Must be showed in Info_Capacities() )</summary>
        ShowInSupported,
        /// <summary>Value return by a standard Get() can be : T (Text), I (Integer, warning up to 64 bits), F (Float), D (Date), B (Binary datas coded Base64) (Numbers are in Base 10).</summary>
        TypeOfValue
    }

    public enum InfoFileOptions {
        /// <summary>No option.</summary>
        Nothing = 0x00,
        /// <summary>Do not browse folders recursively.</summary>
        NoRecursive = 0x01,

        /// <summary>Close all open files before new Open() call.</summary>
        CloseAll = 0x02,
        Max = 0x04
    };

    public enum BlockMethod {
        Immediately,
        AfterLocalInfo
    }

    public enum Demux {
        All,
        Frame,
        Container,
        Elementary
    }

    public enum TraceFormat {
        CSV,
        Tree
    }

    public enum InformPreset {
        Text,
        HTML,
        XML,
        PBCore,
        ReVTMD,
        Mpeg7
    }

    /// <summary>The way of displaying/drawing video frames on the screen.</summary>
    public enum MediaScanType : long {
        /// <summary>An Unknown scan type.</summary>
        Unknown,

        /// <summary>The intelaced scan type. Has two fields (odd and even rows). Each frame only one is shown (they are alternating).</summary>
        /// <remarks>Has to be deinterlaced on non CRT/Plasma screens.</remarks>
        Interlaced,

        /// <summary>The progressive (noninterlaced) scan type. All rows of the drawn for each frame.</summary>
        /// <remarks>Supported on all screens.</remarks>
        Progressive,
        MBAFF,
        Mixed
    }

    /// <summary>The bitrate mode that was used when ecoding.</summary>
    public enum FrameOrBitRateMode : long {

        /// <summary>The bitrate mode is unknown.</summary>
        Unknown,

        /// <summary>The constant/static bit rate.</summary>
        Constant,

        /// <summary>The bitrate that has more bits for complex segments and less for less complex ones. Bitrate therefore varies (is not constant).</summary>
        Variable

    }

    /// <summary>The compression mode used.</summary>
    public enum CompressionMode : long {
        /// <summary>An unknown compression mode.</summary>
        Unknown,
        /// <summary>The compression mode that allows perfect reconsturction to an original.</summary>
        Lossless,
        /// <summary>The compression mode that only aproximates the original and the compression can't be reverted.</summary>
        Lossy
    }

    public enum AudioAlignment {
        Unknown,
        Split,
        Aligned
    }

    //public enum ShowFiles {
    //    Nothing,
    //    VideoAudio,
    //    VideoOnly,
    //    AudioOnly,
    //    TextOnly
    //}
}

//NameSpace
