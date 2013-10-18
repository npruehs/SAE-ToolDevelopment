// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Control
{
    /// <summary>
    /// Main application controller.
    /// </summary>
    public partial class App
    {
        #region Public Methods and Operators

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public void Quit()
        {
            this.Shutdown();
        }

        #endregion
    }
}