namespace ParkyAPI.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        INationalParkRepository NationalPark { get; }
        ITrailRepository Trail { get; }
        IUserRepository User { get; }

        void Save();
    }
}
