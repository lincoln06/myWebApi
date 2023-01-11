using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using MyFirstWebApi.Interfaces;
using MyFirstWebApi.Models;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Xml.Linq;

namespace MyFirstWebApi.Crud
{
    public class SQLiteCrud : ISQLiteCrud
    {
        private readonly static SqliteConnection _connection = new("Data Source=Data/Media.db");
        private SqliteCommand _command = _connection.CreateCommand();
        public void AddNewSong(Song newSong)
        {
            _connection.Open();
            _command.CommandText = 
                @"INSERT INTO Songs
                VALUES($id,$title,$album,$releaseYear)
                ";
            _command.Parameters.AddWithValue("$id", newSong.Id);
            _command.Parameters.AddWithValue("$title", newSong.Title);
            _command.Parameters.AddWithValue("$album", newSong.Album);
            _command.Parameters.AddWithValue("$releaseYear", newSong.ReleaseYear);
            _command.ExecuteNonQuery();
            _connection.Close();
        }

        public bool DeleteSongById(int id)
        {
            var songToDelete = GetSongById(id);
            if (songToDelete is null) return false;
            _connection.Open();
            _command.CommandText = @"DELETE FROM Songs WHERE Id=$id";
            _command.Parameters.AddWithValue("$id", id);
            _command.ExecuteNonQuery();
            _connection.Close();
            return true;
        }

        public IEnumerable<Song> GetAllSongs()
        {
            List<Song> listOfSongs = new List<Song>();
            _connection.Open();
            _command.CommandText =
                @"SELECT * FROM Songs";
            _command.ExecuteNonQuery();
            var reader = _command.ExecuteReader();
            while (reader.Read()) { 
            listOfSongs.Add(new Song(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetInt32(3)));
            }
            _connection.Close();
            return listOfSongs;
        }

        public object GetSongById(int id)
        {
            _connection.Open();
            _command.CommandText = 
                @"SELECT * FROM Songs WHERE Id=$id";
            _command.Parameters.AddWithValue("$id", id);
            _command.ExecuteNonQuery();
            var reader = _command.ExecuteReader();
            reader.Read();
            _connection.Close();
            if (!reader.HasRows) return null;
            return reader.HasRows? new Song(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)) : null;
        }

        public void UpdateSong(Song song)
        {
            var songToReplace = GetSongById(song.Id);
            if (songToReplace is null)
            {
                AddNewSong(song);
                return;
            }
            Song oldSong = songToReplace as Song;
            _connection.Open();
            _command.CommandText =
                @"UPDATE Songs
                SET Title=$newTitle,
                Album=$newAlbum,
                ReleaseYear=$newReleaseYear
                WHERE id=$id";
            _command.Parameters.AddWithValue("$newTitle", song.Title);
            _command.Parameters.AddWithValue("$newAlbum", song.Album);
            _command.Parameters.AddWithValue("$newReleaseYear", song.ReleaseYear);
            _command.Parameters.AddWithValue("$id", oldSong.Id);
        }
    }
}