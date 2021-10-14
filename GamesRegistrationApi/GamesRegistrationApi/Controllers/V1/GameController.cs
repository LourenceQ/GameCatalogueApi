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


        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possivel retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page"> indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Buscar jogo pelo seu Id
        /// </summary>
        /// <param name="idGame">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>
        /// <returns></returns>
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
