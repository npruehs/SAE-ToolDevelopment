// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.View
{
    using System.Windows;

    using LevelEditor.Control;

    /// <summary>
    /// Main window view controller.
    /// </summary>
    public partial class MainWindow
    {
        #region Fields

        /// <summary>
        /// Main application controller.
        /// </summary>
        private readonly App controller;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes the main window.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.controller = (App)Application.Current;
        }

        #endregion

        #region Methods

        private void ButtonQuit_OnClick(object sender, RoutedEventArgs e)
        {
            this.controller.Quit();
        }

        #endregion
    }
}