using Comparison.Comparisons;
using Extensions;
using ItemLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparisonController : ControllerBase
    {
        
        [HttpPost("ComputerComparison/{priceWeight}/{ramWeight}/{storageWeight}")]
        public async Task<ActionResult<ComputerComparisonModel>> ComputerComparison(List<Computer> computers, int priceWeight, int ramWeight, int storageWeight)
        {
            ComputerComparison computerComparison = new ComputerComparison(priceWeight, storageWeight, ramWeight);
            ComputerComparisonModel compare = new ComputerComparisonModel();
            var comp1 = computers[0];
            var comp2 = computers[1];
            computerComparison.UpdateRatings(comp1, comp2,
                    (ranking) => { compare.PriceRanking1 = ranking.Item1.Truncate2(); compare.PriceRanking2 = ranking.Item2.Truncate2(); },
                    (ranking) => { compare.StorageRanking1 = ranking.Item1.Truncate2(); compare.StorageRanking2 = ranking.Item2.Truncate2(); },
                    (ranking) => { compare.RamRanking1 = ranking.Item1.Truncate2(); compare.RamRanking2 = ranking.Item2.Truncate2(); },
                    (itemRanking) => { compare.ItemRanking1 = itemRanking.Item1.Truncate2(); compare.ItemRanking2 = itemRanking.Item2.Truncate2(); }
                );

            return compare;
        }

        [HttpPost("SmartphoneComparison/{priceWeight}/{ramWeight}/{storageWeight}/{cameraWeight}")]
        public async Task<ActionResult<SmartphoneComparisonModel>> SmartphoneComparison(List<Smartphone> smartphones, int priceWeight, int ramWeight, int storageWeight, int cameraWeight)
        {
            SmartphoneComparison smartphoneComparison = new SmartphoneComparison(priceWeight, ramWeight, storageWeight, cameraWeight);
            SmartphoneComparisonModel compare = new SmartphoneComparisonModel();
            var phone1 = smartphones[0];
            var phone2 = smartphones[1];
            smartphoneComparison.UpdateRatings(phone1, phone2,
                    (ranking) => { compare.PriceRanking1 = ranking.Item1.Truncate2(); compare.PriceRanking2 = ranking.Item2.Truncate2(); },
                    (ranking) => { compare.StorageRanking1 = ranking.Item1.Truncate2(); compare.StorageRanking2 = ranking.Item2.Truncate2(); },
                    (ranking) => { compare.RamRanking1 = ranking.Item1.Truncate2(); compare.RamRanking2 = ranking.Item2.Truncate2(); },
                    (ranking) => { compare.CameraRanking1 = ranking.Item1.Truncate2(); compare.CameraRanking2 = ranking.Item2.Truncate2(); },
                    (itemRanking) => { compare.ItemRanking1 = itemRanking.Item1.Truncate2(); compare.ItemRanking2 = itemRanking.Item2.Truncate2(); }
                );

            return compare;
        }
    }
}
