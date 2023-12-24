using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet7.Data;
using SuperHeroAPI_DotNet7.Models;
using SuperHeroAPI_DotNet7.Models.DTO;

namespace SuperHeroAPI_DotNet7.Services.SuperHeroServices
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly DatabaseContext _dbContext;
        public SuperHeroService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(SuperHero entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(SuperHero entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SuperHero>> Get()
        {
            var heroes = await _dbContext.SuperHeroes.AsNoTracking().ToListAsync();

            return heroes;
        }

        public async Task<SuperHero> GetById(int id)
        {
            var hero = await _dbContext.SuperHeroes.FirstOrDefaultAsync(x => x.Id == id);

            return hero;
        }

        public async Task<SuperHero> GetLast()
        {
            var hero = (await Get()).LastOrDefault();
            return hero;
        }

        public async Task<bool> IsExist(int id)
        {
            var heroes = await Get();
            return heroes.Any(x => x.Id == id);
        }

        public async Task<bool> IsExist(string name)
        {
            var heroes = await Get();
            return heroes.Any(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task Update(SuperHero entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
