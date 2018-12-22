using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowRssFeeds();
        }

        static void ShowRssFeeds()
        {
            string[] lines = File.ReadAllLines("./input.txt");

            foreach(string url in lines)
            {
                var feedUrl = GetFeedUrl(url);

                if (feedUrl == null)
                    continue;

                var feed = FeedReader.Read(feedUrl);

                Console.WriteLine("Feed Title: " + feed.Title);
                Console.WriteLine("Feed Description: " + feed.Description);
                Console.WriteLine("Feed Image: " + feed.ImageUrl);
                // ...
                foreach (var item in feed.Items)
                {
                    Console.WriteLine("* [{0}]({1})", item.Title, item.Link);
                }
            }

            Console.ReadLine();
            
        }

        static string GetFeedUrl(string url)
        {
            var urls = FeedReader.GetFeedUrlsFromUrl(url);

            string feedUrl = null;
            if (urls.Count() < 1) // no url - probably the url is already the right feed url
                feedUrl = url;
            else if (urls.Count() == 1)
                feedUrl = urls.First().Url;
            else if (urls.Count() == 2) // if 2 urls, then its usually a feed and a comments feed, so take the first per default
                feedUrl = urls.First().Url;
            else
            {
                // show all urls and let the user select (or take the first or ...)
                // ...
            }

            return feedUrl;
        }
    }
}
