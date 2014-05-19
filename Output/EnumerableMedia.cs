using System.Collections;
using System.Collections.Generic;

namespace Frost.SharpMediaInfo.Output {

    public class EnumerableMedia<T> : Media, IEnumerable<T> where T : Media {

        private int _streamNum = -1;

        /// <summary>Initializes a new instance of the <see cref="EnumerableMedia{T}"/> class.</summary>
        /// <param name="mediaInfo">The <see cref="Frost.SharpMediaInfo.MediaFile">MediaFile</see> instance from which to get the information from..</param>
        /// <param name="streamKind">The kind of this stream.</param>
        public EnumerableMedia(MediaFileBase mediaInfo, StreamKind streamKind) : base(mediaInfo, streamKind) {
        }

        #region IEnumerable

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1">IEnumerator&lt;out T&gt;</see> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() {
            //cache the currently selected StreamNumber
            //so we can restore it after the enumeration
            if (_streamNum == -1) {
                _streamNum = StreamNumber;
            }

            int numStreams = Count;
            for (int i = 0; i < numStreams; i++) {
                StreamNumber = i;
                yield return this as T;
            }
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="System.Collections.IEnumerator">IEnumerator</see>> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion         

    }

}