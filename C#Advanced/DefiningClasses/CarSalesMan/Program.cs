using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesMan
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Engine> engines = new List<Engine>();

            for (int i = 0; i < n; i++)
            {
                string[] inputEngine = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string engineModel = inputEngine[0];
                int enginePower = int.Parse(inputEngine[1]);
                Engine currentEngine = new Engine();
                currentEngine.Model = engineModel;
                currentEngine.Power = enginePower;
                int engineDisplacement = 0;
                string efficienty = null;
                if(inputEngine.Length > 2 && char.IsDigit(inputEngine[2][0]))
                {
                    engineDisplacement = int.Parse(inputEngine[2]);
                    currentEngine.Displacement = engineDisplacement;
                }
                 else if(inputEngine.Length  > 2)
                {
                    efficienty = inputEngine[2];
                    currentEngine.Efficiency = efficienty;
                }

                if(inputEngine.Length > 3)

                {
                    efficienty = inputEngine[3];
                    currentEngine.Efficiency = efficienty;
                }
                engines.Add(currentEngine);
            }



            List<Car> cars = new List<Car>();
            int m = int.Parse(Console.ReadLine());
            for (int i = 0; i < m; i++)
            {
                string[] inputCar = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string carModel = inputCar[0];
                string engineName = inputCar[1];
                List<Engine> selectEngine = engines.Where(x => x.Model == engineName).ToList();
                Engine carEngine = selectEngine[0];
                Car currentCar = new Car();
                currentCar.Model = carModel;
                currentCar.Engine = carEngine;
                if(inputCar.Length > 2 && char.IsDigit(inputCar[2][0]))
                {
                    int weight = int.Parse(inputCar[2]);
                    currentCar.Weight = weight;
                }
                else if(inputCar.Length > 2)
                {
                    string color = inputCar[2];
                    currentCar.Color = color;
                }

                if(inputCar.Length > 3)
                {
                    string color = inputCar[3];
                    currentCar.Color = color;
                }
                cars.Add(currentCar);

            }

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model}:");
                Console.WriteLine($"  {car.Engine.Model}:");
                Console.WriteLine($"    Power: {car.Engine.Power}");
                if(car.Engine.Displacement != 0)
                {
                    Console.WriteLine($"    Displacement: {car.Engine.Displacement}");
                }
                else
                {
                    Console.WriteLine($"    Displacement: n/a");
                }
                
                if(car.Engine.Efficiency != null)
                {
                    Console.WriteLine($"    Efficiency: {car.Engine.Efficiency}");
                }
                else
                {
                    Console.WriteLine($"    Efficiency: n/a");
                }
               
                if(car.Weight != 0)
                {
                    Console.WriteLine($"  Weight: {car.Weight}");
                }
                else
                {
                    Console.WriteLine($"  Weight: n/a");
                }
               
                if(car.Color != null)
                {
                    Console.WriteLine($"  Color: {car.Color}");
                }
                else
                {
                    Console.WriteLine($"  Color: n/a");
                }
               
            }

        }
    }
}
