using TesteNET.Models;
using TesteNET.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteNET.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosRepository repository;
        public UsuariosController(IUsuariosRepository _context)
        {
            repository = _context;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "UsuariosController ::  Acessado em  : " + DateTime.Now.ToLongDateString();
        }

        [AllowAnonymous]
        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await repository.GetAll();

            if (usuarios == null)
            {
                return BadRequest("Usuario é null");
            }

            return Ok(usuarios.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedidos>> GetUsuarios(int id)
        {
            var usuario = await repository.GetById(id);

            if (usuario == null)
            {
                return NotFound("Usuario não encontrado pelo id informado");
            }

            return Ok(usuario);
        }

        [AllowAnonymous]
        // POST api/<controller>  
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuario é null");
            }
            
            await repository.Insert(usuario);

            return CreatedAtAction(nameof(GetUsuarios), new { Id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest($"O código do usuario {id} não confere");
            }

            try
            {
                await repository.Update(id, usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok("Atualização do usuario realizada com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedidos>> DeleteUsuario(int id)
        {
            var usuario = await repository.GetById(id);
            if (usuario == null)
            {
                return NotFound($"Usuario  {id} não foi não encontrado");
            }

            await repository.Delete(id);

            return Ok(usuario);
        }
    }
}
