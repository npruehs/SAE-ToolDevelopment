// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Control
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    using LevelEditor.Control.Commands;
    using LevelEditor.Model;
    using LevelEditor.View;

    using Microsoft.Win32;

    /// <summary>
    /// Main application controller.
    /// </summary>
    public partial class App
    {
        #region Constants

        private const string MapFileExtension = ".map";

        private const string MapFileFilter = "Map files (.map)|*.map";

        private const string XmlElementHeight = "Height";

        private const string XmlElementPositionX = "X";

        private const string XmlElementPositionY = "Y";

        private const string XmlElementTiles = "Tiles";

        private const string XmlElementType = "Type";

        private const string XmlElementWidth = "Width";

        #endregion

        #region Fields

        /// <summary>
        /// Stack of commands that can be redone.
        /// </summary>
        private readonly Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Stack of commands that can be undone.
        /// </summary>
        private readonly Stack<ICommand> undoStack = new Stack<ICommand>();

        /// <summary>
        /// Window showing information about the application.
        /// </summary>
        private AboutWindow aboutWindow;

        /// <summary>
        /// Current active brush.
        /// </summary>
        private MapTileType currentBrush;

        /// <summary>
        /// Current draw command that will be pushed on Undo stack.
        /// </summary>
        private DrawCommand currentDrawCommand;

        /// <summary>
        /// Main application window.
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// Current map being edited by the user.
        /// </summary>
        private Map map;

        /// <summary>
        /// Window allowing to specify the properties of a new map to create.
        /// </summary>
        private NewMapWindow newMapWindow;

        /// <summary>
        /// Available map tile images.
        /// </summary>
        private Dictionary<string, BitmapImage> tileImages;

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
        /// Whether an existing map can be opened.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if an existing map can be opened, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteOpen()
        {
            return true;
        }

        /// <summary>
        /// Whether there is any command to redo.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if there's any command to redo, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteRedo()
        {
            return this.redoStack.Count > 0;
        }

        /// <summary>
        /// Whether the current map can be saved.
        /// </summary>
        /// <returns>
        /// <c>true</c>, the current map can be saved, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteSaveAs()
        {
            return this.map != null;
        }

        /// <summary>
        /// Whether there is any command to undo.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if there's any command to undo, and <c>false</c> otherwise.
        /// </returns>
        public bool CanExecuteUndo()
        {
            return this.undoStack.Count > 0;
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

            // Update status text.
            this.mainWindow.SetStatusText("Creating new map...");

            // Run background worker thread.
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.BackgroundCreateMap;
            worker.RunWorkerCompleted += this.BackgroundCreateMapCompleted;
            worker.ProgressChanged += this.BackgroundCreateMapProgressChanged;
            worker.WorkerReportsProgress = true;

            Vector2I mapSize = new Vector2I(width, height);
            worker.RunWorkerAsync(mapSize);
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

        /// <summary>
        /// Shows an open file dialog box and opens the specified map file.
        /// </summary>
        public void ExecuteOpen()
        {
            // Show open file dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    AddExtension = true, 
                    CheckFileExists = true, 
                    CheckPathExists = true, 
                    DefaultExt = MapFileExtension, 
                    FileName = "Another Map", 
                    Filter = MapFileFilter, 
                    ValidateNames = true
                };

            var result = openFileDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            // Open map file.
            using (var stream = openFileDialog.OpenFile())
            {
                try
                {
                    // Validate schema.
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.Schemas.Add("http://www.npruehs.de/teaching", "XML/MapSchema.xsd");
                    readerSettings.ValidationType = ValidationType.Schema;

                    using (var reader = XmlReader.Create(openFileDialog.FileName, readerSettings))
                    {
                        while (reader.Read())
                        {
                        }
                    }

                    // Deserialize map.
                    XmlSerializer serializer = new XmlSerializer(typeof(Map));
                    this.map = (Map)serializer.Deserialize(stream);

                    // Show new map tiles.
                    this.UpdateMapCanvas();
                }
                catch (InvalidOperationException)
                {
                    this.ShowErrorMessage("Incorrect map file", "Please specify a valid map file!");
                }
                catch (XmlException)
                {
                    this.ShowErrorMessage("Incorrect map file", "Please specify a valid map file!");
                }
                catch (XmlSchemaValidationException)
                {
                    this.ShowErrorMessage("Incorrect map file", "Please specify a valid map file!");
                }
            }
        }

        /// <summary>
        /// Executes the most recent undone command.
        /// </summary>
        public void ExecuteRedo()
        {
            // Move command from Redo to Undo stack.
            var command = this.redoStack.Pop();
            this.undoStack.Push(command);

            // Redo command.
            command.DoCommand();
            this.UpdateMapCanvas();
        }

        /// <summary>
        /// Shows a save file dialog box and saves the current map to the specified file.
        /// </summary>
        public void ExecuteSaveAs()
        {
            // Show save file dialog box.
            SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    AddExtension = true, 
                    CheckPathExists = true, 
                    DefaultExt = MapFileExtension, 
                    FileName = "Another Map", 
                    Filter = MapFileFilter, 
                    ValidateNames = true
                };

            var result = saveFileDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            // Open file stream.
            using (var stream = saveFileDialog.OpenFile())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Map));

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("np", "http://www.npruehs.de/teaching");

                serializer.Serialize(stream, this.map, namespaces);
            }
        }

        /// <summary>
        /// Undoes the last command.
        /// </summary>
        public void ExecuteUndo()
        {
            // Move command from Undo to Redo stack.
            var command = this.undoStack.Pop();
            this.redoStack.Push(command);

            // Undo command.
            command.UndoCommand();
            this.UpdateMapCanvas();
        }

        /// <summary>
        /// Gets the image for the map tile of the specified type.
        /// </summary>
        /// <param name="tileType">Type of the tile to get the image for.</param>
        /// <returns>Image for the map tile of the specified type.</returns>
        public BitmapImage GetTileImage(string tileType)
        {
            return this.tileImages[tileType];
        }

        /// <summary>
        /// Prepares a new command that can be undone later.
        /// </summary>
        public void OnBrushDown()
        {
            this.currentDrawCommand = new DrawCommand(this.map);
        }

        public void OnBrushSelected(string brush)
        {
            this.currentBrush = this.tileTypes[brush];
        }

        /// <summary>
        /// Pushes the current command, enabling Undo.
        /// </summary>
        public void OnBrushUp()
        {
            this.undoStack.Push(this.currentDrawCommand);
            this.redoStack.Clear();
        }

        public void OnTileClicked(Vector2I position)
        {
            // Early out if no brush selected.
            if (this.currentBrush == null)
            {
                return;
            }

            // Early out if nothing changed.
            if (this.map[position].Type.Equals(this.currentBrush.Name))
            {
                return;
            }

            // Modify map model.
            this.currentDrawCommand.OldTileTypes[position] = this.map[position].Type;
            this.currentDrawCommand.NewTileTypes[position] = this.currentBrush.Name;

            this.map[position].Type = this.currentBrush.Name;

            // Update canvas.
            this.mainWindow.UpdateMapCanvas(position, this.tileImages[this.currentBrush.Name]);
        }

        #endregion

        #region Methods

        private void BackgroundCreateMap(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            Vector2I mapSize = (Vector2I)e.Argument;

            // Create new map.
            try
            {
                this.map = new Map(mapSize.X, mapSize.Y);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                this.ShowErrorMessage("Incorrect Map Size", ex.Message);
                return;
            }

            // Fill with tiles.
            var defaultMapTile = this.newMapWindow.SelectedMapTileType;

            var tilesCreated = 0;
            var totalTiles = mapSize.X * mapSize.Y;

            for (var x = 0; x < mapSize.X; x++)
            {
                for (var y = 0; y < mapSize.Y; y++)
                {
                    this.map[x, y] = new MapTile(x, y, defaultMapTile);
                    tilesCreated++;
                }

                // Report progress.
                worker.ReportProgress(tilesCreated * 100 / totalTiles);
                Thread.Sleep(1);
            }
        }

        private void BackgroundCreateMapCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Show new map tiles.
            this.UpdateMapCanvas();

            // Close window.
            this.newMapWindow.Close();

            // Reset status text.
            this.mainWindow.ResetStatusText();

            // Reset undo stack.
            this.undoStack.Clear();
            this.redoStack.Clear();
        }

        private void BackgroundCreateMapProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update progress bar.
            this.mainWindow.SetProgress(e.ProgressPercentage);

            if (e.ProgressPercentage >= 100)
            {
                // Map tiles have been created. Update status text and progress bar.
                this.mainWindow.SetProgress(0);
                this.mainWindow.SetStatusText("Updating canvas...");
            }
        }

        private void OnActivated(object sender, EventArgs e)
        {
            this.mainWindow = (MainWindow)this.MainWindow;
            this.mainWindow.SetMapTileTypes(this.tileTypes.Keys);
        }

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

            // Load sprites.
            this.tileImages = new Dictionary<string, BitmapImage>();

            foreach (var tileType in this.tileTypes.Values)
            {
                var imageUri = "pack://application:,,,/Resources/MapTiles/" + tileType.Name + ".png";

                BitmapImage tileImage = new BitmapImage();
                tileImage.BeginInit();
                tileImage.UriSource = new Uri(imageUri);
                tileImage.EndInit();

                this.tileImages.Add(tileType.Name, tileImage);
            }
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

        /// <summary>
        /// Passes the current map to the canvas for rendering.
        /// </summary>
        private void UpdateMapCanvas()
        {
            this.mainWindow.UpdateMapCanvas(this.map);
        }

        #endregion
    }
}