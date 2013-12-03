// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawCommand.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Control.Commands
{
    using System.Collections.Generic;

    using LevelEditor.Model;

    /// <summary>
    /// Command that draws on the map canvas.
    /// </summary>
    public class DrawCommand : ICommand
    {
        #region Constructors and Destructors

        /// <summary>
        /// Creates a new command for drawing on the specified map.
        /// </summary>
        /// <param name="map">Map to draw on.</param>
        public DrawCommand(Map map)
        {
            this.Map = map;

            this.NewTileTypes = new Dictionary<Vector2I, string>();
            this.OldTileTypes = new Dictionary<Vector2I, string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Map to change the tiles of.
        /// </summary>
        public Map Map { get; set; }

        /// <summary>
        /// New tiles to draw.
        /// </summary>
        public Dictionary<Vector2I, string> NewTileTypes { get; set; }

        /// <summary>
        /// Tiles before this draw command has been applied.
        /// </summary>
        public Dictionary<Vector2I, string> OldTileTypes { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Draws all stored map tiles.
        /// </summary>
        public void DoCommand()
        {
            foreach (var newTile in this.NewTileTypes)
            {
                var position = newTile.Key;
                var newTileType = newTile.Value;
                this.Map[position].Type = newTileType;
            }
        }

        /// <summary>
        /// Reverts all stored map tiles.
        /// </summary>
        public void UndoCommand()
        {
            foreach (var oldTile in this.OldTileTypes)
            {
                var position = oldTile.Key;
                var oldTileType = oldTile.Value;
                this.Map[position].Type = oldTileType;
            }
        }

        #endregion
    }
}