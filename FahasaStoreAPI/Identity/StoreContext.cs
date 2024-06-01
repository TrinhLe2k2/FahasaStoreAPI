using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Identity
{
    public partial class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

    }
}
