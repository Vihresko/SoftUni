using System;
using System.Collections.Generic;

namespace SpeedRacing
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Car> cars = new List<Car>();
            for (int i = 0; i < n; i++)
            {
                string[] inputCar = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = inputCar[0];
                double fuelAmount = double.Parse(inputCar[1]);
                double fuelConsumption = double.Parse(inputCar[2]);
                Car currentCar = new Car(model, fuelAmount, fuelConsumption);
                cars.Add(currentCar);
            }

            string driveInput = Console.ReadLine();
            while(driveInput != "End")
            {
                string[] driveInfo = driveInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string modelForDrive = driveInfo[1];
                double kmForDrive = double.Parse(driveInfo[2]);
                foreach (var car in cars)
                {
                    if(car.Model == modelForDrive)
                    {
                        if(car.CanCarDriveTheDistance(modelForDrive, kmForDrive, car))
                        {
                            car.FuelAmount -= kmForDrive * car.FuelCosnumptionPerKilometer;
                            car.TravelledDistance += kmForDrive;
                        }
                        else
                        {
                            Console.WriteLine("Insufficient fuel for the drive");
                        }
                    }
                }
                driveInput = Console.ReadLine();
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.TravelledDistance}");
            }
        }
    }
}
