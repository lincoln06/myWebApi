using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstWebApi.Interfaces;
using MyFirstWebApi.Models;

namespace MyFirstWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ILogger<SongController> _logger;
        //private readonly ISongService _sqliteCrud;
        private readonly ISQLiteCrud _sqliteCrud;

        public SongController(ILogger<SongController> logger, ISQLiteCrud sqliteCrud)
        {
            _logger = logger;
            _sqliteCrud = sqliteCrud;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Song>> Get()
        {
            IEnumerable<Song> result = _sqliteCrud.GetAllSongs();
            if (result.Any())
              return  Ok(result);
            return NotFound();
        }
        [HttpGet("GetSongById")]
        public ActionResult<Song> GetSongById([FromQuery]int id)
        {
            var result=_sqliteCrud.GetSongById(id);
            if (result is null) 
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Post([FromBody] Song newSong)
        {
            _sqliteCrud.AddNewSong(newSong);
            return Created("",newSong);
        }
        [HttpDelete]
        public ActionResult<Song> DeleteSongById([FromQuery]int id)
        {
            bool result = _sqliteCrud.DeleteSongById(id);
            if (!result)
                return NotFound();
            return Ok();
        }
        [HttpPut]
        public ActionResult UpdateSong([FromBody]Song songToUpdate)
        {
            _sqliteCrud.UpdateSong(songToUpdate);
            return Ok();
        }
    }
}
