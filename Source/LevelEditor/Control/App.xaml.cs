// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Control
{
    using LevelEditor.View;

    /// <summary>
    /// Main application controller.
    /// </summary>
    public partial class App
    {
        #region Public Methods and Operators

        /// <summary>
        /// Shows the About window of the application.
        /// </summary>
        public void About()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

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