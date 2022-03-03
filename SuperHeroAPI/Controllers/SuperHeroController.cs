using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {         
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            // return Ok(heroes);
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            // var hero = heroes.Find(x => x.Id == id);
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return NotFound("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            // heroes.Add(hero);

            context.SuperHeroes.Add(hero);      // change something in the table
            await context.SaveChangesAsync();  // then save the changes in the table

            //return Ok(heroes);

            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            //var hero = heroes.Find(x => x.Id == request.Id);
            var dbHero = await context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return NotFound("Hero not found.");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await context.SaveChangesAsync();

            //return Ok(heroes);
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            // var hero = heroes.Find(x => x.Id == id);
            var dbHero = await context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return NotFound("Hero not found.");
            
            // heroes.Remove(hero);
            context.SuperHeroes.Remove(dbHero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }
    }
}
