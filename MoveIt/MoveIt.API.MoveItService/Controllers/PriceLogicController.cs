using System.Web.Http;
using MoveIt.Core.Data.Model.DTO;

namespace MoveIt.API.MoveItService.Controllers
{

    public class PriceLogicController : ApiController
    {

        [AcceptVerbs("GET")]
        [HttpGet]
        public PriceLogicDTO GetPriceLogic(decimal distance, decimal volume, bool hasPiano)
        {

            PriceLogicDTO priceLogic = new PriceLogicDTO();

            //Distance logic
            if (distance >= 100)
            {
                priceLogic.DistanceCost = 10000 + (distance * 7);
                priceLogic.DistanceRule = "10000 SEK + 7 kr / km for distances over 100 km";
            }
            else if (distance < 50)
            {
                priceLogic.DistanceCost = 1000 + (distance * 10);
                priceLogic.DistanceRule = "1000 SEK + 10 SEK / km for distances below 50km";
            }
            else
            {
                priceLogic.DistanceCost = 5000 + (distance * 8);
                priceLogic.DistanceRule = "5000 SEK + 8 kr / km for distances between 50km and 100 km";
            }


            //Volume Logic
            int numberOfCars = (int)(volume / 50);
            if (numberOfCars >= 1)
            {
                priceLogic.VolumeCost = numberOfCars *
                                        priceLogic.DistanceCost;
                priceLogic.VolumeRule = "For each 50 m 2 needed an extra car";
            }


            //Piano logic
            if (hasPiano)
            {
                priceLogic.PianoRule = "Piano extra handling costs";
                priceLogic.PianoCost = 5000;
            }

            priceLogic.TotalCost = priceLogic.DistanceCost + priceLogic.VolumeCost + priceLogic.PianoCost;

            return priceLogic;
        }
    }
}
