// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapTileType.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    /// <summary>
    /// Type of a map tile.
    /// </summary>
    public class MapTileType
    {
        #region Public Properties

        /// <summary>
        /// Cost for crossing this map tile.
        /// </summary>
        public int MovementCost { get; set; }

        /// <summary>
        /// Name of this map tile type.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}