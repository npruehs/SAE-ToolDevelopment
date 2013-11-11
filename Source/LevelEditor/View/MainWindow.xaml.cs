// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.View
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

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

        /// <summary>
        /// Whether the user is currently drawing.
        /// </summary>
        private bool brushDown;

        /// <summary>
        /// Images of all map tiles.
        /// </summary>
        private Image[,] canvasImages;

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
        /// Sets up radio buttons for each of the specified map tile types.
        /// </summary>
        /// <param name="mapTileTypes">Map tile types to add radio buttons for.</param>
        public void SetMapTileTypes(IEnumerable<string> mapTileTypes)
        {
            this.BrushSelectionPanel.Children.Clear();

            foreach (var mapTileType in mapTileTypes)
            {
                var radioButton = new RadioButton();
                radioButton.Content = mapTileType;
                radioButton.GroupName = "MapTileTypes";
                radioButton.Checked += this.OnBrushSelected;

                this.BrushSelectionPanel.Children.Add(radioButton);
            }
        }

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

            this.canvasImages = new Image[map.Width, map.Height];

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
                            Source = this.controller.GetTileImage(mapTile.Type), 
                            Tag = new Vector2I(x, y)
                        };

                    this.MapCanvas.Children.Add(image);

                    this.canvasImages[x, y] = image;

                    // Set image position.
                    Canvas.SetLeft(image, x * TileSizeInPixels);
                    Canvas.SetTop(image, y * TileSizeInPixels);

                    // Register image event handlers.
                    image.MouseLeftButtonDown += this.OnBrushDown;
                    image.MouseLeftButtonUp += this.OnBrushUp;
                    image.MouseMove += this.OnTileClicked;
                }
            }
        }

        /// <summary>
        /// Updates the canvas image at the specified position.
        /// </summary>
        /// <param name="position">Position to update the image of, in tile space.</param>
        /// <param name="tileImage">New image to be rendered at the specified position.</param>
        public void UpdateMapCanvas(Vector2I position, BitmapImage tileImage)
        {
            this.canvasImages[position.X, position.Y].Source = tileImage;
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

        private void CommandCanExecuteOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteOpen();
        }

        private void CommandCanExecuteSaveAs(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteSaveAs();
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

        private void CommandExecutedOpen(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecuteOpen();
        }

        private void CommandExecutedSaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecuteSaveAs();
        }

        private void OnBrushDown(object sender, MouseButtonEventArgs e)
        {
            this.brushDown = true;
        }

        private void OnBrushSelected(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            this.controller.OnBrushSelected((string)radioButton.Content);
        }

        private void OnBrushUp(object sender, MouseButtonEventArgs e)
        {
            this.brushDown = false;
        }

        private void OnTileClicked(object sender, MouseEventArgs mouseEventArgs)
        {
            if (!this.brushDown)
            {
                return;
            }

            var tile = (Image)sender;
            var position = (Vector2I)tile.Tag;

            this.controller.OnTileClicked(position);
        }

        #endregion
    }
}