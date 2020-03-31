using System;

namespace ДЗ_2_Multiply
{
    class Program
    {
        public static int RequestNumber() //Функция запрашивает строку и проверяет возможность преобразования в Int32
        {
            var collectedLine = Console.ReadLine();
            int collectedNumber;

            for (bool i = false; i == false;)
            {
                try { collectedNumber = Convert.ToInt32(collectedLine); i = true; }
                catch
                {
                    Console.WriteLine("Введено некорректное значение, попробуйте ещё раз!");
                    collectedLine = Console.ReadLine(); i = false;
                }
            }
            collectedNumber = Convert.ToInt32(collectedLine);
            return collectedNumber;
        }



        public static void DisplayMultiplyTable(int aNumber)

        {
            for(int i = 0; i <10; ++i) //Выводим таблицу умножения
            {
                Console.WriteLine(i + "*" + aNumber + "=" +aNumber*i);
            }
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");
            Console.WriteLine("Введите число от 1 до 9");

            int aNumberInputed = 0; 
            for (bool i = true; i == true;)//Проверяем, входит ли число в заданный дипазон и записываем его в переменную
            {
                aNumberInputed = RequestNumber();

                if (aNumberInputed > 9 || aNumberInputed < 1) { Console.WriteLine("Данная программа не понимает таких чисел, попробуйте ещё раз!"); }
                else { i = false; }
            }
            DisplayMultiplyTable(aNumberInputed); //Использую созданный метод
        }
    }
}
