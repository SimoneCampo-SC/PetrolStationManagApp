using System;
using System.Collections.Generic;
using System.Timers;

namespace PetrolStationManagementApp_V.High_
{
    class Data
    {
        private static Timer timer; // Creation of a new object of the class Timer
        private static Random random; //  Creation of a new object of the class Random
        public static List<Vehicle> vehicles; // Creation of a List of the instances of the class Vehicle 
        public static List<Pump> pumps; // Creation of a List of the instances of the Class Pump

        /// <summary>
        /// This method actually calls the methods initialise both Pumps and Vehicles
        /// </summary>
        public static void Initialise()
        {
            InitialisePumps();
            InitialiseVehicles();
        }

        /// <summary>
        /// This method creates 9 objects of the class Pump and stores them in the List declared above 
        /// </summary>
        private static void InitialisePumps()
        {
            pumps = new List<Pump>(); // Initialization of the list declared above with the name of "pumps"

            Pump p; // Creation of the new object of the type Pump

            // In this for loop are initialised 9 instances of the Pump Class and then added to the list. 
            for (int i = 0; i < 9; i++)
            {
                p = new Pump(); // Initialization of the object created before
                pumps.Add(p); // adding the created instance to the list "pumps"
            }
        }

        /// <summary>
        /// This method initialized a list containing the queue of vehicles which are going to be created and,
        /// at a certain interval, calls the method which actually creates the objects
        /// </summary>
        private static void InitialiseVehicles()
        {
            vehicles = new List<Vehicle>(); // Initialization of the list declared above with the name of "vehicles"
            random = new Random(); // Initialization of the instance "random"
            timer = new Timer(); // Initialization of the instance "timer"
            timer.AutoReset = true; // keep repeating 
            timer.Elapsed += CreateVehicle; // when the time is elapsed, the method "Createvehicle()" is execute
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// This method actually generates objects of the class Vehicle and then, it stores them in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CreateVehicle(object sender, ElapsedEventArgs e) //parameters needed for a method called in timer
        {
            Vehicle v;
            Random random = new Random();

            timer.Interval = random.Next(1500, 2201); // interval: random number between 1500 and 2200 milliseconds
            if (vehicles.Count < 5) // As only five vehicles can wait, vehicles will be created only if the lane is not full
            {
                switch (random.Next(3))
                {
                    case 0:
                        v = new Car(); // Create a new Car which interith vehicle
                        ManageVehicles(vehicles, v);
                        break;

                    case 1:
                        v = new Van(); // Create a new Van which interith vehicle
                        ManageVehicles(vehicles, v);
                        break;

                    case 2:
                        v = new HGV(); // Create a new HGV which interith vehicle
                        ManageVehicles(vehicles, v);
                        break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// This method manages the operations of a Vehicle in the list
        /// </summary>
        /// <param name="vehicles"></param>
        /// <param name="v"></param>
        public static void ManageVehicles(List<Vehicle> vehicles, Vehicle v)
        {
            vehicles.Add(v); // add object to the list of vehicles
            // delete a vehicle when its Waiting Time elapses
            timer = new Timer(); // Initialization of the instance "timer"
            timer.Interval = v.WaitingTime; // timer of the  Waiting Time
            timer.AutoReset = false; // do not repeat
            timer.Elapsed += (sender, e) =>
            {   // when the time is elapsed, the vehicle is removed.
                RemoveVehicle(v);
            };
            timer.Enabled = true;
            timer.Start();
        }
        /// <summary>
        /// This method will remove a vehicle passed by parameter and notice the counter of uncerviced vehicles
        /// </summary>
        /// <param name="v">vehicle which needs to be removed</param>
        private static void RemoveVehicle(Vehicle v)
        {
            bool AllOk = false;

            // Remove the vehicle from the list 
            AllOk = vehicles.Remove(v);
            if (AllOk == true) 
            {
                Counter.CountUnservedVehicles(); // Counts the number of unserviced vehicles
            }
        }
        /// <summary>
        /// This method decides which is the best available pump  
        /// </summary>
        public static void AssignVehicleToPump()
        {
            Vehicle v; // creation of the object "v" of the class Vehicle
            Pump p; // creation of the object "p" of the class Pump
            int lane = 0;

            int lastAvailable = 10; // To this variable, which holds the last available pump, has been assigned a default number 10.

            // repeat the following instructions for each pump (9 times)
            for (int i = 0; i < pumps.Count; i++)
            {
                p = pumps[i]; // The object is initialised assuming the values stored in the "pumps" list at the "i" position

                // Checks whether the pump p is available
                if (p.IsAvailable())
                {
                    lastAvailable = i; //  it assumes the number of the integer which belongs to the free pump 
                }

                // Checks whether the lane is finished so it can check for the pump
                if ((i % 3) == 2)
                {
                    // Checks whether the variable has been modified
                    // If so, it would contains the position of the last available pump in the lane
                    // AND checks whether the line is empty so the vehicle can go to the pump
                    if ((lastAvailable != 10) && ((LaneIsFree(lane, "enter", lastAvailable) == true))) 
                    {
                        p = pumps[lastAvailable]; // the object "p" assumes the value of the last available pump
                        v = vehicles[0]; // The object is initialised assuming the values of the first instance stored in the list "vehicles"
                        v.PumpUsed = (lastAvailable + 1); // the number of the pump is stored so it can be used to add it in the transaction
                        v.MyLane = lane;
                        vehicles.Remove(v); // Remove the instance taken from the list "vehicles"
                        p.AssignVehicle(v); // Calls the method "AssignVehicle" which belongs to the object "p" passing as a parameter the object "v"
                        lastAvailable = 10; // The variable returns to its default number as the lane is finished
                        break; // break the loop 
                    }
                    lane++;
                    if (lane == 3)
                    {
                        lane = 0;
                    }
                }
                // Checking in case the queue is empty 
                if (vehicles.Count == 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method checks whether a vehicle is able to enter or to exit from the line.
        /// </summary>
        /// <param name="lane">the current lane where the vehicle wants to get access to</param>
        /// <param name="way">in which way (enter or exit)</param>
        /// <param name="pumpPosition">the position of the interested pump</param>
        /// <returns></returns>
        public static bool LaneIsFree(int lane, string way, int pumpPosition)
        {
            bool laneIsFree = false;

            // Check whether vehicle needs to enter or to exit from the lane
            switch (way)
            {
                case "enter":
                    int firstPumpIndex = 0;

                    // Checking the lane 
                    switch (lane) 
                    {
                        case 0:
                            firstPumpIndex = 0; // First pump in the list which belongs to the lane is 0
                            break;

                        case 1:
                            firstPumpIndex = 3; // First pump in the list which belongs to the lane is 3
                            break;

                        case 2:
                            firstPumpIndex = 6; // First pump in the list which belongs to the lane is 6
                            break;
                        default:
                            break;
                    }
                    // Checks whether the interested pump is the first in the lane, in that case, vehicle can always reach that pump
                    if (pumpPosition == firstPumpIndex) 
                    {
                        laneIsFree = true; // The line is free
                    }
                    else
                    {
                        // For loop starting from the first pump of the lane and finishing at the last pump before the pump interested
                        for (int i = firstPumpIndex; i < pumpPosition; i++)
                        {
                            // Checks whether the pump is available
                            if (pumps[i].IsAvailable())
                            {
                                laneIsFree = true; // for now the line is free
                            }
                            else
                            {
                                laneIsFree = false; // The lane is not free
                                break; // break the loop because a vehicle is enough to block the lane
                            }
                        }
                    }
                    break;
                case "exit":
                    int lastPumpIndex = 0; // held the index of the last pump in the lane
                    switch (lane)
                    {
                        case 0:
                            lastPumpIndex = 2; // the last pump in the lane has index 2
                            break;

                        case 1:
                            lastPumpIndex = 5; // the last pump in the lane has index 5
                            break;

                        case 2:
                            lastPumpIndex = 8; // the last pump in the lane has index 8
                            break;
                        default:
                            break;
                    }
                    // Checks whether the interested pump is the last in the lane, in that case, vehicle can always exit from the forecourt
                    if (pumpPosition == (lastPumpIndex)) 
                    {
                        laneIsFree = true; // lane is free
                    }
                    else
                    {
                        // For loop starting from the first pump after the interested one and finishing at the last pump (i = indexLastPumpIndex)
                        for (int i = pumpPosition + 1; i <= lastPumpIndex; i++)
                        {
                            // checks if pump is available
                            if (pumps[i].IsAvailable())
                            {
                                laneIsFree = true; // the lane is free for now
                            }
                            else
                            {
                                laneIsFree = false; // the lane is not free
                                break; // break the loop because a vehicle is enough to block the lane
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return laneIsFree; // return the boolean value
        }
    }
}
