using System.Collections.Generic;
using System.Threading.Tasks;

namespace CruiseBookingApp.Services.Promotion
{
    public interface IPromotionService
    {
        Task<List<string>> GetFirstViewBannerItemsAsync();
        Task<List<Models.FirstViewCategory>> GetFirstViewCategoryItemsAsync();
    }
}
