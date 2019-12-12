namespace ClubestApp.Data.Seeding
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task Seed();
    }
}