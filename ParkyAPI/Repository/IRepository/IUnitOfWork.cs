﻿namespace ParkyAPI.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        INationalParkRepository NationalPark { get; }

        void Save();
    }
}
