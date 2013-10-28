// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapTileType.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    using System;

    /// <summary>
    /// Type of a map tile.
    /// </summary>
    public class MapTileType
    {
        #region Constructors and Destructors

        /// <summary>
        /// Creates a new map tile type.
        /// </summary>
        /// <param name="movementCost">Cost for crossing the map tile.</param>
        /// <param name="name">Name of the map tile type.</param>
        /// <exception cref="ArgumentOutOfRangeException">Movement cost is less than or equal to zero.</exception>
        /// <exception cref="ArgumentNullException">Type name is null or empty.</exception>
        public MapTileType(int movementCost, string name)
        {
            if (movementCost <= 0)
            {
                throw new ArgumentOutOfRangeException("movementCost", "Movement cost must be greater than zero.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            this.MovementCost = movementCost;
            this.Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Cost for crossing this map tile.
        /// </summary>
        public int MovementCost { get; private set; }

        /// <summary>
        /// Name of this map tile type.
        /// </summary>
        public string Name { get; private set; }

        #endregion
    }
}