using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;


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

/////////////////////////////////////////////////////////////////////////////////////////////////////////////
      
        static void GetTrainInfo( List<TrainElement> train) //Function calcutes parametrs about whole train and displays it in console
        {
            int PassangerCapacity = 0;
            int BaggageCpapcity = 0;
            int TrainMass = 0;
            int MaxLoad = 0;


            foreach (TrainElement car in train) //Starting a cycle for TrainElements
            {
                TrainMass += car.getMass(); //Calulating mass of whole train

                if ((car as TrainElement) == (car as Carriage)) //Check if this TrainElement is also a Carriage(is it proper way to do this?)
                { 
                PassangerCapacity += (car as Carriage).GetHumanCapacity(); //Calculating passangers and baggage capacity
                BaggageCpapcity += (car as Carriage).GetBaggageCapacity();
                }

                if ((car as TrainElement) == (car as LoadCar)) //Same for LoadCar
                {
                    MaxLoad += (car as LoadCar).GetMaxLoadWeight(); // Calculating max load
                }
            }

            Console.WriteLine("The train contains " + train.Count + " cars"); //Displaying info
            Console.WriteLine("Train mass is " + TrainMass + " kilogramms");
            Console.WriteLine("Passanger capacity is " + PassangerCapacity + " people");
            Console.WriteLine("Baggage capacity is " + BaggageCpapcity + " kilogramms");
            Console.WriteLine("Train's max load is " + MaxLoad + " kilogramms");
            Console.WriteLine("The power of locomotive is " + (train.First() as Locomotive).GetEnginePower() + " Watt");
            Console.WriteLine("");

        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        static void SelectCars(List<TrainElement> train, int Input) //Functions finds  cars that capable of containing given amount of people
        {
            bool CarIsFound = false; // Bool to check if at least one car was found

            for (int i = 0; i < train.Count; i++)
            {
                if ((train[i] as TrainElement) == (train[i] as Carriage) && (train[i] as Carriage).GetHumanCapacity() > Input) //Checks car as Carriage type and if its capability enough
                {
                    Console.WriteLine("№" + i); //Dsiplys cars and change bool
                    CarIsFound = true;
                }
            }

            if (!CarIsFound) { Console.WriteLine("There is no sufficient cars, sorry =("); }//Diplays sorry message if the bool is false
        }

  ///////////////////////////////////////////////////////////////////////////////////////////
      
        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");

            var Reader = new StreamReader("../../../TrainsTypesList.txt"); //Create StreamReader object

            Console.WriteLine(Reader.ReadLine()); //a test

            Console.WriteLine("Enter the number of cars in train.");
            int usersInput = RequestNumber(); // usersInput is used to hold last user's input, apparently
            

            if(usersInput > 150) //This is limitation for train's lenght
            {
                Console.WriteLine("Wow, this train is too big! Let's lower it to 100 cars.");
                usersInput = 100;
            }

            List < TrainElement > Train = new List<TrainElement>(); //Creating TrainElements objects

            EconomyClassSleeper ECS1 = new EconomyClassSleeper(10000, 60, 150);
            EconomyClassSleeper ECS2 = new EconomyClassSleeper(9000, 50, 140);

            CompartmentSleeper CS1 = new CompartmentSleeper(11000, 40, 130);
            CompartmentSleeper CS2 = new CompartmentSleeper(10000, 30, 140);

            FirstClassSleeper FCS1 = new FirstClassSleeper(7000, 15, 100);
            FirstClassSleeper FCS2 = new FirstClassSleeper(8000, 20, 90);

            LoadCar LC1 = new LoadCar(7000, 30000);
            LoadCar LC2 = new LoadCar(9000, 35000);

            Locomotive Loco = new Locomotive(20000, 12000);


            Random rnd = new Random(); //fill the train with random cars
            for (int i=0; i < usersInput - 1; i++)
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


            Train.OrderBy(Train => Train.comfortLevel); // Order them by comfort level
            Train.Add(Loco); // And add a locmotive at the end of the train
            Train.Reverse(); // Reverse it
            Train.Count(); // Finish the LINQ sequence

            Console.WriteLine("Train is generated!");
            Console.WriteLine("");

            for (bool On = true; On == true;) //Start the  programm loop
            {
                Console.WriteLine("");
                Console.WriteLine("Enter in (1) to get info about train, (2) to get info about choosen car," );
                Console.WriteLine("(3) to find cars according to amount of passangers, (0) to exit the programm.");
                Console.WriteLine("");

                usersInput = RequestNumber();
             
                     switch(usersInput) // Do the command according to user's input
                     {
                       case 1: //Get info about train
                           GetTrainInfo(Train);
                           break;
                     

                       case 2: //Get info about chosen car
                           Console.WriteLine("Enter the number of car");
                           usersInput = RequestNumber();
                        if (usersInput > Train.Count )  // Check if car is in range of the train
                                { Console.WriteLine("Invalid Number."); }
                           else {
                                 Train[usersInput - 1].GetInfo(usersInput); 
                                }
                          
                        break;
                     

                       case 3: //Find sufficient car
                           Console.WriteLine("Enter the amount of passangers");
                           usersInput = RequestNumber();
                           Console.WriteLine("Next carriages are sufficient:");
                           SelectCars(Train, usersInput);
                           break;
                     

                       case 0: //Exit
                           Console.WriteLine("Bye bye!");
                           On = false;
                           break;
                     

                       default:
                           Console.WriteLine("Wrong number, try again!");
                           break;
                     }
             
            }
        }
    }
}
