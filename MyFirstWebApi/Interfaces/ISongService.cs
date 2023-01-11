using MyFirstWebApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace MyFirstWebApi.Interfaces
{
    public interface ISongService
    {
        void AddNewSong(Song song);
        bool DeleteSongById(int id);
        IEnumerable<Song> GetAllSongs();
        Song GetSongById(int id);
        void UpdateSong(Song songToUpdate);
    }
}
