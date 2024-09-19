using Microsoft.Extensions.DependencyInjection;

namespace EMS.DB.Repository
{
    public class BaseRepository
    {
        /// <summary>
        /// Class to define Iservicescope factory.
        /// </summary>
        protected readonly IServiceScopeFactory _serviceScopeFactory;

        public BaseRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Gets the dbcontext using IServiceScopeFactory
        /// </summary>
        /// <returns></returns>
        protected AppDbContext GetContext() => _serviceScopeFactory.CreateScope().ServiceProvider.GetService<AppDbContext>();
    }
}