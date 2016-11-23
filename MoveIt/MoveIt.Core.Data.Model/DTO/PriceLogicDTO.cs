using System.Collections.Generic;


namespace MoveIt.Core.Data.Model.DTO
{
    public class PriceLogicDTO
    {

        public string DistanceRule { get; set; }

        public decimal DistanceCost { get; set; }


        public string VolumeRule { get; set; } = "No Extra Volume Costs!";

        public decimal VolumeCost { get; set; }

        public string PianoRule { get; set; } = "No Piano, So No Extra Costs!";

        public decimal PianoCost { get; set; }

        public decimal TotalCost { get; set; }
    }
}
