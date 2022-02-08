using ParkyAPI.Models;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepository : IRepository<Trail>
    {
        bool TrailExists(string name);
        bool TrailExists(int id);
        void UpdateTrail(Trail entity);
        ICollection<Trail> GetTrailsInNationalPark(int npId);
    }
}
