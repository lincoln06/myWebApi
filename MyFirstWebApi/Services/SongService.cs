using MyFirstWebApi.Interfaces;
using MyFirstWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstWebApi.Services
{
    public class SongService : ISongService
    {
        private List<Song> _songs=new List<Song>
            {
                new Song{Id=1, Title="Tytul1", Album="Album1", ReleaseYear=2000},
                new Song{Id=2, Title="Tytul2", Album="Album2", ReleaseYear=2000},
                new Song { Id = 3, Title = "Tytul3", Album = "Album3", ReleaseYear = 2000 },
                new Song { Id = 4, Title = "Tytul4", Album = "Album4", ReleaseYear = 2000 },
                new Song { Id = 5, Title = "Tytul5", Album = "Album5", ReleaseYear = 2000 }
            };
        private object _songRepository;

        public void AddNewSong(Song newSong)
        {
            
            //udaję zapis do bazy danych
            _songs.Add(newSong);
        }

        public bool DeleteSongById(int id)
        {
            var songToDelete = GetSongById(id);
            if (songToDelete is null) return false;
            _songs.Remove(songToDelete);
            return true;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            return _songs;
        }

        public Song GetSongById(int id)
        {
            return _songs.FirstOrDefault(song => song.Id == id);
        }

        public void UpdateSong(Song song)
        {
            var songToReplace = GetSongById(song.Id);
            if (songToReplace is null)
            {
                AddNewSong(song);
                return;
            }
            var index = _songs.IndexOf(songToReplace);
            _songs[index] = song;
        }
    }
}
