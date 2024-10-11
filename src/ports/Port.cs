using System;
using System.Collections.Generic;
using System.Text;
using interfaces;
using ships;
using containers;
using System.Collections;
using System.Net;
using System.Xml.Linq;
using System.Linq;

namespace ports
{
    /// <summary>
    /// Implementation of IPort interface.
    /// </summary>
    public class Port : IPort
    {
        /// <summary>
        /// Holds X coordinate of port location.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Holds Y coordinate of port location.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Holds unique ID of the port.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        ///  List that holds all containers that are currently in the port.
        /// </summary>
        public List<Container> containers = new List<Container>();

        /// <summary>
        ///  List that keeps track of all ships that visited the port.
        /// </summary>
        private readonly List<Ship> history = new List<Ship>();

        /// <summary>
        ///  List that holds all ships that are currently docked in the port.
        /// </summary>
        private readonly List<Ship> current = new List<Ship>();

        /// <summary>
        /// Constructs a new Port object.
        /// </summary>
        /// <param name="id"> The unique ID of the created port.</param>
        /// <param name="X"> X coordinate of port to be created.</param>
        /// <param name="Y"> Y coordinate of port to be created.</param>
        public Port(int id, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
        }


        /// <inheritdoc />
        public void IncomingShip(Ship s)
        {
            current.Add(s);
        }

        /// <inheritdoc />
        public void OutgoingShip(Ship s)
        {
            history.Add(s);
            current.Remove(s);
        }

        /// <summary>
        /// Calculates the distance between the Port object itself and another Port.
        /// </summary>
        /// <param name="other"> Port to which the distance is calculated </param>
        /// <returns> The distance between two ports. </returns>
        public double GetDistance(Port other)
        {
            double distance = Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
            return distance;
        }

        /// <summary>
        /// Rerurns the List of all containers that are currently in the port.
        /// </summary>
        /// <returns>List of Containers that are currently in the port. </returns>
        public List<Container> GetContainers()
        {
            return containers;
        }

        /// <summary>
        /// Returns a string representation of ship id and fuel amount with all containers in the port.
        /// </summary>
        /// <returns>All containers ids and ships information that are currently in the port .</returns>
        public override string ToString()
        {
            string output = $"Port {ID}: ({String.Format("{0:0.00}", X)}, {String.Format("{0:0.00}", Y)})\n";

            List<string> containerTypes = new List<string> { "BasicContainer", "HeavyContainer",
                                                            "RefrigeratedContainer", "LiquidContainer"};

            foreach (string contType in containerTypes)
            {
                if (containers.Select(x => x.contType).Distinct().Contains(contType))
                {
                    output += $"  {contType}: "; 
                }
                string output1 = "";
                foreach (Container cont in containers)
                {
                    if (cont.contType == contType)
                    {
                        output1 += $"{cont.ID} ";
                    }
                }
                output1 = output1.Trim();
                if (containers.Select(x => x.contType).Distinct().Contains(contType))
                {
                    output += output1;
                    output += "\n";
                }
            }
            
            List<Ship> currentSortedList = current.OrderBy(o => o.ID).ToList();
            foreach (Ship ship in currentSortedList)
            {
                output += ship.ToString();               
            }
            return output;
        }
    }
}
