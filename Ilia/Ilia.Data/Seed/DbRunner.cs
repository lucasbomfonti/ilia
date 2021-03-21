using Ilia.Data.Context;

namespace Ilia.Data.Seed
{
    public class DbRunner
    {
        public static void UpdateDatabase()
        {
            var context = new DataContext();
            context.UpdateDatabase();
        }
    }
}