using containers;
using ports;

namespace interfaces
{
    /// <summary>
    /// Defines contract for operations that can be used on any implementation of Ship interface.
    /// </summary>
    public interface IShip
    {
        /// <summary>
        /// Sails one ship from current port to another given port, if all conditions for sailing are met,
        /// and returns true if ship was sailed successfully. 
        /// </summary>
        /// <param name="p"> Port, to which the ship is sailed.</param>
        /// <returns> True if a ship was successfully sailed to the destination port, false otherwise.</returns>
        bool SailTo(Port p);

        /// <summary>
        /// Adds fuel to the ship.
        /// </summary>
        /// <param name="newFuel"> The amount of fuel to be added in the ship.</param>
        void Refuel(double newFuel);

        /// <summary>
        /// Loads container to the ship if all conditions for loading are met, and returns true
        /// if container was loaded, returns false otherwise.
        /// </summary>
        /// <param name="cont"> The container that should be loaded in the ship. </param>
        /// <returns> True if a container was successfully loaded to a ship, returns false otherwise.</returns>
        bool Load(Container cont);

        /// <summary>
        /// Unloads container from the ship if all conditions for unloading are met, and returns true
        /// if the container was successfully unloaded.
        /// </summary>
        /// <param name="cont"> The container that should be unloaded from the ship. </param>
        /// <returns> True if a container was successfully unloaded from a ship, returns false otherwise.</returns>
        bool Unload(Container cont);
    }
}