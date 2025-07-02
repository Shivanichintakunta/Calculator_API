using CalculationAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace CalculationAPI.Data;


public class CalculatorContext : DbContext
{
    public CalculatorContext() { }
    public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options) { }
    static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Calci_API;Integrated Security=True";
    public DbSet<Calculation> Calculations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(connection);
    }
}