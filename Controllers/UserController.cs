using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using User_service.Facades;
using User_service.Models;
using User_service.Transformers;

namespace User_service.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private UserFacade userFacade;

        public UserController(ILogger<UserController> logger, UserFacade userFacade)
        {
            this.logger = logger;
            this.userFacade = userFacade;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserJsonModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers()
        {
            logger.LogInformation("The retrieval of a list of users requested");

            try
            {
                return Ok(
                    UserMapper.MapDbToJsonList(
                        userFacade.GetUsers()));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when retrieving list of users", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("/users/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserJsonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            logger.LogInformation($"The retrieval of a user is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    UserMapper.MapDbToJson(
                        userFacade.GetUser(id)));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when retrieving a user", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserJsonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser([FromBody] UserJsonModel user)
        {
            logger.LogInformation("The creation of a user is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    UserMapper.MapDbToJson(
                        userFacade.CreateUser(
                            UserMapper.MapJsonToDb(user))));
            }
            catch (Exception e)
            {
                logger.LogError("error occured when creating a user", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("/users/{id}")]
        [ProducesResponseType(typeof(UserJsonModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            logger.LogInformation($"The deletion of a user is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                userFacade.DeleteUser(id);
                return NoContent();
                        
            }
            catch (Exception e)
            {
                logger.LogError("error occured when deleting a user", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("/users/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserJsonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser([FromBody] UserJsonModel user)
        {
            logger.LogInformation($"The update of a user is requested");

            if (!ModelState.IsValid) return BadRequest();

            try
            {
                return Ok(
                    UserMapper.MapDbToJson(
                        userFacade.UpdateUser(
                            UserMapper.MapJsonToDb(user))));

            }
            catch (Exception e)
            {
                logger.LogError("error occured when updating a user", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
