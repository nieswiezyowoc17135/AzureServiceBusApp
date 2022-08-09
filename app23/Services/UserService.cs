using app23.Data;
using app23.DTOs;
using app23.Interfaces;
using app23.Models;
using Microsoft.EntityFrameworkCore;

namespace app23.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;
        public UserService(UserContext userContext)
        {
            _context = userContext;
        }
        
        //dodawanie uzytkownikow
        public async Task<bool> AddUser(UserAddDTO user)
        {
            var userToDatabase = new User();
            userToDatabase.Name = user.Name;
            userToDatabase.Surname = user.Surname;
            userToDatabase.Age = user.Age;
            userToDatabase.Email = user.Email;

            _context.Users.Add(userToDatabase);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //edytowanie uzytkownikow
        public async Task<bool> UpdateUser(int id, UserEditDTO user)
        {
            var userToEdit = _context.Users.FirstOrDefault(x => x.Id == id);
            
            if (userToEdit == null)
            {
                throw new Exception("Okropna chujnia");
            }

            userToEdit.Name = user.Name;
            userToEdit.Surname = user.Surname;
            userToEdit.Age = user.Age;
            userToEdit.Email = user.Email;

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        //wyswietlanie wszystkich uzytkownikow
        public async Task<List<UserShowDTO>> GetUsers()
        {
            return await _context.Users.Select(x => new UserShowDTO
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Age = x.Age,
                Email= x.Email,
                IsActive = x.IsActive
            }).ToListAsync();
        }
    }
}
