using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AmazonMusicAPI
{
    public class Song
    {
        public Song(XmlDocument doc, XmlNamespaceManager ns, XmlNode item, string trackName)
        {            
            ASIN = item.SelectSingleNode("a:ASIN", ns).InnerText;
            DetailPageURL = item.SelectSingleNode("a:DetailPageURL", ns).InnerText;
            MediumImage = item.SelectSingleNode("a:MediumImage/a:URL", ns).InnerText;
            LargeImage = item.SelectSingleNode("a:LargeImage/a:URL", ns).InnerText;
            albumnName = item.SelectSingleNode("a:ItemAttributes/a:Title", ns).InnerText;
            this.trackName = trackName;
        }

        public string ASIN { get; set; }
        public string DetailPageURL { get; set; }
        public string MediumImage { get; set; }
        public string LargeImage { get; set; }
        public string albumnName { get; set; }
        public string trackName { get; set; }
    }
}
