using System;
using System.Timers;

namespace PetrolStationManagementApp_V.High_
{
    public class Display
    {
        /// <summary>
        /// This method display on the console the queue of vehicles generated with the appropriate states
        /// </summary>
        public static void DrawIntestation()
        {
            Console.BackgroundColor = ConsoleColor.Black; // Set the Console Background to black
            Console.ForegroundColor = ConsoleColor.Yellow; // Set the foreground to yellow

            // Display intestation 
            DisplayCentre("#####              ##                     ##      ####    ##               ##    ##                   ", 1, 2);
            DisplayCentre("##  ##   #####    #####  ##  ##    ###    ##     ##  ##  #####   ######   #####        ###    ######  ", 2, 2);
            DisplayCentre("##  ##  ##    ##   ##    ######  ##   ##  ##      ##      ##    #     ##   ##    ##  ##   ##  ##   ## ", 3, 2);
            DisplayCentre("####    ########   ##    ##      ##   ##  ##       ##     ##     #######   ##    ##  ##   ##  ##   ## ", 4, 2);
            DisplayCentre("##      ##     #   ##    ##      ##   ##  ##    ##  ##    ##    ##    ##   ##    ##  ##   ##  ##   ## ", 5, 2);
            DisplayCentre("##       ######     ###  ##       ####    ##     ####      ###   #######    ###  ##    ###    ##   ## ", 6, 2);
            Console.ForegroundColor = ConsoleColor.Red; // Set the foreground to red
            DisplayCentre("------------------------------------------------------------------------------------------------------", 7, 2);
            DisplayCentre("------------------------------------------------------------------------------------------------------", 9, 2);
            Console.ForegroundColor = ConsoleColor.Yellow; // Set the foreground to yellow
            DisplayCentre("M    A    N    A    G    E    M    E    N    T        A    P    P", 8, 2);
        }
        /// <summary>
        /// This method display vehicles created on the console
        /// </summary>
        public static void DrawVehicles()
        {
            Vehicle v; // Declaration of the variable "v" of the type Vehicle
            Console.ForegroundColor = ConsoleColor.Yellow; // Set the foreground to yellow
            Console.WriteLine();
            Console.WriteLine();
            DisplayCentre(" ---------------- ", 11, 2);
            DisplayCentre("| VEHICLES QUEUE |", 12, 2);
            DisplayCentre(" ---------------- ", 13, 2);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray; // Set the foreground to gray
            Console.Write("<<|| ");
            // Execute the following instuctions until the iterator reaches the end of the list "vehicle" in the class "Data"
            for (int i = 0; i < Data.vehicles.Count; i++)
            {
                // The variable "v" previously declared is created assuming the value of the instance stored in the list "vehicle" at the "i" position
                v = Data.vehicles[i];
                Console.Write("{0}. Vehicle: {1}, Fuel: {2} || ", v.carID, v.VehicleType, v.FuelType.MyFuel); // display to the user the states of the Instance v
            }
        }

