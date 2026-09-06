using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3
{
    internal class Program
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

        public static Encoding textCodingChoise(int choise)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (choise < 0 || choise > 3) { throw new ArgumentOutOfRangeException(); }
            if (choise == 1) { return Encoding.UTF8; }
            if(choise == 2) { return Encoding.GetEncoding(1251); }
            if(choise == 3) { return Encoding.GetEncoding(866); }
            else { throw new Exception(); }
        }
            static void Main(string[] args)
            {
                

                Console.WriteLine("Введите путь до директории в которую вы хотите сохранить файл.");
                string folderPath = Console.ReadLine();
                bool folderExist = userFolderPath(folderPath);
                if (!folderExist) { return; }


                Console.WriteLine("Введите название файла");
                string fileName = Console.ReadLine();
                fileName = userFileName(fileName);
                string file = Path.Combine(folderPath, fileName);

                Console.WriteLine("Выберите необходиму кодировку:");
                Console.WriteLine("1. UTF-8");
                Console.WriteLine("2. Windows-1251");
                Console.WriteLine("3. CP866");
                int textCode = int.Parse(Console.ReadLine());
                Encoding code = textCodingChoise(textCode);

                Console.WriteLine("Введите текст -> ");
                string userText = Console.ReadLine();
                File.WriteAllText(file, userText, code);

                long fileSize = new FileInfo(file).Length;

                Console.WriteLine("Вес созданного пользователем файла в байтах: " + fileSize);

            }
        }
    } 
