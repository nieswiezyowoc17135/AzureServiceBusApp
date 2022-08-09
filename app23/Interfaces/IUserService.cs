using app23.DTOs;

namespace app23.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(UserAddDTO user); 
        Task<bool> UpdateUser(int id, UserEditDTO user);
        Task<List<UserShowDTO>> GetUsers();
    }
}
