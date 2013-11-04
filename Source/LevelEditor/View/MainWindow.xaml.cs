// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.View
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using LevelEditor.Control;
    using LevelEditor.Model;

    /// <summary>
    /// Main window view controller.
    /// </summary>
    public partial class MainWindow
    {
        #region Constants

        /// <summary>
        /// Size of a tile on the canvas, in pixels.
        /// </summary>
        private const int TileSizeInPixels = 32;

        #endregion

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

        #region Public Methods and Operators

        /// <summary>
        /// Clears the map canvas and fills it with sprites for the specified map.
        /// </summary>
        /// <param name="map">Map to show.</param>
        public void UpdateMapCanvas(Map map)
        {
            // Remove old images.
            this.MapCanvas.Children.Clear();

            // Set canvas size.
            this.MapCanvas.Width = map.Width * TileSizeInPixels;
            this.MapCanvas.Height = map.Height * TileSizeInPixels;

            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    var mapTile = map[x, y];

                    // Add new image to canvas.
                    var image = new Image
                        {
                            Width = TileSizeInPixels, 
                            Height = TileSizeInPixels, 
                            Source = this.controller.GetTileImage(mapTile.Type)
                        };

                    this.MapCanvas.Children.Add(image);

                    // Set image position.
                    Canvas.SetLeft(image, x * TileSizeInPixels);
                    Canvas.SetTop(image, y * TileSizeInPixels);
                }
            }
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