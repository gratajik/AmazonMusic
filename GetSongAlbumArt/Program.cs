using System;

using AmazonMusicAPI;
namespace GetSongAlbumArt
{
    // 
    // Simple Wrapper around the AmazonMusicAPI
    //
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Usage: GetSongAlbumArt AWS_KEY AWS_SECRET ASSOICATE_ID \"BAND_NAME\" \"SONG_NAME\"");
                return;
            }

            var awsKey = args[0];
            var awsSecret = args[1];
            var awsAssoicateTag = args[2];
            var artist = args[3];
            var songName = args[4];

            var song = AmazonMusic.GetSongArt(awsKey, awsSecret, awsAssoicateTag, artist, songName);
            if (song != null)
            {
                Console.WriteLine($"Result for {artist} - {songName} ");
                Console.WriteLine(song.albumnName);
                Console.WriteLine(song.trackName);
                Console.WriteLine(song.LargeImage);
            }
            else
            {
                Console.WriteLine($"Unable to find {artist} - {songName} ");
            }

        }
    }
}

