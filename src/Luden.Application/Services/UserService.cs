using Luden.Application.Core.Services;
using Luden.Application.Interfaces;
using Luden.Application.Models.DTOs;
using Luden.Application.Models.Requests;
using Luden.Application.Models.Responses;
using Luden.Domain.Core.Repositories;
using Luden.Domain.Entities;
using Luden.Domain.Enums;
using Luden.Domain.Exceptions;
using Luden.Domain.Specifications;


namespace Luden.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;

        public UserService(IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
        }

        public async Task<CreateUserRes> CreateUser(CreateUserReq req)
        {
            var user = await _unitOfWork.Repository<User>().AddAsync(new User
            {
                UserName = req.Name,
                Email = req.Email,
                Password = req.Password,
                Status = req.Status
            });

            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo($"user: {user.Id} created");

            return new CreateUserRes() { Data = new UserDTO(user) };
        }

        public async Task<ValidateUserRes> ValidateUser(ValidateUserReq req)
        {
            var validateUserSpec = UserSpecifications.GetUserByEmailAndPasswordSpec(req.Email, req.Password);

            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(validateUserSpec);

            if (user == null)
            {
                _loggerService.LogInfo("User not found");

                throw new UserNotFoundException();
            }

            if (user.Status != UserStatus.Active)
            {
                _loggerService.LogInfo("User not active");

                throw new UserIsNotActiveException();
            }

            return new ValidateUserRes()
            {
                Id = user.Id,
                Name = user.UserName,
            };
        }

        public async Task<GetAllActiveUsersRes> GetAllActiveUsers()
        {
            var activeUsersSpec = UserSpecifications.GetAllActiveUsersSpec();

            var users = await _unitOfWork.Repository<User>().ListAsync(activeUsersSpec);

            return new GetAllActiveUsersRes()
            {
                Data = users.Select(x => new UserDTO(x)).ToList()
            };
        }
    }
}