using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI_DotNet7.Models;

namespace SuperHeroAPI_DotNet7.Services.SuperHeroServices
{
    public interface ISuperHeroService
    {
        public Task<List<SuperHero>> Get();
        public Task<SuperHero> GetLast();

        public Task<SuperHero> GetById(int id);

        public Task<bool> IsExist(int id);
        public Task<bool> IsExist(string name);

        public Task Delete(SuperHero entity);

        public Task Update(SuperHero entity);

        public Task Create(SuperHero entity);

    }
}
