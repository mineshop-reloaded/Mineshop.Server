﻿using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories.Interfaces;

public interface IProductRepository : IMineshopRepository<ProductEntity>
{
    Task<ProductEntity?> GetByName(string name);
}