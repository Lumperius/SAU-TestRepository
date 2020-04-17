using System;
using System.Collections;
using System.Collections.Generic;


namespace HomeWork_3
{
    class Program
    {


        public static int RequestNumber() //Function requsts line, transforms it to int and returns the result
        {
            var collectedLine = Console.ReadLine();
            int collectedNumber;

            for (bool i = false; i == false;)
            {
                try { collectedNumber = Convert.ToInt32(collectedLine); i = true; }
                catch
                {
                    Console.WriteLine("This input is invalid, try again!");
                    collectedLine = Console.ReadLine(); i = false;
                }
            }
            collectedNumber = Convert.ToInt32(collectedLine);
            return collectedNumber;
        }


        static void ComposeTrain( int trainCapacity, int numberOfCarriages)
        {

        }
      
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");

            List < TrainElement > Train = new List<TrainElement>();

            EconomyClassSleeper ECS1 = new EconomyClassSleeper(10000, 60, 150);
            EconomyClassSleeper ECS2 = new EconomyClassSleeper(9000, 50, 140);

            CompartmentSleeper CS1 = new CompartmentSleeper(11000, 40, 130);
            CompartmentSleeper CS2 = new CompartmentSleeper(10000, 30, 140);

            FirstClassSleeper FCS1 = new FirstClassSleeper(7000, 15, 100);
            FirstClassSleeper FCS2 = new FirstClassSleeper(8000, 20, 90);

            Load_Car LC1 = new Load_Car(7000, 30000);
            Load_Car LC2 = new Load_Car(9000, 35000);


            Locomotive Loco = new Locomotive(20000, 12000);


            Random rnd = new Random();
            for (int i=0; i <= 50; i++)
            {
                int type = rnd.Next(1, 8);
                switch (type)
                {
                    case 1:
                        Train.Add(ECS1);
                        break;
                    case 2:
                        Train.Add(ECS2);
                        break;
                    case 3:
                        Train.Add(CS1);
                        break;
                    case 4:
                        Train.Add(CS2);
                        break;
                    case 5:
                        Train.Add(FCS1);
                        break;
                    case 6:
                        Train.Add(FCS2);
                        break;
                    case 7:
                        Train.Add(LC1);
                        break;
                    case 8:
                        Train.Add(LC2);
                        break;

                }
            }
                        Train.Add(Loco);


        }
    }
}
