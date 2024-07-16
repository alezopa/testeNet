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
    public class PedidosController : Controller
    {

        private readonly IPedidosRepository repository;
        public PedidosController(IPedidosRepository _context)
        {
            repository = _context;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "PedidossController ::  Acessado em  : " + DateTime.Now.ToLongDateString();
        }

        [AllowAnonymous]
        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<Pedidos>>> GetPedidos()
        {
            var pedidos = await repository.GetAll();

            if (pedidos == null)
            {
                return BadRequest("Pedidos é null");
            }

            return Ok(pedidos.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedidos>> GetPedidos(int id)
        {
            var pedido = await repository.GetById(id);

            if (pedido == null)
            {
                return NotFound("Pedido não encontrado pelo id informado");
            }

            return Ok(pedido);
        }

        [AllowAnonymous]
        // POST api/<controller>  
        [HttpPost]
        public async Task<IActionResult> PostPedido([FromBody]Pedidos pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Pedido é null");
            }
            
            await repository.Insert(pedido);

            return CreatedAtAction(nameof(GetPedidos), new { Id = pedido.PedidoId }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedidos pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest($"O código do pedido {id} não confere");
            }

            try
            {
                await repository.Update(id, pedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok("Atualização do pedido realizada com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedidos>> DeletePedido(int id)
        {
            var pedido = await repository.GetById(id);
            if (pedido == null)
            {
                return NotFound($"Pedido  {id} não foi não encontrado");
            }

            await repository.Delete(id);

            return Ok(pedido);
        }
    }
}
