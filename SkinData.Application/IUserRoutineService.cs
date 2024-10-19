using SkinData.Domain;
using SkinData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinData.Application
{
    public interface IUserRoutineService
    {
        Task<List<UserRoutine>> GetUserRoutinesAsync(int userId);
        Task AddUserRoutinesAsync(List<UserRoutine> userRoutines);
    }

    public class UserRoutineService : IUserRoutineService
    {
            private readonly IUserRoutineRepository _repository;

            public UserRoutineService(IUserRoutineRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<UserRoutine>> GetUserRoutinesAsync(int userId)
            {
                return await _repository.GetUserRoutinesAsync(userId);
            }

            public async Task AddUserRoutinesAsync(List<UserRoutine> userRoutine)
            {
                await _repository.AddUserRoutinesAsync(userRoutine);
            }
    }

}
