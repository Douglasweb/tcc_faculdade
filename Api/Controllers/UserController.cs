using ApiCQRS.Application.Commands;
using ApiCQRS.Application.Cripto;
using ApiCQRS.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("{id}/verifypassword/{password}")]
        public async Task<IActionResult> GetById(Guid id, string password)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });

            if (result == null)
                return NotFound();

            var hashing = new HashingManager();

            var h = hashing.HashToString(password);

            var hashpass = hashing.Verify(password, result.UserPassword);

            return Ok(hashpass);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserByIdCommand { Id = id });
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            if (result == Guid.Empty)
            {
                return BadRequest("Usuário não encontrado");
            }

            return Ok(result);
        }

        [HttpGet("hot/{id}")]
        public async Task<IActionResult> GetByHOTId(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdHOTQuery { Id = id });
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

    }
}
