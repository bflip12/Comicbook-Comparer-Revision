using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// (represents one song and has two string properties, Album and Artist deriving from media
    /// </summary>
    class Song : Media
    {
        public string Album { get; set; }
        public string Artist { get; set; }

        /// <summary>
        /// the constructor holds the information for a song object, deriving from media
        /// </summary>
        /// <param name="title">title of the media object</param>
        /// <param name="year">year of the media object</param>
        /// <param name="director">director of the media object</param>
        /// <param name="summary">summary of the media object</param>
        public Song(string title, int year, string album, string artist) : base(title, year)
        {
            this.Album = album;
            this.Artist = artist;
        }
        /// <summary>
        /// overrides to string to return the song information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Song Title: {0} ({1})\nArtist: {2}\n", base.Title, base.Year, this.Artist);
        }
    }
}
