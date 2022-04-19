using System;
using System.IO;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте!");
            Base atelier = new Base();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите желаемое действие:\n1 - Добавление записи в файл\n" +
                    "2 - Вывод записей из файла\n3 - Вывод общего количество записей в файле\n4 - Вывести услугу, которую вы можете себе позволить" +
                    "\n5 - Вывести общую сумму услуг в  Ателье\n6 - Очистить имеющиеся записи из файла\n7 - Завершить работу\n");
                int num = Convert.ToInt32(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Введите код: ");
                        int code = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите название услуги: ");
                        string nameusl = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Введите цену за услугу: ");
                        double price = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите краткое описание услуги: ");
                        string opisanie = Convert.ToString(Console.ReadLine());
                        atelier.Read(code, nameusl, price, opisanie); break;
                    case 2:
                        Console.Clear();
                        atelier.show();
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        atelier.showGeneral();
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Введите ваш бюджет ");
                        double sum = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Услуги доступные с вашим бюджетом"); 
                        atelier.showService(sum);
                            Console.ReadKey();
                    
                             
                        break;
                    case 5:
                        Console.Clear();
                        atelier.showAmmount();
                        Console.ReadKey();
                        break;
                  
                    case 6:
                        atelier.ClearFile();
                        break;
                    case 7: return; break;
                }
            }
        }
    }
    public struct AtelieUsluga
    {
        public int code; // код услуги
        public string nameusl; // название услуги
        public double price; // цена за услугу
        public string opisanie; // описание услуги
        public AtelieUsluga(int c, string n, double p, string o)
        {
            code = c;
            nameusl = n;
            price = p;
            opisanie = o;
        }
    }
    class Base
    {
        AtelieUsluga[] usluga = new AtelieUsluga[1];
        string Path = "Usluga.txt";
        int count = 0;
        public void Read(int code, string nameusl, double price, string opisanie) // метод для записи данных в бинарный файл
        {
            usluga[count].code = code;
            usluga[count].nameusl = nameusl;
            usluga[count].price = price;
            usluga[count].opisanie = opisanie;
         
            Array.Resize(ref usluga, usluga.Length + 1);
            using (BinaryWriter writer = new BinaryWriter(File.Open(Path, FileMode.Append)))
            {

                writer.Write(usluga[count].code);
                writer.Write(usluga[count].nameusl);
                writer.Write(usluga[count].price);
                writer.Write(usluga[count].opisanie);
            
            }
            count++;
        }
        public void show() // метод для чтения записей из бинарного файла и вывода записей в консоль
        {
            using (BinaryReader br = new BinaryReader(File.Open(Path, FileMode.Open)))
            {
                int i = 0;
                while (br.PeekChar() > -1)
                {
                    usluga[i].code = br.ReadInt32();
                    usluga[i].nameusl = br.ReadString();
                    usluga[i].price = br.ReadDouble();
                    usluga[i].opisanie = br.ReadString();
                    
                    Array.Resize(ref usluga, usluga.Length + 1);
                    Console.WriteLine($"Код услуги: { usluga[i].code} | Название услуги: { usluga[i].nameusl} | Цена за услугу: {usluga[i].price} | Описание услуги: {usluga[i].opisanie}");
                    i++;
                }
            }
        }
        public void showGeneral() // метод, предназначенный для вывода общего количества записей в файле
        {
            
            using (BinaryReader br = new BinaryReader(File.Open(Path, FileMode.Open)))
            {
                int i = 0;
                while (br.PeekChar() > -1)
                {
                    usluga[i].code = br.ReadInt32();
                    usluga[i].nameusl = br.ReadString();
                    usluga[i].price = br.ReadDouble();
                    usluga[i].opisanie = br.ReadString();
                    Array.Resize(ref usluga, usluga.Length + 1);
                    
                    i++;
                }
                Console.WriteLine("Количество записей в базе данных – " + i);
            }
            
        }
        public void showService(double sum) // метод, предназначенный для вывода в консоль данных, которые удовлетворяют условию
        {
            int number = 0;
            using (BinaryReader br = new BinaryReader(File.Open(Path, FileMode.Open)))
            {
                int i = 0;
                while (br.PeekChar() > -1)
                {

                    usluga[i].code = br.ReadInt32();
                    usluga[i].nameusl = br.ReadString();
                    usluga[i].price = br.ReadDouble();
                    usluga[i].opisanie = br.ReadString();
                    Array.Resize(ref usluga, usluga.Length + 1);
                    if (usluga[i].price <= sum) // вывод записей в которых указанная сумма больше услуги ( т.е услуги, которые мы можем себе позволить имея определенную сумму)
                    {
                        number++;
                        Console.WriteLine($"Код услуги: { usluga[i].code} | Название услуги: { usluga[i].nameusl} | Цена за услугу: {usluga[i].price} | Описание услуги: {usluga[i].opisanie}");
                        Console.WriteLine("\n");
                    }
                    i++;
                }
            }
           
        }
        public void showAmmount() // метод, предназначеный для вывода суммы всех услуг, имеющихся в ателье
        {
            int i = 0;
            double sum=0;
            using (BinaryReader br = new BinaryReader(File.Open(Path, FileMode.Open)))
            {
                
                while (br.PeekChar() > -1)
                {
                    usluga[i].code = br.ReadInt32();
                    usluga[i].nameusl = br.ReadString();
                    usluga[i].price = br.ReadDouble();
                    usluga[i].opisanie = br.ReadString();
                    Array.Resize(ref usluga, usluga.Length + 1);
                    sum = sum + usluga[i].price;
                    i++;
                }
                

            }
            Console.WriteLine("Ателье имеет {0} услуг на общую сумму {1}", i, sum);
        }
        public void ClearFile() // метод, предназначенный для очистки данных из файла
        {
            File.WriteAllText(Path, "");
        }
    }
}