using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet7.Data;
using SuperHeroAPI_DotNet7.Models;

namespace SuperHeroAPI_DotNet7.Services.SuperHeroServices
{
    public class ProxySuperHeroService : ISuperHeroService
    {
        private readonly DatabaseContext _dbContext;
        private readonly SuperHeroService _superHeroService;
        private List<SuperHero> _superHeroes;
        private SuperHero _superHero;

        public ProxySuperHeroService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _superHeroService = new SuperHeroService(dbContext);
        }

        public async Task Create(SuperHero entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _superHeroes.Add(entity);
        }

        public async Task Delete(SuperHero entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<SuperHero>> Get()
        {
            return _superHeroes ??= await _superHeroService.Get();
        }

        public async Task<SuperHero> GetById(int id)
        {

            return _superHero ??= await _superHeroService.GetById(id);
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
