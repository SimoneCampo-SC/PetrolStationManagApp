using System;

namespace PetrolStationManagementApp_V.High_
{
    public abstract class Vehicle
    {
        public Random random;

        public double fuelTime;
        public double WaitingTime { get; }
        public static int nextCarID = 0;
        public int carID; // Number of the car generated
        public int MaximumFuel { get; set; }
        public float ActualFuel { get; set; }
        public int PumpUsed { get; set; } // Stores the Pump number being used for the transaction list
        protected string vehicleType;
        public int MyLane { get; set; }
        protected Fuel fuelType;

        public string VehicleType
        {
            get
            {
                return vehicleType;
            }
            set
            {
                if ((value == "Car") || (value == "Van") || (value == "HGV"))
                {
                    vehicleType = value;
                }
            }
        }

        public Fuel FuelType
        {
            get
            {
                return fuelType;
            }
            set
            {
                fuelType = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vtp"> Vehicle Type </param>
        /// <param name="ftp"> Fuel Type </param>
        /// <param name="mfl"> Maximum Fuel </param>
        /// <param name="afl"> Actual Fuel </param>
        public Vehicle(string vtp, Fuel ftp, int mfl, float afl)
        {
            random = new Random();
            this.vehicleType = vtp;
            this.fuelType = ftp;
            this.MaximumFuel = mfl;
            this.ActualFuel = afl;
            // The pump is capable of dispensing 2 litres per second
            fuelTime = (((MaximumFuel - ActualFuel) * 1000) / 2.0f); // From the proportion 2.0l : 1000 ms = fuel needed : x ms, where x is the fuelling time
            WaitingTime = random.Next(1000, 2001); //Time frame to service a vehicle is between 1000 and 2000 milliseconds
            carID = nextCarID++;
        }
        /// <summary>
        /// This method will randomly choose the fuel type of the Car
        /// </summary>
        /// <returns></returns>
        public static Fuel MyCarFuel()
        {
            Random random = new Random();
            Fuel fuelType;
            switch (random.Next(3)) // Random numbers 0, 1, 2
            {
                case 0:
                    fuelType = new Unleaded();
                    break;
                case 1:
                    fuelType = new Diesel();
                    break;
                case 2:
                    fuelType = new LPG();
                    break;
                default:
                    fuelType = null;
                    break;
            }
            return fuelType;
        }
        /// <summary>
        /// This method will randomly choose the fuel type of the Van
        /// </summary>
        /// <returns></returns>
        public static Fuel MyVanFuel()
        {
            Random random = new Random();
            Fuel fuelType;
            switch (random.Next(0, 2)) // Random numbers 0, 1
            {
                case 0:
                    fuelType = new Diesel();
                    break;
                case 1:
                    fuelType = new LPG();
                    break;
                default:
                    fuelType = null;
                    break;
            }
            return fuelType;
        }

        public static float CurrentFuel(int amountTank)
        {
            Random rand = new Random();
            float fuel;
            fuel = rand.Next((amountTank / 4) + 1);
            return fuel;
        }
    }
}
