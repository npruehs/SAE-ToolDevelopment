// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Control.Commands
{
    /// <summary>
    /// Editor command that can be undone.
    /// </summary>
    public interface ICommand
    {
        #region Public Methods and Operators

        /// <summary>
        /// Executes this editor command.
        /// </summary>
        void DoCommand();

        /// <summary>
        /// Reverts changes made by this editor command.
        /// </summary>
        void UndoCommand();

        #endregion
    }
}