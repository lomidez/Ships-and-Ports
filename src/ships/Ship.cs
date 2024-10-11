using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using interfaces;
using containers;
using ports;

namespace ships
{
    /// <summary>
    /// Implementation of IShip interface.
    /// </summary>
    public class Ship : IShip
    {
        /// <summary>
        /// Holds unique ID of the ship.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Maximum weight of all containers allowed in that ship.
        /// </summary>
        public int totalWeightCapacity { get; private set; }

        /// <summary>
        /// Maximum number of all containers allowed in that ship.
        /// </summary>
        public int maxNumberOfAllContainers { get; private set; }

        /// <summary>
        /// Maximum number of Heavy containers allowed in that ship.
        /// </summary>
        public int maxNumberOfHeavyContainers { get; private set; }

        /// <summary>
        /// Maximum number of Refrigerated containers allowed in that ship.
        /// </summary>
        public int maxNumberOfRefrigeratedContainers { get; private set; }

        /// <summary>
        /// Maximum number of Liquid containers allowed in that ship.
        /// </summary>
        public int maxNumberOfLiquidContainers { get; private set; }

        /// <summary>
        /// The fuel consumption per km of that ship.
        /// </summary>
        public double fuelConsumptionPerKM { get; private set; }

        /// <summary>
        ///  List that holds all containers that are currently in the ship.
        /// </summary>
        private readonly List<Container> allContainers = new List<Container>();

        /// <summary>
        ///  List that holds all heavy containers that are currently in the ship.
        /// </summary>
        private readonly List<Container> heavyContainers = new List<Container>();

        /// <summary>
        ///  List that holds all refrigerated containers that are currently in the ship.
        /// </summary>
        private readonly List<Container> refrigeratedContainers = new List<Container>();

        /// <summary>
        ///  List that holds all liquid containers that are currently in the ship.
        /// </summary>
        private readonly List<Container> liquidContainers = new List<Container>();

        /// <summary>
        ///  Maximum allowed weight of all containers in the ship.
        /// </summary>
        private double containersTotalWeight;

        /// <summary>
        ///  The port, where the ship is currently docked.
        /// </summary>
        private Port currentPort;

        /// <summary>
        ///  The amount of fuel of the ship.
        /// </summary>
        private double fuel;

        /// <summary>
        /// Constructs a new Ship object and adds the created ship object to the current port. 
        /// </summary>
        /// <param name="ID"> The unique ID of the created ship.</param>
        /// <param name="p"> The port, where ship is created.</param>
        /// <param name="totalWeightCapacity"> Maximum weight of all containers allowed in that ship.</param>
        /// <param name="maxNumberOfAllContainers"> Maximum number of all containers allowed in that ship.</param>
        /// <param name="maxNumberOfHeavyContainers"> Maximum number of Heavy containers allowed in that ship.</param>
        /// <param name="maxNumberOfRefrigeratedContainers"> Maximum number of Refrigerated containers allowed in that ship..</param>
        /// <param name="maxNumberOfLiquidContainers"> Maximum number of Liquid containers allowed in that ship.</param>
        /// <param name="fuelConsumptionPerKM">  The fuel consumption per km of that ship.</param>
        public Ship(int ID, Port p, int totalWeightCapacity, int maxNumberOfAllContainers, 
                    int maxNumberOfHeavyContainers, int maxNumberOfRefrigeratedContainers,
                    int maxNumberOfLiquidContainers, double fuelConsumptionPerKM)
        {
            this.ID = ID;
            this.totalWeightCapacity = totalWeightCapacity;
            this.maxNumberOfAllContainers = maxNumberOfAllContainers;
            this.maxNumberOfHeavyContainers = maxNumberOfHeavyContainers;
            this.maxNumberOfRefrigeratedContainers = maxNumberOfRefrigeratedContainers;
            this.maxNumberOfLiquidContainers = maxNumberOfLiquidContainers;
            this.fuelConsumptionPerKM = fuelConsumptionPerKM;
            currentPort = p;
            p.IncomingShip(this);
            fuel = 0;
            containersTotalWeight = 0;

        }

