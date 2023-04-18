using Microsoft.EntityFrameworkCore;

namespace DFN2023.Infrastructure.Context
{
    public partial class ApplicationContext : BaseContext<ApplicationContext>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}

