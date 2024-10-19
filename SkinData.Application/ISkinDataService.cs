using SkinData.Domain;

namespace SkinData.Application
{
    public interface ISkinDataService
    {
        Task<UserSkin> GetSkinDataAsync(int userId);
        Task UpdateSkinDataAsync(UserSkin skinData);
    }
}
