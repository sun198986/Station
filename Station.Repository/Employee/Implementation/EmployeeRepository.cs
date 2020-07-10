using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Station.EFCore.IbmDb;

namespace Station.Repository.Employee.Implementation
{
    [ServiceDescriptor(typeof(IEmployeeRepository), ServiceLifetime.Transient)]
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
