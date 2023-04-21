using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;

        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetByEmail(string email)
        {
            var result = dbContext.Users.FirstOrDefault(e => e.Email == email);
            return result;
        }
    }
}
