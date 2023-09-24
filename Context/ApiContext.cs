using Microsoft.EntityFrameworkCore;

namespace RESTfulAPI.Context{
    public class ApiContext: DbContext{
        public ApiContext(DbContextOptions<ApiContext> options): base(options){}
        public DbSet<Models.Task> Tasks {get; set;} = null!;
    }
}