using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Station.Aop;
using Station.EFCore.IbmDb;
using Station.Repository.RepositoryPattern;
using Station.Repository.RepositoryPattern.Implementation;

namespace Station.Repository.Employee.Implementation
{
    [ServiceDescriptor(typeof(IEmployeeRepository), ServiceLifetime.Transient)]
    public class EmployeeRepository : RepositoryBase<Entity.DB2Admin.Employee>,IEmployeeRepository
    {
        private readonly IbmDbContext _context;
        private readonly IApplicationContext _applicationContext;

        public EmployeeRepository(IbmDbContext context,IApplicationContext applicationContext):base(context,applicationContext)
        {
            this._context = context;
            _applicationContext = applicationContext;
        }
    }
}
