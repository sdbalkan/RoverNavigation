using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoverNavigation
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            string[] inputs = null;
            int x, y = 0;

            Grid grid = new Grid();
            //Grid koordinatı doğru girilene kadar devam eder
            while (true)
            {
                try
                {
                    Console.Write("Grid koordinatlarını giriniz : ");
                    //Grid koordinatı ekrandan okunur
                    input = Console.ReadLine();
                    //Koordinat değerleri ayrıştırılır
                    inputs = input.Split(' ');
                    //Koordinat değerleri atanır
                    x = Convert.ToInt32(inputs[0]);
                    y = Convert.ToInt32(inputs[1]);
                    if (x <= 0 || y <= 0)
                        throw new Exception("Koordinat değerleri 0'dan büyük olmalıdır");
                    grid.X = x;
                    grid.Y = y;
                    //Döngüden çıkar
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata : {0}", ex.Message);
                    Console.Write("Devam etmek için Enter tuşuna basınız...");
                    Console.ReadLine();
                }
            }

            //Ardı ardına yeni araç girişleri için işlemler tekrarlanır
            while (true)
            {
                Rover rover = new Rover(grid);
                
                //Araç pozisyonu doğru girilene kadar devam eder
                while (true)
                {
                    try
                    {
                        Console.Write("Aracın mevcut pozisyonunu giriniz : ");
                        //Aracın pozisyonu okunur
                        input = Console.ReadLine();
                        //Pozisyon değerleri ayrıştırılır
                        inputs = input.Split(' ');
                        //Pozisyon değerleri değişkenlere atanır
                        x = Convert.ToInt32(inputs[0]);
                        y = Convert.ToInt32(inputs[1]);
                        char direction = inputs[2][0];
                        //Pozisyon değerleri kontrol edilir
                        if (x < 0 || y < 0)
                            throw new Exception("Koordinat değerleri 0'dan küçük olamaz");
                        if (x > grid.X || y > grid.Y)
                            throw new Exception("Aracın pozisyonu belirlenen sınırların içinde olmalıdır");
                        if (!rover.CheckDirection(direction))
                            throw new Exception(string.Format("Tanımsız yön '{0}'", direction));
                        //Pozisyon değerleri atanır
                        rover.X = x;
                        rover.Y = y;
                        rover.Direction = direction;
                        //Döngüden çıkar
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata : {0}", ex.Message);
                        Console.Write("Devam etmek için Enter tuşuna basınız...");
                        Console.ReadLine();
                    }
                }

                //Araç kontrol komutları doğru girilene kadar devam eder
                while (true)
                {
                    try
                    {
                        Console.Write("Araç kontrol komut dizisini giriniz : ");
                        //Komut dizisi okunur
                        input = Console.ReadLine();
                        //Komut dizisi uzunluğu kontrol edilir
                        if (input.Length > 100)
                            throw new Exception("Komut dizisi uzunluğu 100 karakteri geçemez");
                        //Komutlar tek tek uygulanır
                        rover.Move(input);
                        //Döngüden çıkar
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata : {0}", ex.Message);
                        Console.Write("Devam etmek için Enter tuşuna basınız...");
                        Console.ReadLine();
                    }
                }

                //Aracın yeni pozisyonu ekrana yazılır
                Console.WriteLine("Aracın yeni pozisyonu : {0} {1} {2}", rover.X, rover.Y, rover.Direction);
                Console.Write("Devam etmek için Enter tuşuna basınız...");
                Console.ReadLine();
            }
        }
    }
}