        /// <inheritdoc />
        public bool SailTo(Port p)
        {
            double totalFuelConsOfContainers = 0;
            double weightFactor;

            foreach (Container cont in allContainers)
            {

                weightFactor = cont.Consumption() * cont.weight;
                totalFuelConsOfContainers += weightFactor;
            }

            double fuelNeeded = (fuelConsumptionPerKM + totalFuelConsOfContainers) * currentPort.GetDistance(p);

            if (fuel >= fuelNeeded)
            {
                currentPort.OutgoingShip(this);
                fuel -= fuelNeeded;
                currentPort = p;
                currentPort.IncomingShip(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc />
        public void Refuel(double newFuel)
        {
            fuel += newFuel;
        }

        /// <inheritdoc />
        public bool Load(Container cont)
        {
            if (currentPort.containers.Contains(cont) && CanContainerBeLoaded(cont))
            {
                AddContainer(cont);
                currentPort.containers.Remove(cont);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool Unload(Container cont)
        {
            if (allContainers.Contains(cont))
            {
                currentPort.containers.Add(cont);
                RemoveContainer(cont);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a string representation of ship information and
        /// all containers information that are currently in this ship.
        /// </summary>
        /// <returns> All containers information and ship id with fuel amount.</returns>
        public override string ToString()
        {
            string output = $"  Ship {ID}: {String.Format("{0:0.00}", fuel)}\n";

            List<string> containerTypes = new List<string> { "BasicContainer", "HeavyContainer",
                                                            "RefrigeratedContainer", "LiquidContainer"};

            foreach (string contType in containerTypes)
            {
                if (allContainers.Select(x => x.contType).Distinct().Contains(contType))
                {
                    output += $"    {contType}: ";
                }
                string output1 = "";
                foreach (Container cont in allContainers)
                {
                    if (cont.contType == contType)
                    {
                        output1 += $"{cont.ID} ";
                    }
                }
                output1 = output1.TrimEnd();
                if (allContainers.Select(x => x.contType).Distinct().Contains(contType))
                {
                    output += output1;
                    output += "\n";
                }
            }
            return output;
        }

        /// <summary>
        /// Adds container to the ship.
        /// </summary>
        /// <param name="cont"> The container that should be added to the ship. </param>
        private void AddContainer(Container cont)
        {
            containersTotalWeight += cont.weight;
            allContainers.Add(cont);
            if (cont.contType == "HeavyContainer")
            {
                heavyContainers.Add(cont);
            }
            else if (cont.contType == "LiquidContainer")
            {
                liquidContainers.Add(cont);
            }
            else if (cont.contType == "RefrigeratedContainer")
            {
                refrigeratedContainers.Add(cont);
            }
        }

        /// <summary>
        /// Removes the container from the ship.
        /// </summary>
        /// <param name="cont"> Container that should be removed from the ship.</param>
        private void RemoveContainer(Container cont)
        {
            containersTotalWeight -= cont.weight;
            allContainers.Remove(cont);
            if (cont.contType == "HeavyContainer")
            {
                heavyContainers.Remove(cont);
            }
            else if (cont.contType == "LiquidContainer")
            {
                liquidContainers.Remove(cont);
            }
            else if (cont.contType == "RefrigeratedContainer")
            {
                refrigeratedContainers.Remove(cont);
            }
        }

        /// <summary>
        /// Checks if the container meets conditions for being loaded on the ship.
        /// </summary>
        /// <param name="cont">The container that should be checked for loading conditions.</param>
        /// <returns> True if container mets conditions for loading, return false otherwise. </returns>
        private bool CanContainerBeLoaded(Container cont)
        {
            int heavyAll = heavyContainers.Count;
            int liquidAll = liquidContainers.Count;
            int refrAll = liquidContainers.Count;

            if (cont.contType == "HeavyContainer")
            {
                heavyAll += 1;
            }
            else if (cont.contType == "LiquidContainer")
            {
                liquidAll += 1;
            }
            else if (cont.contType == "RefrigeratedContainer")
            {
                refrAll +=  1;
            }

            return (containersTotalWeight + cont.weight <= totalWeightCapacity &&
                allContainers.Count + 1 <= maxNumberOfAllContainers &&
                heavyAll <= maxNumberOfHeavyContainers &&
                refrAll <= maxNumberOfRefrigeratedContainers &&
                liquidAll <= maxNumberOfLiquidContainers);

        }
    }
}