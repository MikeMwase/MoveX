using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoveX.Infrastructure.Data;

public class MoveXDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MoveXDbContext>
{
    public MoveXDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("MOVEX_CONNECTION_STRING")
            ?? "Server=(localdb)\\MSSQLLocalDB;Database=MoveXDb;Trusted_Connection=True;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<MoveXDbContext>();
        optionsBuilder.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure());

        return new MoveXDbContext(optionsBuilder.Options);
    }
}
