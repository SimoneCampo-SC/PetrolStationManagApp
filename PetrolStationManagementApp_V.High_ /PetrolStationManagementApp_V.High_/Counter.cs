using System;
using System.IO;
using System.Collections.Generic;
namespace PetrolStationManagementApp_V.High_
{
    public static class Counter
    {
        public static List<FuellingTransaction> TransactionList = new List<FuellingTransaction>();

        public const float COST_PER_LITRE = 1.7f; //Cost of one litre in £

        public static double TotalUnleaded { get; set; }
        public static double TotalLPG { get; set; }
        public static double TotalDiesel { get; set; }
        public static double TotalLitres { get; set; } = 0;
        public static double TotalMoney { get; set; } = 0;
        public static double Commission { get; set; } = 0; // 1% commission
        public static int ServedVehicles { get; set; } = 0; // Number of the vehicles serviced
        public static int UnservedVehicles { get; set; } = 0; // Number of unserviced vehicles

        /// <summary>
        /// This method Converts the Litres dispensed in Money base on the Const variable declared above
        /// </summary>
        /// <returns></returns>
        public static double CalculateMoney()
        {
            TotalMoney = Math.Floor(COST_PER_LITRE * TotalLitres);
            return TotalMoney;
        }

        /// <summary>
        /// This method calculate the 1% of the total money earned
        /// </summary>
        /// <returns></returns>
        public static double CalculateCommission()
        {
            Commission = TotalMoney * 0.01;
            return Math.Floor(Commission);
        }

        /// <summary>
        /// This method simply count the number of vehicle serviced
        /// </summary>
        public static void CountServedVehicles()
        {
            ServedVehicles++;
        }

        /// <summary>
        /// As this is a private state, its content is taken using this method
        /// </summary>
        /// <returns></returns>
        public static int GetServedVehicles()
        {
            return ServedVehicles;
        }

        /// <summary>
        /// This method simply count the number of vehicle serviced
        /// </summary>
        public static void CountUnservedVehicles()
        {
            UnservedVehicles++;
        }

        /// <summary>
        /// As this is a private state, its content is taken using this method
        /// </summary>
        /// <returns></returns>
        public static int GetUnservedVehicles()
        {
            return UnservedVehicles;
        }

        /// <summary>
        /// This method displays to the screen a transaction in a given position and then will write it in a file.csv 
        /// </summary>
        /// <param name="position">position of the transaction in the list</param>
        public static void DisplayList(int position)
        {
            FuellingTransaction t; // declares an instance of transaction 
            // use of the try-catch statement in order to catch any eventual exception which may occur during the operation 
            try
            {
                // The variable "t" previously declared is created assuming the value of the instance stored in the list "Transaction" at the "i" position
                t = Counter.TransactionList[position];
                Console.WriteLine();
                Console.WriteLine("{0}. Vehicle Type: {1},      Fuel Type: {2},     Number of litres: {3},      Pump number: {4}; ", position + 1, t.vehicleType, t.fuelType, t.numberOfLitres, t.pumpNumber); // display to the user the states of the Instance t
                // Creation of a new onject of the class Streamwriter (name of the file.csv, does not override the file)
                StreamWriter writer = new StreamWriter("Transaction List.csv", true);
                // The intestation needs to be written just one time and this if statement checks whether it is the first time
                if (position == 0)
                {

                    writer.WriteLine("Transaction No, Vehicle Type, Fuel Type, Number of Litres, Pump Number");
                }
                // In order to avoid that a file is written many times in the program, it checks its  boolean property to see whether it's been already written 
                if (t.FilledInTheFile == false)
                {
                    // Write information on the file
                    writer.WriteLine("{0}, {1}, {2}, {3}, {4}", position + 1, t.vehicleType, t.fuelType, t.numberOfLitres, t.pumpNumber);
                    // Set propery = true so that it will not written anymore
                    t.FilledInTheFile = true;
                    // Close the file 
                    writer.Close(); 
                }
           }
           catch (Exception e)
           {
                // if an exception is found, its Message property will be displayed on the screen.
                Console.WriteLine(e.Message);
           }
        }
    }
}
