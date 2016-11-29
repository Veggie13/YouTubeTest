using System;
using System.Collections.Generic;
using System.Text;
using Google.YouTube;

namespace YouTubeTest
{
    class Program
    {
        static DateTime PublishDate(Video v)
        {
            return v.YouTubeEntry.Published;
        }

        public static int ComparePublishDate(Video v1, Video v2)
        {
            return PublishDate(v1).CompareTo(PublishDate(v2));
        }

        static void Main(string[] args)
        {
            var request = new YouTubeRequest(
                new YouTubeRequestSettings(
                    "YouTubeTest",
                    "AI39si4diV_7ebufqI-_h1tCkD_8B36b8pqzFlNUQDKEO0hww8nLrqO7UjsOksWQvcUsEILnK9qJIn12cuQbgf4QhZauyw8s8g"
                    ));
            var feed = request.GetVideoFeed("CGPGrey");
            feed.AutoPaging = true;

            long total = 0;
            int nVids = 0;
            Video oldest = null;
            Video newest = null;
            foreach (var v in feed.Entries)
            {
                if (oldest == null || ComparePublishDate(v, oldest) < 0)
                    oldest = v;
                if (newest == null || ComparePublishDate(v, newest) > 0)
                    newest = v;
                nVids++;
                int dur = int.Parse(v.Media.Duration.Seconds);
                total += dur;
            }

            var span = PublishDate(newest).Subtract(PublishDate(oldest));
            double rate = (double)total * 365.25 / span.TotalDays / 3600;
        }
    }
}
