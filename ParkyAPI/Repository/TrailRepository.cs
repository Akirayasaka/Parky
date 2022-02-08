using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{
    public class TrailRepository : Repository<Trail>, ITrailRepository
    {
        private readonly ApplicationDbContext _db;

        public TrailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool TrailExists(string name)
        {
            bool exists = _db.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return exists;
        }

        public bool TrailExists(int id)
        {
            return _db.Trails.Any(x =>x.Id == id);
        }

        public void UpdateTrail(Trail entity)
        {
            var target = _db.Trails.FirstOrDefault(x => x.Id == entity.Id);
            try
            {
                _db.Trails.Update(entity);
                _db.SaveChanges();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public ICollection<Trail> GetTrailsInNationalPark(int npId)
        {
            return _db.Trails.Include(x => x.NationalPark).Where(x => x.NationalParkId == npId).ToList();
        }
    }
}
