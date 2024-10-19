using SkinData.Domain;
using SkinData.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinData.Application
{
    public interface IProductRecommendationService
    {
        Task<List<ProductRecommendation>> GetProductRecommendationAsync(int userId);
        Task AddProductRecommendationsAsync(List<ProductRecommendation> productRecommendations);
    }

    public class ProductRecommendationService : IProductRecommendationService
    {
        private readonly IProductRecommendationRepository _repository;

        public ProductRecommendationService(IProductRecommendationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductRecommendation>> GetProductRecommendationAsync(int userId)
        {
            return await _repository.GetProductRecommendationAsync(userId);
        }

        public async Task AddProductRecommendationsAsync(List<ProductRecommendation> productRecommendations)
        {
            await _repository.AddProductRecommendationsAsync(productRecommendations);
        }
    }


}
