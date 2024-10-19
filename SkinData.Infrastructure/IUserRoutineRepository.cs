using SkinData.Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace SkinData.Infrastructure
{
    public interface IUserRoutineRepository
    {
        Task<List<UserRoutine>> GetUserRoutinesAsync(int userId);
        Task AddUserRoutinesAsync(List<UserRoutine> userRoutines);
    }

    public class UserRoutineRepository : IUserRoutineRepository
    {
        private readonly string _connectionString;

        public UserRoutineRepository(IConfiguration configuration)
        {
            var skinAnalysisDbConfig = configuration.GetSection("ConnectionStrings").GetSection("SkinAnalysisDb");
            var host = skinAnalysisDbConfig["Host"];
            var port = skinAnalysisDbConfig["Port"];
            var database = skinAnalysisDbConfig["Database"];
            var username = skinAnalysisDbConfig["Username"];
            var password = skinAnalysisDbConfig["Password"];

            _connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }

        public async Task<List<UserRoutine>> GetUserRoutinesAsync(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("SELECT * FROM user_routine WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("UserId", userId);

                var routines = new List<UserRoutine>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var userRoutine = new UserRoutine
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            Task = reader.GetString(reader.GetOrdinal("task")),
                            Time = reader.GetString(reader.GetOrdinal("time")),
                            WhyWeShouldDoIt = reader.GetString(reader.GetOrdinal("why_we_should_do_it"))
                        };

                        routines.Add(userRoutine);
                    }
                }

                return routines;
            }
        }

        public async Task AddUserRoutinesAsync(List<UserRoutine> userRoutines)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var routine in userRoutines)
                        {
                            var query = @"
                            INSERT INTO public.user_routine (
                                user_id,
                                task,
                                time,
                                why_we_should_do_it
                            )
                            VALUES (
                                @UserId,
                                @Task,
                                @Time,
                                @WhyWeShouldDoIt
                            )";

                            var parameters = new
                            {
                                UserId = routine.UserId,
                                Task = routine.Task,
                                Time = routine.Time,
                                WhyWeShouldDoIt = routine.WhyWeShouldDoIt
                            };

                            await connection.ExecuteAsync(query, parameters, transaction);
                        }

                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }

}
