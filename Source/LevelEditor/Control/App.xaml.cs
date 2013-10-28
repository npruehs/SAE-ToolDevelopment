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
        /// Whether the application can be shut down.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if the application can be shut down, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteClose()
        {
            return true;
        }

        /// <summary>
        /// Whether the About window of the application can be shown.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if the About window of the application can be shown, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteHelp()
        {
            return true;
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public void ExecuteClose()
        {
            this.Shutdown();
        }

        /// <summary>
        /// Shows the About window of the application.
        /// </summary>
        public void ExecuteHelp()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        #endregion
    }
}