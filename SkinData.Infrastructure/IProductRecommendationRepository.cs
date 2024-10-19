using SkinData.Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;


namespace SkinData.Infrastructure
{
    public interface IProductRecommendationRepository
    {
        Task<List<ProductRecommendation>> GetProductRecommendationAsync(int userId);
        Task AddProductRecommendationsAsync(List<ProductRecommendation> productRecommendations);
    }

    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private readonly string _connectionString;

        public ProductRecommendationRepository(IConfiguration configuration)
        {
            var skinAnalysisDbConfig = configuration.GetSection("ConnectionStrings").GetSection("SkinAnalysisDb");
            var host = skinAnalysisDbConfig["Host"];
            var port = skinAnalysisDbConfig["Port"];
            var database = skinAnalysisDbConfig["Database"];
            var username = skinAnalysisDbConfig["Username"];
            var password = skinAnalysisDbConfig["Password"];

            _connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }

        public async Task<List<ProductRecommendation>> GetProductRecommendationAsync(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("SELECT * FROM product_recommendations WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("UserId", userId);

                var recommendations = new List<ProductRecommendation>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var productRecommendation = new ProductRecommendation
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                            WhatItContains = reader.GetString(reader.GetOrdinal("what_it_contains")),
                            WhyWeShouldDoIt = reader.GetString(reader.GetOrdinal("why_we_should_do_it"))
                        };

                        recommendations.Add(productRecommendation);
                    }
                }

                return recommendations;
            }
        }

        public async Task AddProductRecommendationsAsync(List<ProductRecommendation> productRecommendations)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                foreach (var recommendation in productRecommendations)
                {
                    var query = @"
                    INSERT INTO public.product_recommendations (
                        user_id,
                        product_name,
                        what_it_contains,
                        why_we_should_do_it
                    )
                    VALUES (
                        @UserId,
                        @ProductName,
                        @WhatItContains,
                        @WhyWeShouldDoIt
                    )";

                    var parameters = new
                    {
                        UserId = recommendation.UserId,
                        ProductName = recommendation.ProductName,
                        WhatItContains = recommendation.WhatItContains,
                        WhyWeShouldDoIt = recommendation.WhyWeShouldDoIt
                    };

                    await connection.ExecuteScalarAsync(query, parameters);
                }
            }
        }
    }

}

