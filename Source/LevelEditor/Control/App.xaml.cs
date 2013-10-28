// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Control
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using LevelEditor.Model;
    using LevelEditor.View;

    /// <summary>
    /// Main application controller.
    /// </summary>
    public partial class App
    {
        #region Fields

        /// <summary>
        /// Window showing information about the application.
        /// </summary>
        private AboutWindow aboutWindow;

        /// <summary>
        /// Current map being edited by the user.
        /// </summary>
        private Map map;

        /// <summary>
        /// Window allowing to specify the properties of a new map to create.
        /// </summary>
        private NewMapWindow newMapWindow;

        /// <summary>
        /// Available map tile types.
        /// </summary>
        private Dictionary<string, MapTileType> tileTypes;

        #endregion

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
        /// Whether the window that allows specifying the properties of a new map to create can be shown.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if the New Map window can be shown, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteNew()
        {
            return true;
        }

        /// <summary>
        /// Creates a new map based on the properties of the New Map window.
        /// </summary>
        public void CreateMap()
        {
            // Parse map dimensions.
            int width;
            int height;

            try
            {
                width = int.Parse(this.newMapWindow.TextBoxMapWidth.Text);
            }
            catch (FormatException)
            {
                this.ShowErrorMessage("Incorrect Width", "Please specify a map width.");
                return;
            }

            try
            {
                height = int.Parse(this.newMapWindow.TextBoxMapHeight.Text);
            }
            catch (FormatException)
            {
                this.ShowErrorMessage("Incorrect Height", "Please specify a map height.");
                return;
            }

            // Create new map.
            try
            {
                this.map = new Map(width, height);
            }
            catch (ArgumentOutOfRangeException e)
            {
                this.ShowErrorMessage("Incorrect Map Size", e.Message);
                return;
            }

            // Fill with tiles.
            var defaultMapTile = this.newMapWindow.SelectedMapTileType;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    this.map[x, y] = new MapTile(x, y, defaultMapTile);
                }
            }

            // Close window.
            this.newMapWindow.Close();
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
            if (this.aboutWindow == null || !this.aboutWindow.IsLoaded)
            {
                this.aboutWindow = new AboutWindow();
            }

            this.aboutWindow.Show();
            this.aboutWindow.Focus();
        }

        /// <summary>
        /// Shows the window that allows specifying the properties of a new map to create.
        /// </summary>
        public void ExecuteNew()
        {
            if (this.newMapWindow == null || !this.newMapWindow.IsLoaded)
            {
                this.newMapWindow = new NewMapWindow();
                this.newMapWindow.SetMapTileTypes(this.tileTypes.Keys);
            }

            this.newMapWindow.Show();
            this.newMapWindow.Focus();
        }

        #endregion

        #region Methods

        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Define map tile types.
            this.tileTypes = new Dictionary<string, MapTileType>();

            var desert = new MapTileType(3, "Desert");
            var water = new MapTileType(5, "Water");
            var grass = new MapTileType(1, "Grass");

            this.tileTypes.Add(desert.Name, desert);
            this.tileTypes.Add(water.Name, water);
            this.tileTypes.Add(grass.Name, grass);
        }

        /// <summary>
        /// Shows an error message with the specified title and text.
        /// </summary>
        /// <param name="title">Title of the error message.</param>
        /// <param name="text">Text of the error message.</param>
        private void ShowErrorMessage(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
        }

        #endregion
    }
}