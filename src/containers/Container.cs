using System;
using System.Collections.Generic;
using System.Linq;

namespace containers
{
    /// <summary>
    /// Abstract class to represent a Container.
    /// </summary>
    public abstract class Container
    {
        /// <summary>
        /// Holds the container type.
        /// </summary>
        public string contType { get; protected set; }

        /// <summary>
        /// Holds the fuel consumption required by the container. 
        /// </summary>
        public double fuelConsumption { get; protected set; }

        /// <summary>
        /// Holds a unique ID of the container.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Holds the weight of the container.
        /// </summary>
        public double weight { get; private set; }

        /// <summary>
        /// Constructs a new Container object.
        /// </summary>
        /// <param name="ID"> Unique ID of the container.</param>
        /// <param name="weight"> Weight of the container.</param>
        public Container (int ID, double weight)
        {
            this.ID = ID;
            this.weight = weight;
        }

        /// <summary>
        /// Returns fuel consumption required by the container.
        /// </summary>
        /// <returns>Fuel consumption of the container.</returns>
        public double Consumption()
        {
            return fuelConsumption;
        }

        /// <summary>
        /// Checks type, ID and weight of current and other container,
        /// if they are the same, return true, otherwise return false.
        /// </summary>
        /// <param name="other"> The container that is compared to the current container.</param>
        /// <returns>True if containers are the same, false otherwise.</returns>
        public bool Equals(Container other)
        {
            return (contType == other.contType && ID == other.ID && weight == other.weight);
        }

    }
}
