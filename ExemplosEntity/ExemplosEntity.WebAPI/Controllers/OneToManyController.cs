using System;
using System.Threading.Tasks;
using ExemplosEntity.OneToMany;
using Microsoft.AspNetCore.Mvc;

namespace ExemplosEntity.WebAPI.Controllers
{
    [ApiController]
    [Route("api/one-to-many")]
    public class OneToManyController : ControllerBase
    {
        private readonly OneToManyContext _dbContext;

        public OneToManyController(OneToManyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post()
        {
            var randomInt = new Random().Next();

            var desenvolvedor = new Desenvolvedor()
            {
                Nome = $"Pedro {randomInt}",
                Projeto = new Projeto() {Nome = $"Projeto do Pedro {randomInt}"}
            };

            await _dbContext.Desenvolvedores.AddAsync(desenvolvedor);
            await _dbContext.SaveChangesAsync();

            return Ok(desenvolvedor);
        }
    }
}