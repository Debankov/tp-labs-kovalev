using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.Autopark;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация автопарка ===\n");

            Truck truck1 = new Truck("KAMAZ-6520", 35.0, 150000, 2000.0);
            Truck truck2 = new Truck("Volvo FH16", 28.0, 85000, 1500.5);
            Bus bus1 = new Bus("Mercedes Sprinter", 14.0, 120000, 18);
            LightCar car1 = new LightCar("Toyota Camry", 8.5, 45000, 5, 500, true, true);
            LightCar car2 = new LightCar("Lada Vesta", 7.0, 15000, 5, 400, false, false);

            List<Vehicle> autopark = new List<Vehicle>
            {
                truck1, truck2, bus1, car1, car2
            };

            double distance = 150.0; // км
            double fuelPrice = 55.5; // руб/литр

            Console.WriteLine("Демонстрация работы загрузки и выгрузки у грузовиков и автобуса");
            truck1.CargoLoad(100);
            truck1.CargoUnload(50);

            bus1.PassagiereLoad(15);
            bus1.PassagiereUnload(12);

            Console.WriteLine($"Расчет стоимости поездки на {distance} км при цене топлива {fuelPrice} руб/л:\n");
            Console.WriteLine(new string('-', 70));

            foreach (Vehicle v in autopark)
            {
                Console.WriteLine(v.Info());

                // Полиморфизм в действии: каждый объект сам знает, как считать свою стоимость
                double cost = v.RoadTripCost(distance, fuelPrice);
                Console.WriteLine($"Стоимость поездки: {cost:F2} руб.");
                Console.WriteLine(new string('-', 70));
            }

            Console.WriteLine("Готово. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
