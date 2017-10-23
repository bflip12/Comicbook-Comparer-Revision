/*
  Bobby Filippopulos
  000338236
  I, Bobby Filippopoulos, 000338236 verify that this is my work and only my work
  The program reads a data text file, storing up to 100 searchable media objects, can decrypt a ROT13 summary and output each media objects information
  Date created: 3/9/2017
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// This class contains the methods that will test the functionality for media objects
    /// Main will test the methods and check for user input errors
    /// </summary>
    class Lab3
    {
        /// <summary>
        /// Implements the following methods that provide a menu to the user in which the user can select output functionality.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ISearchable[] mediaObjects = new ISearchable[100];
            int num;
            Read(out mediaObjects, out num);
            bool moreFunctions = true;

            while (moreFunctions) //If the user enters '7' the flag will be set to false and the program will quit.
            {

                string choice = menu(); //set sort selection to the return string from menu, request input and display menu
                int choiceParsed;
                if (Int32.TryParse(choice, out choiceParsed)) //convert the string to an integer, check for appropriate input
                {
                    if (choiceParsed == 1 || choiceParsed == 2 || choiceParsed == 3 || choiceParsed == 4 || choiceParsed == 5)
                    {
                        displayResults(mediaObjects, choiceParsed);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Invalid choice, please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid choice, please try again.");
                    Console.WriteLine("");
                }
                if (choiceParsed == 6)
                {
                    moreFunctions = false;
                }
            }
        }
        
        /// <summary>
        /// Depending on the user input, displayResults will write the objects information to the console.
        /// </summary>
        /// <param name="mediaObjects">mediaObjects is the ISearchable array of references to each media object</param>
        /// <param name="userInput">Input provided by the user</param>
        private static void displayResults(ISearchable[] mediaObjects, int userInput)
        {
            Console.Clear();
            string searchString = "";
            if (userInput == 5)
            {
                Console.WriteLine("Please enter the title that you would like to search");
                searchString = Console.ReadLine();
            }

            foreach (ISearchable mediaObject in mediaObjects)
            {
               
                if (userInput == 1 || userInput == 4)
                {
                    Book book = mediaObject as Book;
                    if (book != null)
                    {
                        Console.WriteLine(book.ToString());
                    }
                }

                if (userInput == 2 || userInput == 4)
                {
                    Movie movie = mediaObject as Movie;
                    if (movie != null)
                    {
                        Console.WriteLine(movie.ToString());
                    }
                }

                if (userInput == 3 || userInput == 4)
                {
                    Song song = mediaObject as Song;
                    if (song != null)
                    {
                        Console.WriteLine(song.ToString());
                    }
                }

                if (userInput == 5)
                {
                    Song song = mediaObject as Song;
                    Movie movie = mediaObject as Movie;
                    Book book = mediaObject as Book;
                    if (song != null && song.Search(searchString))
                    {
                        Console.WriteLine(song.ToString());
                    }
                    if (book != null && book.Search(searchString))
                    {
                        Console.WriteLine(book.ToString() + "\n" + book.Decrypt());
                    }
                    if (movie != null && movie.Search(searchString))
                    {
                        Console.WriteLine(movie.ToString() + "\n" + book.Decrypt());
                    }
                }
            }
        if (userInput == 6)
        {
            Environment.Exit(0);
        }
    }

        /// <summary>
        /// This method prompts the user to choose which parameter to sort by
        /// </summary>
        /// <returns>returns the users choice bound within 1-7</returns>
        private static string menu()
        {

            Console.WriteLine("1. List All Books");
            Console.WriteLine("2. List All Movies");
            Console.WriteLine("3. List All Songs");
            Console.WriteLine("4. List All Media");
            Console.WriteLine("5. Search All Media by Title");
            Console.WriteLine();
            Console.WriteLine("6.Exit Program");
            Console.WriteLine("");
            Console.Write("Enter choice: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// this read method goes through the text file line by line, over writing an array with each line
        /// after the array is set, a new comic will be created with the contents of the array
        /// </summary>
        /// <param name="comicBookCollection">sets the objects data</param>
        /// <param name="num">sets the size of the array, needed to display and sort</param>
        private static void Read(out ISearchable[] mediaObjects, out int num)
        {
            num = 0;
            mediaObjects = new Media[100];
            try
            {
                string filename = "Data.txt";
                string fullFileName = Path.Combine(Directory.GetCurrentDirectory(), filename); //this creates the absolute path of the file
                FileStream file = new FileStream(fullFileName, FileMode.Open);
                StreamReader data = new StreamReader(file);

                string mediaLine;
                
                while ((mediaLine = data.ReadLine()) != null) //read until the end of file
                {
                    string[] explodeLine = mediaLine.Split('|');
                    if (explodeLine[0].Equals("MOVIE"))
                    {
                        AddMovie(mediaObjects, num, explodeLine, data);
                        num++;
                    }
                    if (explodeLine[0].Equals("BOOK"))
                    {
                        AddBook(mediaObjects, num, explodeLine, data);
                        num++;
                    }
                    if (explodeLine[0].Equals("SONG"))
                    {
                        AddSong(mediaObjects, num, explodeLine);
                        num++;
                    }
                }
                data.Close();
                file.Close();
            }
            catch
            {
                Console.WriteLine("error");
            }
        }

        /// <summary>
        /// AddBook will set the object at index with the proper structure. Summary will be a string that contancatenates each line of the summary until "-----" is hit. 
        /// </summary>
        /// <param name="mediaObject">This is the array of ISearchable references</param>
        /// <param name="num">This is the number of the index</param>
        /// <param name="ExplodedArray">These are the values of the first line of each object(object description) seperated</param>
        /// <param name="data">contains the information for summary</param>
        private static void AddBook(ISearchable[] mediaObject, int num, String[] ExplodedArray, StreamReader data)
        {
            string summary = "";
            string line;
            do
            {
                line = data.ReadLine();
                if (line != "-----")
                {
                    summary += line;
                }
            } while (line != "-----");
            
            mediaObject[num] = new Book(ExplodedArray[1], Convert.ToInt32(ExplodedArray[2]), ExplodedArray[3], summary);
        }

        /// <summary>
        /// AddMovie will set the object at index with the proper structure. Summary will be a string that contancatenates each line of the summary until "-----" is hit. 
        /// </summary>
        /// <param name="mediaObject">This is the array of ISearchable references</param>
        /// <param name="num">This is the number of the index</param>
        /// <param name="ExplodedArray">These are the values of the first line of each object(object description) seperated</param>
        /// <param name="data">contains the information for summary</param>
        private static void AddMovie(ISearchable[] mediaObject, int num, String[] ExplodedArray, StreamReader data)
        {
            string summary = "";
            string line;
            do
            {
                line = data.ReadLine();
                if (line != "-----")
                {
                    summary += line;
                }
            } while (line != "-----");

            mediaObject[num] = new Movie(ExplodedArray[1], Convert.ToInt32(ExplodedArray[2]), ExplodedArray[3], summary);
        }
        /// <summary>
        /// AddBook will set the object at index with the proper structure. 
        /// </summary>
        /// <param name="mediaObject">This is the array of ISearchable references</param>
        /// <param name="num">This is the number of the index</param>
        /// <param name="ExplodedArray">These are the values of the first line of each object(object description) seperated</param>
        private static void AddSong(ISearchable[] mediaObject, int num,  String[] ExplodedArray)
        {
            mediaObject[num] = new Song(ExplodedArray[1], Convert.ToInt32(ExplodedArray[2]), ExplodedArray[3], ExplodedArray[4]);
        }





    }

}