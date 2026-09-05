using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Autopark
    {
        public interface ITruckDoSomething
        {
            void CargoLoad(double cargo);
            void CargoUnload(double cargo);
            void CargoInfo();
        }


        public interface IBusDoSomething
        {
            void PassagiereLoad(int passagiere);
            void PassagiereUnload(int passagiere);
            void PassangiereInfo();
        }

        public interface ILightCar
        {
            void BoardPassengers(int count);
            void UnboardPassengers(int count);
            void LoadTrunk(double volumeLiters);
            void UnloadTrunk(double volumeLiters);
        }

        public class Vehicle
        {
            public string Model {  get; }
            public double FuelConsumption { get;  }
            public double Mileage { get; private set; }


            public Vehicle(string model, double fuelComsumption, double mileage)
            {
                if (model == null) throw new ArgumentNullException("model cant be nullable");
                if (fuelComsumption < 0) throw new ArgumentOutOfRangeException("fuelComsumption must be bigger than 0s");
                if (mileage < 0) mileage = 0;
                Model = model;
                FuelConsumption = fuelComsumption; 
                Mileage = mileage; 
            }

            public virtual double RoadTripCost(double distance, double fuelcost)
            {
                return (distance * FuelConsumption) * fuelcost;
            }

            public virtual string Info()
            {
                return "ТС модели: " + Model;
            }

        }

        public class Truck: Vehicle, ITruckDoSomething
        {

            public double CargoCapacity { get; private set; }
            public double MaxCargoCapacity { get; }
            public Truck(string model, double fuelComsumption, double mileage, double maxCargoCapacity) : base(model, fuelComsumption, mileage)
            {
                if (maxCargoCapacity < 0) throw new ArgumentOutOfRangeException("max cargo capacity must be bigger than 0");
                CargoCapacity = 0;
                MaxCargoCapacity = maxCargoCapacity;
            }
            public void CargoInfo()
            {
                Console.WriteLine("Текущая загрузка " + Model + ": " + CargoCapacity + "|" + MaxCargoCapacity);
            }

            public void CargoLoad(double cargo) 
            {
                if (cargo < 0) throw new ArgumentOutOfRangeException();

                if (CargoCapacity + cargo > MaxCargoCapacity)
                {
                    Console.WriteLine("Невозможно загрузить груз, свыше максимальной грузоподъёмности");
                }
                else
                {
                    CargoCapacity += cargo;
                    CargoInfo();
                }
            }
            public void CargoUnload(double cargo) 
            {
                if (cargo < 0) throw new ArgumentOutOfRangeException("");

                if (CargoCapacity - cargo < 0)
                {
                    Console.WriteLine("Невозможно выгрузить больше груза чем есть.");
                }
                else
                {
                    CargoCapacity -= cargo;
                    CargoInfo();
                }
            }

            public override string Info()
            {
                return "Грузовик " + Model + " Максимальная вместимость: " + MaxCargoCapacity + " Текущая вместимость: " + CargoCapacity;
            }
            public override double RoadTripCost(double distance, double fuelcost)
            {
                return ((distance * FuelConsumption) * fuelcost) + (MaxCargoCapacity * 5.75);
            }

        }


        public class Bus: Vehicle, IBusDoSomething
        {
            public int PassagiereCapacity{ get; private set; }
            public int MaxPassagiereCapacity { get; private set; }

            public Bus(string model, double fuelComsumption, double mileage, int maxpassngierecapacity) : base(model, fuelComsumption, mileage)
            {
                if (maxpassngierecapacity < 0) throw new ArgumentOutOfRangeException("it must be bigger than 0");
                PassagiereCapacity = 0;
                MaxPassagiereCapacity = maxpassngierecapacity;
            }
            public void PassangiereInfo()
            {
                Console.WriteLine("Текущее количество пассажиров: " + Model + " "+ PassagiereCapacity + "|" + MaxPassagiereCapacity);
            }

            public void PassagiereLoad(int passagiere)
            {
                if (passagiere < 0) throw new ArgumentOutOfRangeException();
                if (passagiere + PassagiereCapacity > MaxPassagiereCapacity)
                {
                    Console.WriteLine("нельзя разместить больше людей, чем есть мест");
                }
                else
                {
                    PassagiereCapacity += passagiere;
                    PassangiereInfo();
                }
            }

            public void PassagiereUnload(int passagiere)
            {
                if (passagiere < 0) throw new ArgumentOutOfRangeException();
                if (PassagiereCapacity - passagiere < 0)
                {
                    Console.WriteLine("нельзя высадить больше людей, чем есть");
                }
                else
                {
                    PassagiereCapacity -= passagiere;
                    PassangiereInfo();
                }
            }

            public override double RoadTripCost(double distance, double fuelcost)
            {
                return ((distance * FuelConsumption) * fuelcost) / MaxPassagiereCapacity;
            }
            public override string Info()
            {
                return "Автобус " + Model + " Максимальная вместимость: " + MaxPassagiereCapacity + " Текущая вместимость: " + PassagiereCapacity;
            }
        }

        public class LightCar : Vehicle, ILightCar
        {
            public int PassengerSeats { get; private set; }
            public int CurrentPassengers { get; private set; }
            public double TrunkCapacity { get; } // Объем багажника в литрах
            public double CurrentTrunkLoad { get; private set; }
            public bool HasAirConditioner { get; }
            public bool HasNavigationSystem { get; }

            public LightCar(string model, double fuelConsumption, double mileage,
                            int passengerSeats, double trunkCapacity,
                            bool hasAirConditioner = false, bool hasNavigationSystem = false)
                : base(model, fuelConsumption, mileage)
            {
                if (passengerSeats <= 0 || passengerSeats > 9)
                    throw new ArgumentOutOfRangeException("Passenger seats must be between 1 and 9");
                if (trunkCapacity < 0)
                    throw new ArgumentOutOfRangeException("Trunk capacity cannot be negative");

                PassengerSeats = passengerSeats;
                CurrentPassengers = 0;
                TrunkCapacity = trunkCapacity;
                CurrentTrunkLoad = 0;
                HasAirConditioner = hasAirConditioner;
                HasNavigationSystem = hasNavigationSystem;
            }

            // Посадка пассажиров
            public void BoardPassengers(int count)
            {
                if (count < 0) throw new ArgumentOutOfRangeException("Count cannot be negative");

                if (CurrentPassengers + count > PassengerSeats)
                {
                    Console.WriteLine($"Невозможно посадить {count} пассажиров. Свободных мест: {PassengerSeats - CurrentPassengers}");
                }
                else
                {
                    CurrentPassengers += count;
                    Console.WriteLine($"Посажено {count} пассажиров. Всего в салоне: {CurrentPassengers}/{PassengerSeats}");
                }
            }

            // Высадка пассажиров
            public void UnboardPassengers(int count)
            {
                if (count < 0) throw new ArgumentOutOfRangeException("Count cannot be negative");

                if (CurrentPassengers - count < 0)
                {
                    Console.WriteLine($"Невозможно высадить {count} пассажиров. В салоне только {CurrentPassengers}");
                }
                else
                {
                    CurrentPassengers -= count;
                    Console.WriteLine($"Высажено {count} пассажиров. Осталось в салоне: {CurrentPassengers}/{PassengerSeats}");
                }
            }

            // Загрузка багажа
            public void LoadTrunk(double volumeLiters)
            {
                if (volumeLiters < 0) throw new ArgumentOutOfRangeException("Volume cannot be negative");

                if (CurrentTrunkLoad + volumeLiters > TrunkCapacity)
                {
                    Console.WriteLine($"Невозможно загрузить {volumeLiters}л. Свободно: {TrunkCapacity - CurrentTrunkLoad}л");
                }
                else
                {
                    CurrentTrunkLoad += volumeLiters;
                    Console.WriteLine($"Багаж загружен: {CurrentTrunkLoad}/{TrunkCapacity} литров");
                }
            }

            // Разгрузка багажа
            public void UnloadTrunk(double volumeLiters)
            {
                if (volumeLiters < 0) throw new ArgumentOutOfRangeException("Volume cannot be negative");

                if (CurrentTrunkLoad - volumeLiters < 0)
                {
                    Console.WriteLine($"Невозможно разгрузить {volumeLiters}л. В багажнике только {CurrentTrunkLoad}л");
                }
                else
                {
                    CurrentTrunkLoad -= volumeLiters;
                    Console.WriteLine($"Багаж разгружен. Осталось: {CurrentTrunkLoad}/{TrunkCapacity} литров");
                }
            }

            // Переопределение стоимости поездки (легковой дешевле)
            public override double RoadTripCost(double distance, double fuelcost)
            {
                double baseCost = (distance * FuelConsumption) * fuelcost;

                // Дополнительные услуги увеличивают стоимость
                if (HasAirConditioner) baseCost += distance * 0.5;
                if (HasNavigationSystem) baseCost += 100; // Фиксированная плата за навигатор

                return baseCost;
            }

            // Переопределение информации
            public override string Info()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Легковой автомобиль: {Model}");
                sb.AppendLine($"Мест: {CurrentPassengers}/{PassengerSeats}");
                sb.AppendLine($"Багаж: {CurrentTrunkLoad}/{TrunkCapacity} л");
                sb.AppendLine($"Кондиционер: {(HasAirConditioner ? "Есть" : "Нет")}");
                sb.AppendLine($"Навигатор: {(HasNavigationSystem ? "Есть" : "Нет")}");
                return sb.ToString();
            }
        }
    }

}
