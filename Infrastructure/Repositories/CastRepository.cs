
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Cast> GetByIdAsync(int id)
        {
            var cast = await _dbContext.Casts.Include(c => c.MovieCasts).ThenInclude(mc => mc.Movie).FirstOrDefaultAsync(c => c.Id == id);

            if (cast == null)
            {
                throw new Exception($"No Cast Found with {id}");
            }
            return cast;
        }
    }
}