using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Station.EFCore.IbmDb;

namespace Station.Repository.Employee.Implementation
{
    [ServiceDescriptor(typeof(IEmployeeRepository), ServiceLifetime.Transient)]
    public class EmployeeRepository : RepositoryBase<Entity.DB2Admin.Employee>,IEmployeeRepository
    {
        private readonly IbmDbContext _context;

        public EmployeeRepository(IbmDbContext context):base(context)
        {
            this._context = context;
        }
    }
}
