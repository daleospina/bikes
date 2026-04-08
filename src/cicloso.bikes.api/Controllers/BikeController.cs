using Microsoft.AspNetCore.Mvc;
using cicloso.bikes.core.Models;

namespace cicloso.bikes.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeController : ControllerBase
    {
        [HttpGet(Name = "ListBikes")]
        public IEnumerable<Bike> List()
        {
            return Enumerable.Range(1, 5).Select(index => new Bike
            {
                Id = index,
                Brand = "Brand " + index,
                Model = "Model " + index,
                Fork = "Fork " + index,
                Groupset = "Groupset " + index,
                Brakes = "Brakes " + index,
                Price = Random.Shared.Next(100, 1000),
                Url = "https://example.com/bike" + index,
                Image = "https://example.com/image" + index,
                Notes = "Notes for bike " + index
            })
            .ToArray();
        }
    }
}
