namespace ParkyWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        INationalParkRepository NationalPark { get; }
        ITrailRepository Trail { get; }
        IAccountRepository Account { get; }
    }
}
