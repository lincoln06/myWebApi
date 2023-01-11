namespace MyFirstWebApi.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public int ReleaseYear { get; set; }
        public Song(int id, string title, string album, int releaseYear)
        {
            Id = id;
            Title = title;
            Album = album;
            ReleaseYear = releaseYear;
        }
    }
}
