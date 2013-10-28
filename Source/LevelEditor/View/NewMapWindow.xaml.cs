// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewMapWindow.xaml.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.View
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    using LevelEditor.Control;

    /// <summary>
    ///  New Map window view controller.
    /// </summary>
    public partial class NewMapWindow
    {
        #region Fields

        /// <summary>
        /// Main application controller.
        /// </summary>
        private readonly App controller;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes the New Map window.
        /// </summary>
        public NewMapWindow()
        {
            this.InitializeComponent();

            this.controller = (App)Application.Current;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Name of the default map tile type for the new map.
        /// </summary>
        public string SelectedMapTileType { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Sets up radio buttons for each of the specified map tile types.
        /// </summary>
        /// <param name="mapTileTypes">Map tile types to add radio buttons for.</param>
        public void SetMapTileTypes(IEnumerable<string> mapTileTypes)
        {
            // Add radio buttons.
            foreach (var mapTileType in mapTileTypes)
            {
                var radioButton = new RadioButton();
                radioButton.Content = mapTileType;
                radioButton.GroupName = "MapTileTypes";
                radioButton.Checked += this.OnMapTileSelected;
                
                this.DefaultTilePanel.Children.Add(radioButton);
            }

            // Check first radio button.
            var firstRadioButton = (RadioButton)this.DefaultTilePanel.Children[0];
            firstRadioButton.IsChecked = true;
        }

        #endregion

        #region Methods

        private void OnCreateMapClicked(object sender, RoutedEventArgs e)
        {
            this.controller.CreateMap();
        }

        private void OnMapTileSelected(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            this.SelectedMapTileType = radioButton.Content.ToString();
        }

        #endregion
    }
}