using System;

namespace containers
{
    /// <summary>
    /// Liquid Container class which extends the Heavy Container class.
    /// </summary>
    public class LiquidContainer : HeavyContainer
    {
        /// <summary>
        /// Constructs a new LiquidContainer object by inheriting the HeavyContainer class constructor.
        /// </summary>
        /// <param name="cont_ID"> Unique ID of the container.</param>
        /// <param name="weight"> Weight of the container.</param>
        public LiquidContainer(int cont_ID, double weight) : base(cont_ID, weight)
        {
            fuelConsumption = 4.0;
            contType = "LiquidContainer";
        }

    }
}
