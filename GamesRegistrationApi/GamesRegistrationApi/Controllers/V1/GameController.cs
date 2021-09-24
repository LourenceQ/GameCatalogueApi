using GamesRegistrationApi.Exceptions;
using GamesRegistrationApi.InputModel;
using GamesRegistrationApi.Services;
using GamesRegistrationApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }


        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetById([FromRoute] Guid idGame)
        {
            var game = await _gameService.GetById(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }


        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch (GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }


        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }


        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }


        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}
