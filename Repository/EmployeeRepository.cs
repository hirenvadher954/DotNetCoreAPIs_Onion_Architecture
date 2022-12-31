using Contracts;

namespace Repository;

public class EmployeeRepository : RepositoryBase<EmployeeRepository>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}