using ApiWithToken.Domain.Model;
using ApiWithToken.Domain.Repositories.Interfaces;
using ApiWithToken.Domain.Repositories.UnitOfWork;
using ApiWithToken.Domain.Services;
using ApiWithToken.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public UserResponse AddUser(Users user)
        {
            try
            {
                _userRepository.AddUser(user);
                _unitOfWork.Complete();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı Oluşturulurken Hata Oluştu {ex.Message}");
            }
        }

        public UserResponse FindByEmailAndPassword(string email, string password)
        {
            try
            {
                var user = _userRepository.FindByEmailAndPassword(email, password);

                if (user == null)
                    return new UserResponse($"Kullanıcı Bulunamadı");

                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı Bulunurken Hata Oluştu {ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                var user = _userRepository.FindById(userId);

                if (user == null)
                    return new UserResponse($"Kullanıcı Bulunamadı");

                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı Bulunurken Hata Oluştu {ex.Message}");
            }
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                var user = _userRepository.GetUserWithRefreshToken(refreshToken);

                if (user == null)
                    return new UserResponse($"Kullanıcı Bulunamadı");

                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı Bulunurken Hata Oluştu {ex.Message}");
            }
        }

        public void RemoveRefreshToken(Users user)
        {
            try
            {
                _userRepository.RemoveRefreshToken(user);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //Loglama yapılacak.
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                _userRepository.SaveRefreshToken(userId, refreshToken);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //Loglama yapılacak.
            }
        }
    }
}
