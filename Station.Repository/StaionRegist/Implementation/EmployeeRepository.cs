using Station.EFCore.IbmDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Repository.StaionRegist.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IbmDbContext _context;

        public EmployeeRepository(IbmDbContext context)
        {
            this._context = context;
        }

        public IbmDbContext GetDbContext() => this._context;


    }
}
