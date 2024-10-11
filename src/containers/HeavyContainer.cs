using System;

namespace containers
{
    /// <summary>
    /// Heavy Container class which extends Container class.
    /// </summary>
    public class HeavyContainer : Container
    {
        /// <summary>
        /// Constructs a new HeavyContainer object by inheriting the Container class constructor.
        /// </summary>
        /// <param name="cont_ID"> Unique ID of the container.</param>
        /// <param name="weight"> Weight of the container.</param>
        public HeavyContainer(int cont_ID, double weight) : base(cont_ID, weight)
        {
            fuelConsumption = 3.0;
            contType = "HeavyContainer";
        }

    }
}
