using SkinData.Domain;
using SkinData.Infrastructure;



namespace SkinData.Application
{
    public class SkinDataService : ISkinDataService
    {
        private readonly ISkinAnalysisRepository _repository;

        public SkinDataService(ISkinAnalysisRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSkin> GetSkinDataAsync(int userId)
        {
            return await _repository.GetSkinDataAsync(userId);
        }

        public async Task UpdateSkinDataAsync(UserSkin skinData)
        {
            await _repository.AddSkinDataAsync(skinData);
        }
    }
}
