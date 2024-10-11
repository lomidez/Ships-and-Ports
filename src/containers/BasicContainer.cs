using System;

namespace containers
{
    /// <summary>
    /// Basic Container class which extends Container class.
    /// </summary>
    public class BasicContainer : Container
    {
        /// <summary>
        /// Constructs a new BasicContainer object by inheriting the Container class constructor.
        /// </summary>
        /// <param name="cont_ID"> Unique ID of the container.</param>
        /// <param name="weight"> Weight of the container.</param>
        public BasicContainer(int cont_ID, double weight) : base(cont_ID, weight)
        {
            fuelConsumption = 2.5;
            contType = "BasicContainer";
        }
    }
}
