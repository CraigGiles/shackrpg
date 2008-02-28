using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using TestProject.Systems;
using TestProject.Utilities;

namespace TestProject.Utilities
{
    /// <summary>
    /// GameState Manager is a utility class designed to keep track of which
    /// state the game is currently in. (title screen, Main Menu, Paused, 
    /// Main Game, etc) You can change teh current game state within this class
    /// or just check which game state you're currently in.
    /// 
    /// Craig Giles
    /// Feb.2008
    /// </summary>
    class GameStateManager
    {
        static public GameState curGameState;       // The Games current GameState

        /// <summary>
        /// GameStates used by the RPG. Will include states like Title,
        /// Game, NewGame, GameOver, and CutScene.
        /// </summary>
        public enum GameState
        {
            Title,
            Scene,
        }

        /// <summary>
        /// Changes the Current Game state to the indicated gamestate.
        /// </summary>
        /// <param name="setGameState">The Gamestate you wish to set as the Current Gamestate</param>
        static public void ChangeGameState(GameState setGameState)
        {
            switch (setGameState)
            {
                case GameState.Title:
                    curGameState = GameState.Title;
                    break;

                case GameState.Scene:
                    curGameState = GameState.Scene;
                    break;
            }
        }//end ChangeGameState

    }//end Class
}//end Namespace
