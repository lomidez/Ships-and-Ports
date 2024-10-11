using ships;

namespace interfaces
{
    /// <summary>
    /// Defines contract for operations that can be used on any implementation of Port interface.
    /// </summary>
    public interface IPort
    {
        /// <summary>
        /// Adds the given ship to the list of ships that are curerntly docked in the port. 
        /// </summary>
        /// <param name="s">The ship to add in List of all ships at port.</param>
        void IncomingShip(Ship s);

        /// <summary>
        /// Adds the given ship to the history of ships that visited the port,
        /// and removes the ship from the list of ships that are currently in the port. 
        /// </summary>
        /// <param name="s">The ship to add in the history List of all ships and
        /// to remove from the List of current ships.</param>
        void OutgoingShip(Ship s);

    }
}
