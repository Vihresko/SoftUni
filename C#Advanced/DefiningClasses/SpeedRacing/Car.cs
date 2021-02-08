using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedRacing
{
     public class Car
    {
        public string Model{ get; set; }
        public double FuelAmount{ get; set; }
        public double FuelCosnumptionPerKilometer{ get; set; }
        public double TravelledDistance{ get; set; }

        public Car()
        {
            this.TravelledDistance = 0;
        }

        public Car(string model, double fuelAmount, double consumption) : this()
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelCosnumptionPerKilometer = consumption;
        }

        public bool CanCarDriveTheDistance(string model, double distance, Car car)
        {
            bool result = false;
            double needeFuel = car.FuelCosnumptionPerKilometer * distance;
            if (car.FuelAmount - needeFuel >= 0)
            {
                result = true;
            }

            return result;
        }
    }
}
