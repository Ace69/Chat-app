using Communication.BL.Exceptions;
using Communication.BL.Models;
using Communication.BL.Repositories;
using System;

namespace Communication.BL.Services
{
    public class LoginService 
    {
        UserRepository userRepository;
        public LoginService()
        {
            this.userRepository = new UserRepository(new DAL.CommunicationDbContext());
        }
        public LoginService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel LoadUserByEmail(string email, string rawPassword)
        {
            UserModel userModel = userRepository.GetUserByEmail(email);
            if (userModel != null)
            {
                if (userModel.isEnabled)
                {
                    if (BCrypt.Net.BCrypt.Verify(rawPassword, userModel.Password))
                    {
                        return userModel;
                    }
                }
            }
            throw new LoginException("Špatně zadaný email nebo heslo");
        }
        public void register(string name, string surname, string email, string password, string telephoneNumber)
        {
            string hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
            UserModel userModel = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Email = email,
                Password = hashedPass,
                TelephoneNumber = telephoneNumber,
                isEnabled = true
            };
            try
            {
                userRepository.Insert(userModel);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new CommunicationException("Uživatel s tímto emailem je již zaregistrován");
            }
        }

        public void register(string name, string surname, string email, string password, string telephoneNumber, byte[] photo)
        {
            string hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
            UserModel userModel = new UserModel() {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Email = email,
                Password = hashedPass,
                TelephoneNumber = telephoneNumber,
                isEnabled = true
            };
            try
            {
                userRepository.Insert(userModel);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new CommunicationException("Uživatel s tímto emailem je již zaregistrován");
            }
        }

        public void deactivateUser(string email)
        {
            UserModel userModel = userRepository.GetUserByEmail(email);
            //userRepository.dbContext.RemoveEntities<UserEntity>();
            this.deactivateUser(userModel);
        }
        public void deactivateUser(Guid guid)
        {
            UserModel userModel = userRepository.GetById(guid);
            //userRepository.dbContext.RemoveEntities<UserEntity>();
            this.deactivateUser(userModel);
        }

        private void deactivateUser(UserModel userModel)
        {
            userModel.isEnabled = false;
            userRepository.Update(userModel);
        }
    }
}
