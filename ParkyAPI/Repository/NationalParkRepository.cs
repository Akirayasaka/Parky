using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public bool NationalParkExists(string name)
        {
            bool exists = _db.NationalParks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return exists;
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(x => x.Id == id);
        }

        public void UpdateNationalPark(NationalPark entity)
        {
            var target = _db.NationalParks.FirstOrDefault(x => x.Id == entity.Id);
            try
            {
                _db.NationalParks.Update(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
