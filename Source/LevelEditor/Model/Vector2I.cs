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
    public struct Vector2I
    {
        #region Fields

        /// <summary>
        /// X-component of this vector.
        /// </summary>
        private readonly int x;

        /// <summary>
        /// Y-component of this vector.
        /// </summary>
        private readonly int y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new vector with the specified components.
        /// </summary>
        /// <param name="x">X-component of the vector.</param>
        /// <param name="y">Y-component of the vector.</param>
        public Vector2I(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// X-component of this vector.
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Y-component of this vector.
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }
        }

        #endregion
    }
}