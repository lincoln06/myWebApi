using Microsoft.AspNetCore.Mvc;
using MyFirstWebApi.Models;
using System.Collections.Generic;

namespace MyFirstWebApi.Interfaces
{
    public interface ISQLiteCrud
    {
        void AddNewSong(Song newSong);
        bool DeleteSongById(int id);
        IEnumerable<Song> GetAllSongs();
        object GetSongById(int id);
        void UpdateSong(Song songToUpdate);
    }
}