using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// represents one book and has two string properties, Author and Summary
    /// </summary>
    class Book : Media, IEncryptable
    {
        public string Author { get; set; }
        public string Summary { get; set; }

        /// <summary>
        /// the constructor holds the information for a book object, deriving from media
        /// </summary>
        /// <param name="title">title of the media object</param>
        /// <param name="year">year of the media object</param>
        /// <param name="author">director of the media object</param>
        /// <param name="summary">summary of the media object</param>
        public Book(string title, int year, string author, string summary) : base(title, year)
        {
            this.Author = author;
            this.Summary = summary;
        }

        /// <summary>
        /// calls the decrypt method
        /// </summary>
        /// <returns></returns>
        public string Encrypt()
        {
            return this.Decrypt();
        }

        /// <summary>
        /// this method returns the summary of the media object in a decrypted format.
        /// </summary>
        /// <returns>Decrypted object summary</returns>
        public string Decrypt()
        {
            if (string.IsNullOrEmpty(this.Summary)) return this.Summary;

            char[] buffer = new char[this.Summary.Length];

            for (int i = 0; i < this.Summary.Length; i++)
            {
                char c = this.Summary[i];
                if (c >= 97 && c <= 122)
                {
                    int j = c + 13;
                    if (j > 122) j -= 26;
                    buffer[i] = (char)j;
                }
                else if (c >= 65 && c <= 90)
                {
                    int j = c + 13;
                    if (j > 90) j -= 26;
                    buffer[i] = (char)j;
                }
                else
                {
                    buffer[i] = (char)c;
                }
            }
            return new string(buffer);
        }

        /// <summary>
        /// overrides to string to return the book information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Book Title: {0} ({1})\nAuthor: {2}\n", base.Title, base.Year, this.Author);
        }
    }
}
