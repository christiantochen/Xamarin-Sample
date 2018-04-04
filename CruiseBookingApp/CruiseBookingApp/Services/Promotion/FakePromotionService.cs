using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CruiseBookingApp.Services.Promotion
{
    public class FakePromotionService : IPromotionService
    {
        public async Task<List<string>> GetFirstViewBannerItemsAsync()
        {
            await Task.Delay(1000);

            return new List<string>
            {
                "http://www.shorts-trip.com/wp-content/uploads/2018/02/front.jpg",
                "http://www.shorts-trip.com/wp-content/uploads/2018/02/tidar.jpg",
                "http://www.shorts-trip.com/wp-content/uploads/2018/02/end-1.jpg"
            };
        }

        public async Task<List<Models.FirstViewCategory>> GetFirstViewCategoryItemsAsync()
        {
            await Task.Delay(1000);

            return new List<Models.FirstViewCategory>
            {
                new Models.FirstViewCategory {
                    Name = "Today's Top Deal",
                    Items = new List<string>{
                        "https://s3-ap-southeast-1.amazonaws.com/asset1.gotomalls.com/uploads/news/translation/2017/09/LRQjLst5IP0yvaAc-promo-buy-1-get-1-free-royal-caribbean-on-tx-travel-1506209631_1.jpg",
                        "https://3.bp.blogspot.com/-dL2ec3TZcsk/WLT41uNq40I/AAAAAAAAa7c/jge3cUxUzgQzNfcpiPlbRPkE3mnEdPADwCLcB/s400/Star%2BCruises%2Bpromo.jpg",
                        "https://scontent-sin6-2.xx.fbcdn.net/v/t1.0-9/25158220_1999580896967649_4758992677781982331_n.jpg?oh=28cc325add7abf1b28ef9aba31795d7c&oe=5AFFEBF4",
                        }
                },
                new Models.FirstViewCategory {
                    Name = "Popular Cruise Lines",
                    Items = new List<string>{
                        "https://s3-ap-southeast-1.amazonaws.com/asset1.gotomalls.com/uploads/news/translation/2017/09/LRQjLst5IP0yvaAc-promo-buy-1-get-1-free-royal-caribbean-on-tx-travel-1506209631_1.jpg",
                        "https://3.bp.blogspot.com/-dL2ec3TZcsk/WLT41uNq40I/AAAAAAAAa7c/jge3cUxUzgQzNfcpiPlbRPkE3mnEdPADwCLcB/s400/Star%2BCruises%2Bpromo.jpg",
                        "https://scontent-sin6-2.xx.fbcdn.net/v/t1.0-9/25158220_1999580896967649_4758992677781982331_n.jpg?oh=28cc325add7abf1b28ef9aba31795d7c&oe=5AFFEBF4",
                    }
                },
                new Models.FirstViewCategory {
                    Name = "Recommended For You",
                    Items = new List<string>{
                        "https://s3-ap-southeast-1.amazonaws.com/asset1.gotomalls.com/uploads/news/translation/2017/09/LRQjLst5IP0yvaAc-promo-buy-1-get-1-free-royal-caribbean-on-tx-travel-1506209631_1.jpg",
                        "https://3.bp.blogspot.com/-dL2ec3TZcsk/WLT41uNq40I/AAAAAAAAa7c/jge3cUxUzgQzNfcpiPlbRPkE3mnEdPADwCLcB/s400/Star%2BCruises%2Bpromo.jpg",
                        "https://scontent-sin6-2.xx.fbcdn.net/v/t1.0-9/25158220_1999580896967649_4758992677781982331_n.jpg?oh=28cc325add7abf1b28ef9aba31795d7c&oe=5AFFEBF4",
                    }
                },
            };
        }
    }
}