        /// <summary>
        /// This method display on the console the pumps with their actual status (FREE | BUSY)
        /// </summary>
		public static void DrawPumps()
        {
            int widthPump; // hold the coordinate x to display the pump status (FREE | BUSY)
            int heigthPump; // hold the coordinate y to display the pump status (FREE | BUSY)
            Pump p;  // Create object "p" of the type Pump

            Console.ForegroundColor = ConsoleColor.Yellow; // Set the foreground to yellow
            DisplayCentre(" ---------------- ", 17, 2);
            DisplayCentre("|   FOURECOURT   |", 18, 2);
            DisplayCentre(" ---------------- ", 19, 2);
            Console.WriteLine();

            // These for loops display the the grid of the forecourt
            for (int width = 48; width < Console.WindowWidth - 48; width++)
            {
                for (int heigth = 21; heigth < 43; heigth++)
                {
                    if ((heigth == 21) | (heigth == 28) | (heigth == 35) | (heigth == 42)) 
                    {
                        Console.SetCursorPosition(width, heigth);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("="); // Draw the end of the line
                    }
                    if ((width == 48) | (width == 84) | (width == 120) | (width == 155)) 
                    {
                        Console.SetCursorPosition(width, heigth);
                        if ((heigth == 24) || (heigth == 31) || (heigth == 38)) 
                        {
                            // Draw the arrow which display the direction of vehicles in the forecourt
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(@"====\");
                            Console.SetCursorPosition(width, heigth+1);
                            Console.WriteLine(@"====/");
                        }
                    }
                }
            }
            Console.WriteLine();
            // Execute the following instructions 9 times
            for (int i = 0; i < Data.pumps.Count; i++)
            {
                // Object "p" assumes the value of the object stored in the list "pump" at the "i" position
                p = Data.pumps[i];

                // Set the Cursor where to show the pump numer and get the coordinates where to show the pump status
                switch(i)
                {
                    case 0:
                        Console.SetCursorPosition(66, 23);
                        widthPump = 66;
                        heigthPump = 25;
                        break;
                    case 1:
                        Console.SetCursorPosition(102, 23);
                        widthPump = 102;
                        heigthPump = 25;
                        break;
                    case 2:
                        Console.SetCursorPosition(138, 23);
                        widthPump = 138;
                        heigthPump = 25;
                        break;
                    case 3:
                        Console.SetCursorPosition(66, 30);
                        widthPump = 66;
                        heigthPump = 32;
                        break;
                    case 4:
                        Console.SetCursorPosition(102, 30);
                        widthPump = 102;
                        heigthPump = 32;
                        break;
                    case 5:
                        Console.SetCursorPosition(138, 30);
                        widthPump = 138;
                        heigthPump = 32;
                        break;
                    case 6:
                        Console.SetCursorPosition(66, 37);
                        widthPump = 66;
                        heigthPump = 39;
                        break;
                    case 7:
                        Console.SetCursorPosition(102, 37);
                        widthPump = 102;
                        heigthPump = 39;
                        break;
                    case 8:
                        Console.SetCursorPosition(138, 37);
                        widthPump = 138;
                        heigthPump = 39;
                        break;
                    default:
                        widthPump = 0;
                        heigthPump = 0;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("PUMP {0}", i + 1); // Display pump number

                // Check if the return value of IsAvailable() p’s method is true
                if (p.IsAvailable()) 
                {
                    Console.SetCursorPosition(widthPump, heigthPump);
                    Console.ForegroundColor = ConsoleColor.Green; // When it is free, display in green
                    Console.Write("FREE");
                }
                else
                {
                    // Since the vehicle has not been relased, the status can be either:
                    // - BUSY: The vehicle is using the pump;
                    // - BLOCK: The vehicle has finished but it can't go out because of the other vehicles which block the lane
                    // Checks whether the property Blocked is set to true or to false
                    if (p.Blocked)
                    {
                        Console.SetCursorPosition(widthPump, heigthPump);
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // When it is blocked, display in dark yellow
                        Console.Write("BLOCK");
                    }
                    else
                    {
                        Console.SetCursorPosition(widthPump, heigthPump);
                        Console.ForegroundColor = ConsoleColor.Red; // When it is busy, display in red
                        Console.Write("BUSY");
                    }
                }
            }
        }

        /// <summary>
        /// this method displays the counters and the recent transactions 
        /// </summary>
        public static void DrawCounters()
        {
            // Draw the divisor from counters and recent transaction
            for (int width = 46; width < 59; width++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(102, width);
                Console.Write("|");
            }
            // Draw intestation in yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            DisplayCentre(" ------------------------------ ", 46, 4);
            DisplayCentre("|     RECENT TRANSACTIONS      |", 47, 4);
            DisplayCentre(" ------------------------------ ", 48, 4);
            Console.SetCursorPosition(138, 46);
            // Draw intestation in yellow
            Console.WriteLine(" ------------- ");
            Console.SetCursorPosition(138, 47);
            Console.WriteLine("|   DETAILS   |");
            Console.SetCursorPosition(138, 48);
            Console.WriteLine(" ------------- ");
            Console.ForegroundColor = ConsoleColor.Gray;
            // This if statement will allow viewing of the last 5 transaction only soas not to clog the Console
            if (Counter.TransactionList.Count <= 5)
            {
                for (int i = 0; i < Counter.TransactionList.Count; i++)
                {
                    Counter.DisplayList(i);
                }
            }
            else
            {
                // for loop will start from the fifth last transaction
                for (int i = Counter.TransactionList.Count - 5; i < Counter.TransactionList.Count; i++)
                {
                    Counter.DisplayList(i);
                }
            }
            // Displays on the console all the following counters
            Console.SetCursorPosition(115, 50);
            Console.WriteLine("* Fuel dispensed: {0}L", Counter.TotalLitres);
            Console.SetCursorPosition(115, 52);
            Console.WriteLine("* Unleaded dispensed: {0}L", Counter.TotalUnleaded);
            Console.SetCursorPosition(115, 54);
            Console.WriteLine("* Diesel dispensed: {0}L", Counter.TotalDiesel);
            Console.SetCursorPosition(115, 56);
            Console.WriteLine("* LPG dispensed: {0}L", Counter.TotalLPG);
            Console.SetCursorPosition(155, 50);
            Console.WriteLine("* Money earned: £{0} (£{1}/L)", Counter.CalculateMoney(), Counter.COST_PER_LITRE);
            Console.SetCursorPosition(155, 52);
            Console.WriteLine("* Total Commission (1%): £{0}", Counter.CalculateCommission());
            Console.SetCursorPosition(155, 54);
            Console.WriteLine("* Number of vehicles serviced: {0}", Counter.GetServedVehicles());
            Console.SetCursorPosition(155, 56);
            Console.WriteLine("* Number of vehicles unserviced: {0}", Counter.GetUnservedVehicles());
        }
        public static void CloseApp(Timer timer)
        {
            // Display to the user if he wants to close the App
            DisplayCentre("Press 'X' to close the App.", 61, 2);
            string close = Console.ReadLine().ToUpper();
            if (close == "X") 
            {
                // Do not repeat
                timer.Enabled = false;
            }
            else
            {
                // Keep repeating
                timer.Enabled = true;
            }
        }

            /// <summary>
            /// This method take a string a string of text and displays it in the center of the portion of the console
            /// </summary>
            /// <param name="text">string to show</param>
            /// <param name="width">at the width needed</param>
            /// <param name="portion">how many portion the console is divided </param>
            public static void DisplayCentre(string text, int width, int portion)
        {
            int winWidth = (Console.WindowWidth / portion);
            Console.SetCursorPosition(winWidth - (text.Length / 2), width);
            Console.WriteLine(text);
        }
    }
}




