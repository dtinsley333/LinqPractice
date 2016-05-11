using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BasicEntityFrameworkDataAccess.Models;


namespace BasicEntityFrameworkDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
           // Database.SetInitializer<ChinookContext>(null);

            ChinookContext dbContext = new ChinookContext();
            //TODO: Refactor-these queries need to be separated out into their own class or classes

            //bring back searched for Artist
            var artistSearch = "Sabbath";
            var artists = dbContext.Artist.Where(a => a.Name.Contains(artistSearch));
            Console.WriteLine(Environment.NewLine);
            Console.Write("ARTIST WITH " + artistSearch.ToUpper() + " IN NAME");
            Console.WriteLine(Environment.NewLine);
            foreach (var artist in artists)
            {
                Console.Write(artist.Name);
            }

            //bring back 100 artist order by name
            var first100Artist = dbContext.Artist.Take(100).OrderBy(a => a.Name);
            Console.WriteLine(Environment.NewLine);
            Console.Write("First 100 ARTIST ORDERED BY NAME ");
            Console.WriteLine(Environment.NewLine);
            var count = 0;
            foreach (var artist in first100Artist)
            {
                count++;
                Console.Write(count.ToString() + "  " + artist.Name + Environment.NewLine);
            }

            //bring back 100 artist order by name
            var isThereATVShowGenre = dbContext.Genre.Where(a => a.Name.Contains("TV"));
            Console.WriteLine(Environment.NewLine);
            Console.Write("Is there a TV Show Genre ");
            Console.WriteLine(Environment.NewLine);

            var result = isThereATVShowGenre != null ? "Yes" : "No";
            Console.WriteLine(result);

            //get my favorite artist and info
            var favoriteArtistAlbums = (from artist in dbContext.Artist
                                        join album in dbContext.Album
                                        on artist.ArtistId equals album.ArtistId
                                        where artist.ArtistId == 152
                                        orderby artist.Name
                                        select new
                                        {
                                            Artist = artist.Name,
                                            Album = album.Title,
                                        }).ToList();


            Console.WriteLine(Environment.NewLine);
            Console.Write("ALBUM LISTING FOR " + favoriteArtistAlbums.Select(a => a.Artist).FirstOrDefault());
            Console.WriteLine(Environment.NewLine);
            foreach (var album in favoriteArtistAlbums)
            {
                Console.Write(album.Album + Environment.NewLine);
            }


            ///Get the total bill and mailing address for for the following customers with an id of (10, 38, 57)
            List<int> searchedCustomer = new List<int>();
            searchedCustomer.Add(10);
            searchedCustomer.Add(38);
            searchedCustomer.Add(57);

            var customerTotals = (from customer in dbContext.Customer
                                  join invoice in dbContext.Invoice
                                  on customer.CustomerId equals invoice.CustomerId
                                  where searchedCustomer.Contains(customer.CustomerId)
                                  orderby invoice.Total descending
                                  select new
                                  {
                                      CustomerId = customer.CustomerId,
                                      CustomerName = customer.FirstName +" " + customer.LastName,
                                      CustomerAddress = customer.Address,
                                      CustomerTotal = invoice.Total
                                  }).ToList();



            Console.WriteLine(Environment.NewLine);
            Console.Write("SELECTED CUSTOMER TOTALS ORDERED BY TOTAL");
            Console.WriteLine(Environment.NewLine);
            foreach (var customer in customerTotals)
            {
                Console.Write(customer.CustomerName+" " + customer.CustomerTotal.ToString() + Environment.NewLine);
            }

            ///set up my 3 dream concerts
            List<DreamConcert> dreamConcerts = new List<DreamConcert>();

            DreamConcert bcWilly = new DreamConcert
            {
                Artist = "Box Car Willy",
                Venue = "Ryman",
                City = "Nashville"
            };

            DreamConcert dollyParton = new DreamConcert
            {
                Artist = "Dolly Parton",
                Venue = "Bridgestone Arena",
                City = "Nashville"
            };

            DreamConcert dokken = new DreamConcert
            {
                Artist = "Dokken",
                Venue = "Bridgestone Arena",
                City = "Nashville"
            };
            dreamConcerts.Add(bcWilly);
            dreamConcerts.Add(dollyParton);
            dreamConcerts.Add(dokken);

            var concertList = dreamConcerts.Where(a => a.Venue == "Bridgestone Arena");
            Console.WriteLine(Environment.NewLine);
            Console.Write("DREAM CONCERTS AT BRIDGESTONE");
            Console.WriteLine(Environment.NewLine);
            foreach (var concert in concertList)
            {
                Console.Write(concert.Artist + " " + concert.Venue + Environment.NewLine);
            }

            ////COMPLETE THESE Queries in Linq. You will need to modify your connection string to point to your database

            //1  Bring back 100 artist and order by name
            //2. Is there a genre for TV show?
            //3. List the artist on a particular album you like(hint, will need to create a new model and set up in Chinook context)
            //4. List all of the albums by your favorite artist.
            //5. List the total bill and mailing address for the following customers with an id of (10, 38, 57)
            //6. Create a class that will hold information regarding concerts you would like to attend. Create a list containing
            //your concerts of choice. Set up several properties. Query your favorite concert list. Something like List<AwesomeConcert> ...

            Console.ReadLine();

        }
       
       
    }
   
}
