namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public Car()
        {
            this.Sales = new List<Sale>();
            this.PartCars = new List<PartCar>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int TravelledDistance { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<PartCar> PartCars { get; set; }
    }
}