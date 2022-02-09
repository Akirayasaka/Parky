﻿using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpClientFactory _clientFactory;

        public UnitOfWork(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            NationalPark = new NationalParkRepository(clientFactory);
            Trail = new TrailRepository(clientFactory);
        }

        public INationalParkRepository NationalPark { get; private set; }
        public ITrailRepository Trail { get; private set; }
    }
}
