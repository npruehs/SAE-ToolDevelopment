// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector2I.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    /// <summary>
    /// Two-dimensional vector with integer coordinates.
    /// </summary>
    public class Vector2I
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new null vector.
        /// </summary>
        public Vector2I()
        {
        }

        /// <summary>
        /// Constructs a new vector with the specified components.
        /// </summary>
        /// <param name="x">X-component of the vector.</param>
        /// <param name="y">Y-component of the vector.</param>
        public Vector2I(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// X-component of this vector.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-component of this vector.
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", this.X, this.Y);
        }

        #endregion
    }
}