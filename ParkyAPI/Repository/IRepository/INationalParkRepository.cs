using ParkyAPI.Models;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository : IRepository<NationalPark>
    {
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
        void UpdateNationalPark(NationalPark entity);
    }
}
