// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    /// <summary>
    /// Map data.
    /// </summary>
    public class Map
    {
        #region Public Properties

        /// <summary>
        /// Tiles of this map.
        /// </summary>
        public MapTile[,] Tiles { get; private set; }

        #endregion
    }
}