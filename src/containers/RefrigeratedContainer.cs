using System;

namespace containers
{
    /// <summary>
    /// Refrigerated Container class which extends the Heavy Container class.
    /// </summary>
    public class RefrigeratedContainer : HeavyContainer
    {
        /// <summary>
        /// Constructs a new RefrigeratedContainer object by inheriting the HeavyContainer class constructor.
        /// </summary>
        /// <param name="cont_ID"> Unique ID of the container.</param>
        /// <param name="weight"> Weight of the container.</param>
        public RefrigeratedContainer(int cont_ID, double weight) : base(cont_ID, weight)
        {
            fuelConsumption = 5.0;
            contType = "RefrigeratedContainer";
        }
    }
}
