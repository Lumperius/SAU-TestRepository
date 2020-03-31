using System;

namespace ДЗ_2
{
    class Program
    {
        public static int RequestNumber() //Функция запрашивает строку и проверяет возможность преобразования в Int32
        {
            var collectedLine = Console.ReadLine();
            int collectedNumber;

            for (bool i = false; i == false;) 
            { 
            try { collectedNumber = Convert.ToInt32(collectedLine);  i=true;}
            catch { Console.WriteLine("Введено некорректное значение, попробуйте ещё раз!");
                    collectedLine = Console.ReadLine(); i = false; }
            }
            collectedNumber = Convert.ToInt32(collectedLine); 
            return collectedNumber;
        }
       
        
        public static void CheckDayOfWeek(int NumberOfDay)
        {
           
          
          
            
                switch(NumberOfDay) //Выводим день недели в зависимости от числа
                {
                    case 1:
                        Console.WriteLine("Это понедельник.");
                        break;
                    case 2:
                        Console.WriteLine("Это вторник.");
                        break;
                    case 3:
                        Console.WriteLine("Это среда.");
                        break;
                    case 4:
                        Console.WriteLine("Это четверг.");
                        break;
                    case 5:
                        Console.WriteLine("Это Акула! Шучу, это пятница.");
                        break;
                    case 6:
                        Console.WriteLine("Это суббота.");
                        break;
                    case 7:
                        Console.WriteLine("Это воскресенье.");
                        break;
                    default:
                    Console.WriteLine("Нет такого дня недели. =( ");
                    break;
                }
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");
            Console.WriteLine("Введите число от 1 до 7.");
            var NumberOfDayRequested = RequestNumber(); //Получаем число от пользователя
            CheckDayOfWeek(NumberOfDayRequested);
        }
    }
}
