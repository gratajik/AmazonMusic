using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

using AmazonProductAdvtApi;
namespace AmazonMusicAPI
{
    public static class AmazonMusic
    {
        private const string DESTINATION = "ecs.amazonaws.com";
        private const string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-011";

        public static Song GetSongArt(string awsKey, string awsSecret, string awsAssoicateTag, string artist, string songName)
        {
            var signedHelper = new SignedRequestHelper(awsKey, awsSecret, DESTINATION);
            var request = new Dictionary<string, String>
            {
                {"Service", "AWSECommerceService" },
                {"Version", "2009-03-31"},
                {"Operation", "ItemSearch"},
                {"ResponseGroup", "Images,ItemAttributes,Tracks"},
                {"Sort", "salesrank"},
                {"SearchIndex", "Music"},
                {"AssociateTag", awsAssoicateTag},
                {"Artist", artist}
             };

            var requestUrl = signedHelper.Sign(request);
            var song = GetAmazonSong(requestUrl, songName);
            return song;
        }

        private static Song GetAmazonSong(string url, string sSongName)
        {
            try
            {
                var sUpperSongName = sSongName.ToUpper();
                var request = HttpWebRequest.Create(url);
                var response = request.GetResponse();
                var doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                var errorMessageNodes = doc.GetElementsByTagName("Message");
                if (errorMessageNodes != null && errorMessageNodes.Count > 0)
                {
                    var message = errorMessageNodes.Item(0).InnerText;
                    Console.WriteLine("Error: " + message + " (but signature worked)");
                   
                    return null;
                }

                var ns = new XmlNamespaceManager(doc.NameTable);
                ns.AddNamespace("a", "http://webservices.amazon.com/AWSECommerceService/2011-08-01");

                var item = doc.GetElementsByTagName("Item");
                for (int i = 0; i < item.Count; i++)
                {
                    var tracks = item[i].SelectNodes("a:Tracks/a:Disc/a:Track", ns);

                    for (int j = 0; j < tracks.Count; j++)
                    {
                        var trackName = tracks[j].InnerText;
                        if (trackName.ToUpper() == sUpperSongName)
                        {
                            return new Song(doc, ns, item[i], trackName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception: " + e.Message);
                Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

            return null;
        }
    }
}

