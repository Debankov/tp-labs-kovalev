using Taylor.App;
using Taylor.Core;

namespace Taylor
{
   internal class Programm
    {

        public static bool userFolderPath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    throw new ArgumentException("Заданной директории не существует.");
                }
                return Directory.Exists(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public static string userFileName(string fileName)
        {
            if (fileName.Length > 20) { throw new ArgumentOutOfRangeException("Длина названивая файла слишком большая."); }
            fileName = fileName.ToLower();
            string res = fileName.Trim();
            return res + ".txt";

        }
        public static void Main(string[] args)
        {
            while (true)
            {
                try 
                {
                Console.WriteLine("Выберите предпочитаемый способ вывода расчётов: \n1. Терминал\n2. Файл");
                int output_choise = int.Parse(Console.ReadLine());
                    IResultStorage storage;
                    switch (output_choise)
                    {
                        case 1:
                            storage = new ConsoleResultStorage();
                            break;
                        case 2:
                            Console.WriteLine("Введите путь до директории в которую вы хотите сохранить файл.");
                            string folderPath = Console.ReadLine();
                            bool folderExist = userFolderPath(folderPath);
                            if (!folderExist) { return; }


                            Console.WriteLine("Введите название файла");
                            string fileName = Console.ReadLine();
                            fileName = userFileName(fileName);
                            string file = Path.Combine(folderPath, fileName);
                            storage = new FileResultStorage(file);
                            break;
                        default:
                            throw new ArgumentException("Неизвестный способ вывода.");
                    }


                Console.WriteLine("В чем измеряем? \n1. Градусы\n2. Радианы");
                int value_choise = int.Parse(Console.ReadLine());
                bool is_degres;
                    switch (value_choise)
                    {
                        case 1:
                            is_degres = true;
                            break;
                        case 2:
                            is_degres = false;
                            break;
                        default:
                            throw new ArgumentException("Необходимо выбрать 1 или 2.");
                    }

                Console.WriteLine("Введите искомое значение: ");
                double value = double.Parse(Console.ReadLine());
                if (!is_degres){value = TaylorTriginometry.DegreesToRadians(value);}


                Console.WriteLine("Выберите функицию:\n1. Cos\n2. Sin\n3. Ctg\n4. Tg");
                
                    int func_choise = int.Parse(Console.ReadLine());
                    TaylorTriginometry calc = new TaylorTriginometry(storage);
                    switch (func_choise)
                    {
                        case 1:
                            calc.CalculateCos(value);
                            break;
                        case 2:
                            calc.CalculateSin(value);
                            break;
                        case 3:
                            calc.CalculateCot(value);
                            break;
                        case 4:
                            calc.CalculateTan(value);
                            break;
                        default :
                            throw new ArgumentException("Некорректный ввод");
                    }

                    Console.ReadLine();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); break; }

                Console.WriteLine("Нажмите любою кнопку для пролжения или <exit> для выхода");
                string exit = Console.ReadLine();
                if(exit == "exit") { break; }
                continue;

            }
      
        }
    }
    
}