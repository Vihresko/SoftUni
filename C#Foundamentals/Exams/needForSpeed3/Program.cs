using System;
using System.Collections.Generic;
using System.Linq;
namespace needForSpeed3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> garage = new List<Car>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string[] cmdArgs = input.Split("|",StringSplitOptions.RemoveEmptyEntries);
                string carName = cmdArgs[0];
                int miles = int.Parse(cmdArgs[1]);
                int fuel = int.Parse(cmdArgs[2]);
                garage.Add(new Car(carName, miles, fuel));
            }
            string actionInput = Console.ReadLine();
            while(actionInput != "Stop")
            {
                string[] cmdArgs = actionInput.Split(" : ");
                string command = cmdArgs[0];
                string car = cmdArgs[1];
                Car currentCar = garage.FirstOrDefault(x => x.Name == car);
                
                if(command == "Drive")
                {
                    int distance = int.Parse(cmdArgs[2]);
                    int fuelNeed = int.Parse(cmdArgs[3]);
                    if(currentCar.Fuel < fuelNeed)
                    {
                        Console.WriteLine("Not enough fuel to make that ride");
                    }
                    else
                    {
                        Console.WriteLine($"{car} driven for {distance} kilometers. {fuelNeed} liters of fuel consumed.");
                        currentCar.Fuel -= fuelNeed;
                        currentCar.Miles += distance;
                        if(currentCar.Miles >= 100000)
                        {
                            Console.WriteLine($"Time to sell the {car}!");
                            garage.Remove(currentCar);
                        }
                    }
                }
                else if (command == "Refuel")
                {
                    int fuelForReload = int.Parse(cmdArgs[2]);
                    int currentFuel = currentCar.Fuel;
                    currentCar.Fuel += fuelForReload;
                    int reloadedFuel = fuelForReload;

                    if (currentCar.Fuel > 75)
                    {
                        currentCar.Fuel = 75;
                        reloadedFuel = 75 - currentFuel;

                    }
                    Console.WriteLine($"{car} refueled with {reloadedFuel} liters");
                }
                else if(command == "Revert")
                {
                    int kilometersForRevert = int.Parse(cmdArgs[2]);
                    currentCar.Miles -= kilometersForRevert;
                    if(currentCar.Miles < 10000)
                    {
                        currentCar.Miles = 10000;
                    }
                    else
                    {
                        Console.WriteLine($"{car} mileage decreased by {kilometersForRevert} kilometers");
                    }
                }
                actionInput = Console.ReadLine();
            }

            foreach (var car in garage.OrderByDescending(m=> m.Miles).ThenBy(n=> n.Miles))
            {
                Console.WriteLine(car);
            }
        }
    }
    class Car
    {
        public  Car(string name, int miles, int fuel)
        {
            Name = name;
            Miles = miles;
            Fuel = fuel;
        }
        public string Name { get; set; }
        public int Miles { get; set; }
        public int Fuel { get; set; }
        public override string ToString()
        {
            return $"{Name} -> Mileage: {Miles} kms, Fuel in the tank: {Fuel} lt.";

        }
    }
}
