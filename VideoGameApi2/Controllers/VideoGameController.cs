using Microsoft.AspNetCore.Mvc;
using VideoGameApi2;
using System.Collections.Generic;

namespace VideoGameApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame
            {
                Id = 1,
                Title = "The Legend of Zelda: Breath of the Wild",
                Platform = "Nintendo Switch",
                Developer = "Nintendo",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Id = 2,
                Title = "God of War Ragnarök",
                Platform = "PlayStation 5",
                Developer = "Santa Monica Studio",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame
            {
                Id = 3,
                Title = "Elden Ring",
                Platform = "PC",
                Developer = "FromSoftware",
                Publisher = "Bandai Namco Entertainment"
            }
        };

        [HttpGet] // Get all games

        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
        } 

        // get a specific game by id
        [HttpGet] // [HttpGet("{id}")]
        [Route("{id}")] // HttpGet with a specific id 
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
           var game = videoGames.FirstOrDefault(g => g.Id == id);
            if(game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost] // add a new game to the list
        public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest("Video game cannot be null.");

            newGame.Id = videoGames.Max(g => g.Id) + 1;
            videoGames.Add(newGame);

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")] // update a game by id
        public ActionResult<VideoGame> UpdateVideoGame(int id, VideoGame updatedGame)
        {
            var game =  videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Developer = updatedGame.Developer;
            game.Publisher = updatedGame.Publisher;

            return NoContent();
        }


        [HttpDelete("{id}")] // delete a game by id
        public ActionResult DeleteVideoGame(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();
            videoGames.Remove(game);
            return NoContent();
        }


    }
}
