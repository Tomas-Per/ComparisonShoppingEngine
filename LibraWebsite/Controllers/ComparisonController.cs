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
       
        [HttpPost("/api/Comparison/{priceWeight}/{storageWeight}/{ramWeight}")]
        public async Task<ActionResult<ComputerComparisonModel>> Comparison(List<Computer> computers, int priceWeight, int storageWeight, int ramWeight)
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
    }
}
