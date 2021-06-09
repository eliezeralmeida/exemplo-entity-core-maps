using System;
using System.Threading.Tasks;
using ExemplosEntity.OneToMany;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put()
        {
            var desenvolvedor = await _dbContext.Desenvolvedores.FirstAsync();
            var projeto = await _dbContext.Projetos.FirstAsync();

            desenvolvedor.Projeto = null;
            _dbContext.Entry(desenvolvedor).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            desenvolvedor.Projeto = projeto;
            _dbContext.Entry(desenvolvedor).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return Ok(desenvolvedor);
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var desenvovledores = await _dbContext.Desenvolvedores
                .AsNoTrackingWithIdentityResolution()
                .Include(d => d.Projeto)
                .ToListAsync();

            return Ok(desenvovledores);
        }
    }
}