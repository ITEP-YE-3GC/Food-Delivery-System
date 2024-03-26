
namespace OrderService.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public void UpdateUserPassword(User user)
        {

        }
        public UserRole GetUserRole(int id)
        {
            User user = _applicationContext.Users.Find(id);
            return user.Role;
        }
    }
}
