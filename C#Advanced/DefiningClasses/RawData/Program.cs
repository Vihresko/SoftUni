using System;
using System.Collections.Generic;
using System.Linq;

namespace RawData
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Car> allCarsInformation = new List<Car>();
            for (int i = 0; i < n; i++)
            {
                string[] inputCar = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = inputCar[0];
                int engineSpeed = int.Parse(inputCar[1]);
                int enginePower = int.Parse(inputCar[2]);
                int cargoWeight = int.Parse(inputCar[3]);
                string cargoType = inputCar[4];
                double tire1Pressure = double.Parse(inputCar[5]);
                int tire1Age = int.Parse(inputCar[6]);
                double tire2Pressure = double.Parse(inputCar[7]);
                int tire2Age = int.Parse(inputCar[8]);
                double tire3Pressure = double.Parse(inputCar[9]);
                int tire3Age = int.Parse(inputCar[10]);
                double tire4Pressure = double.Parse(inputCar[11]);
                int tire4Age = int.Parse(inputCar[12]);


                Engine currentEngine = new Engine
                {
                    Speed = engineSpeed,
                    Power = enginePower
                };

                Cargo currentCargo = new Cargo
                {
                    Weight = cargoWeight,
                    Type = cargoType
                };
                List<Tire> currentCarTires = new List<Tire>(); 
                Tire tire1 = new Tire { Pressure = tire1Pressure, Age = tire1Age };
                Tire tire2 = new Tire { Pressure = tire2Pressure, Age = tire2Age };
                Tire tire3 = new Tire { Pressure = tire3Pressure, Age = tire3Age };
                Tire tire4 = new Tire { Pressure = tire4Pressure, Age = tire4Age };
                currentCarTires.Add(tire1);
                currentCarTires.Add(tire2);
                currentCarTires.Add(tire3);
                currentCarTires.Add(tire4);

                Car currentCarInfo = new Car(model, currentEngine, currentCargo, currentCarTires);
                allCarsInformation.Add(currentCarInfo);
            }

            string inputCargoForFilter = Console.ReadLine();

            //fragile   pressure < 1
            //flameable  engine power > 250

            List<Car> output = new List<Car>();
            if(inputCargoForFilter == "fragile")
            {
                output = allCarsInformation.Where(c => c.Cargo.Type == inputCargoForFilter).Where(p => p.Tires.Any(x => x.Pressure < 1)).ToList();
            }
            else if(inputCargoForFilter == "flamable")
            {
                output = allCarsInformation.Where(c => c.Cargo.Type == inputCargoForFilter).Where(e => e.Engine.Power > 250).ToList();
            }

            foreach (var car in output)
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}
