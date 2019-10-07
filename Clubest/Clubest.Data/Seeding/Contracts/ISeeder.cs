namespace Clubest.Data.Data.Seeding
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task Seed();
    }
}