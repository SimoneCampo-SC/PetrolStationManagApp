using System;
using System.Timers;

namespace PetrolStationManagementApp_V.High_
{
    class Pump
    {
        public Vehicle currentVehicle = null; // Create and Initialise an object of the class Vehicle giving no values (Null)
        public bool Blocked { get; set; } // Boolean roperty which is true when the Pump is blocked by a vehicle that is not able to get out
        // Constructor 
        public Pump()
        {
        }
        /// <summary>
        /// This method returns the status of the pump
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            // returns TRUE if currentVehicle is NULL, meaning available
            // returns FALSE if currentVehicle is NOT NULL, meaning busy
            return currentVehicle == null; // it checks whether currentVehicle is equal to null.
        }

        /// <summary>
        /// This method assigns a vehicle to the chosen pump
        /// </summary>
        /// <param name="v">The Object of the vehicle class that needs to be assigned to the pump</param>
        public void AssignVehicle(Vehicle v)
        {
            currentVehicle = v; // the variable assumes the value of the parameter "v"
            Counter.CountServedVehicles(); // Keep track of the number of vehicle serviced
            Timer timer = new Timer(); // Creation and Initialization of the new instance timer 
            timer.Interval = v.fuelTime; // the interval depends on the fuelling time of the instance "v"
            timer.AutoReset = false; // don't repeat again as it's a countdown, not a loop
            timer.Elapsed += ReleaseVehicle; // when the fuelling time is finished, it calls the method "RelaseVehicles"
            timer.Enabled = true;
            timer.Start();

        }

        /// <summary>
        /// This method relases the vehicle object from the pump storing the litres dispensed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReleaseVehicle(object sender, ElapsedEventArgs e)
        {
            double litres = Math.Floor(currentVehicle.MaximumFuel - currentVehicle.ActualFuel);//(currentVehicle.MaximumFuel - currentVehicle.ActualFuel);// Because the fuelling time depends on the fuel needed
            Counter.TotalLitres += litres; // Adds the litres to the totalLitres counter

            switch (currentVehicle.FuelType.MyFuel) // Checks the type of the fuel being used in the vehicle
            {
                case "Unleaded":
                    Counter.TotalUnleaded += litres; // if it is Unleaded,
                    break;

                case "Diesel":
                    Counter.TotalDiesel += litres; // if it is Diesel
                    break;

                case "LPG":
                    Counter.TotalLPG += litres; // if it is LPG
                    break;

                default:
                    break;

            }
            // Creation and Initialization of a new object that assumes the values of the transaction just occured 
            FuellingTransaction Transaction = new FuellingTransaction(currentVehicle.VehicleType, currentVehicle.FuelType.MyFuel, litres, currentVehicle.PumpUsed);
            Counter.TransactionList.Add(Transaction); // The transaction created is added to the list

            // Before to left, it is important to check whether it is not blocked from the other vehicles in the lane
            // This timer keep checking every second whether the lane is free and it stops repeating as soon as the vehicle leaves the forecourt
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += (sender, e) =>
            {
                // If the line is free, vehicle is relased and the timer is stopped
                if (Data.LaneIsFree(currentVehicle.MyLane, "exit", (currentVehicle.PumpUsed - 1)) == true)
                {
                    // Through the list pumps, using the property of the vehicle "PumpUsed", it gets the pump blocked and set its property to false
                    Data.pumps[currentVehicle.PumpUsed - 1].Blocked = false;
                    currentVehicle = null; // vehicle is relased
                    timer.AutoReset = false;
                }
                else
                {
                    // Through the list pumps, using the property of the vehicle "PumpUsed", it gets the pump blocked and set its property to true
                    Data.pumps[currentVehicle.PumpUsed - 1].Blocked = true;
                    timer.AutoReset = true; // Keep checking until the line is free
                }
            };
            timer.Enabled = true;
            timer.Start();
        }
    }
}

