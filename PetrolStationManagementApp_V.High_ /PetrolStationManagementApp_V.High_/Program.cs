using System;
using System.Timers;

namespace PetrolStationManagementApp_V.High_
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialise();

            Timer timer = new Timer();
            timer.Interval = 1099; // 1099 milliseconds: as lower is this time, as faster is the animation
            timer.AutoReset = true; // repeat in loop every 2 seconds
            timer.Elapsed += (sender, e) =>
            {   // when the timer elapsed, it executes the RunProgramLoop method
                RunProgramLoop(timer);
            }; // when the timer elapsed, it executes the RunProgramLoop method
            timer.Start();
            Console.ReadLine();
        }
        /// <summary>
        /// This method calls all the methods in the other classes and representing the general flow of execution of the program 
        /// </summary>
        static void RunProgramLoop(Timer timer)
        {
            Console.Clear(); // Clear the console
            Display.DrawIntestation();
            Display.DrawVehicles();
            Console.WriteLine();
            Console.WriteLine();
            Data.AssignVehicleToPump();
            Display.DrawPumps();
            Display.DrawCounters();
            Display.CloseApp(timer);

            // The order is:
            // 1. Clear the console;
            // 2. Dislay to the console vehicles created during the initialisation;
            // 3. Leave space so the console looks tidy;
            // 4. Assign the fist vehicle in the list to the fist available Pump;
            // 5. Display to the console the Pumps with their actual status;
            // 6. Close the App if the user requested it;
        }
    }
}