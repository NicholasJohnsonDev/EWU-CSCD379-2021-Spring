using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Api.Dto;
using System.Linq;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository Repository { get; }

        public UsersController(IUserRepository repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IEnumerable<UpdateUser> Get()
        {
            return Repository.List().Select(user => new UpdateUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UpdateUser), StatusCodes.Status200OK)]
        public ActionResult<UpdateUser?> Get(int id)
        {
            User? user = Repository.GetItem(id);
            if (user is null) return NotFound();
            return new UpdateUser()
            {
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Id = id
            };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            if (Repository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UpdateUser), StatusCodes.Status200OK)]
        public ActionResult<UpdateUser?> Post([FromBody] UpdateUser? UpdateUser)
        {
            if (UpdateUser is null)
            {
                return BadRequest();
            }
            int id;
            if (Repository.List().Count == 0)
            {
                id = 0;
            }
            else
            {
                id = (Repository.List().Select(item => item.Id).Max() + 1);
            }
            Repository.Create(new User()
            {
                FirstName = UpdateUser.FirstName ?? "",
                LastName = UpdateUser.LastName ?? "",
                Id = id
            });

            return UpdateUser;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put(int id, [FromBody] UpdateUser? user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            User? foundUser = Repository.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = user.FirstName ?? "";
                foundUser.LastName = user.LastName ?? "";

                Repository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}