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
        /// <summary>
        /// Main application controller.
        /// </summary>
        private App controller;

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
    }
}