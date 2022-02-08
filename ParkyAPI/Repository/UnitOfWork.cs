﻿using ParkyAPI.Data;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            NationalPark = new NationalParkRepository(_db);
        }

        public INationalParkRepository NationalPark { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public bool Save()
        {
            _db.SaveChanges();
            return _db.SaveChanges() >= 0;
        }
    }
}
