﻿using Maslshop.Repositories;

namespace Maslshop.Persistence
{
    public interface IUnitOfWork
    {
        IRoleRepository Roles { get; }
        IAdminRepository Admin { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IFileRepository File { get; }

        void Complete();
    }
}