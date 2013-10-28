// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.View
{
    using System.Windows;
    using System.Windows.Input;

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

        private void CommandCanExecuteClose(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteClose();
        }

        private void CommandCanExecuteHelp(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteHelp();
        }

        private void CommandCanExecuteNew(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteNew();
        }

        private void CommandExecutedClose(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecuteClose();
        }

        private void CommandExecutedHelp(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecuteHelp();
        }

        private void CommandExecutedNew(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecuteNew();
        }

        #endregion
    }
}