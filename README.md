There's a few ways to get the album that a song is on, via the Amazon Product API.  I haven't been happy with any of them, 
primarily because, while they give a valid result, the "BEST" one isn't easy to find.

I wrote this simple API to try to get that best one.

I do this by:

  - Requesting albums for the artist, requesting a sort order of best sales rank
  - Iterate through the results, finding the first album that matches the song name
  - Return that album as the "best" result
  
The repo contains the the .net library to do this, AmazonMusicAPI, and a simple wrapper, GetSongAlbumArt.  This obvously can be extended a lot - it's exactly what I need, and is not meant to be a generic wrapper.
  
