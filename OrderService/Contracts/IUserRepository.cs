
namespace OrderService.Contracts
{
    public interface IUserRepository: IGenericRepository<User>
    {
        void UpdateUserPassword(User user);
        public UserRole GetUserRole(int id);
    }
}
