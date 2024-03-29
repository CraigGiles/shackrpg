using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public class ScreenManager
    {

        #region Data


        /// <summary>
        /// Bool to indicate if the screen manager is initialized
        /// </summary>
        bool initialized = false;


        /// <summary>
        /// List of all screens loaded into memory
        /// </summary>
        List<GameScreen> screens = new List<GameScreen>();


        /// <summary>
        /// List of screens needing to be updated
        /// </summary>
        List<GameScreen> screensToUpdate = new List<GameScreen>();


        /// <summary>
        /// List of screens needing to be drawn
        /// </summary>
        List<GameScreen> screensToDraw = new List<GameScreen>();


        /// <summary>
        /// List of screens to be removed from the stack
        /// </summary>
        List<GameScreen> screensToRemove = new List<GameScreen>();

        #endregion


        #region Constructor(s)


        public ScreenManager()
        {
            GameScreen.ScreenManager = this;
        }

        ~ScreenManager()
        {
            UnloadContent();
        }


        public void Initialize(GameScreen entryScreen)
        {
            //adds the initial screen to game
            screens.Add(entryScreen);

            //marks the screenmanager as initialzed
            initialized = true;
        }


        public void UnloadContent()
        {
            //unloads all content from each gamescreen
            foreach (GameScreen s in screens)
                s.UnloadContent();

            //removes all entries from the screens list
            screens.Clear();
            screensToRemove.Clear();
            screensToUpdate.Clear();
        }


        #endregion


        #region Add / Remove Screens


        public void AddScreen(GameScreen screen)
        {
            //Load the new screens content
            screen.LoadContent();

            //add the screen to list
            screens.Add(screen);
        }


        public void RemoveScreen(GameScreen screen)
        {
            screensToRemove.Add(screen);
        }


        public void RemoveScreen(string assetName)
        {
            foreach (GameScreen s in screens)
            {
                if (s.Name == assetName)
                    screensToRemove.Add(s);

            }//end foreach        
        }

        /// <summary>
        /// Clears all screens designated to be removed
        /// </summary>
        private void ClearRemovedScreens()
        {
            //cycle through all gamescreens
            foreach (GameScreen screen in screensToRemove)
            {
                //unload screens content
                screen.UnloadContent();

                //remove screen from lists
                screens.Remove(screen);
                screensToUpdate.Remove(screen);
            }

            //once all screens have been removed, clear list
            screensToRemove.Clear();
        }


        #endregion


        #region Update Screens

        public void Update(GameTime gameTime)
        {
            //Update audio engine
            Globals.AudioManager.Update(gameTime);

            //Clears any screens removed from game
            ClearRemovedScreens();

            //get user input
            Globals.Input.GetUserInput(gameTime);

            //clears the update list
            screensToUpdate.Clear();

            //populates the update list
            foreach (GameScreen screen in screens)
            {
                screensToUpdate.Add(screen);
            }

            //reverses so we're updating "top down"
            screensToUpdate.Reverse();

            //updates all screens until reaching a "BlocksUpdate"
            foreach (GameScreen screen in screensToUpdate)
            {
                screen.Update(gameTime);

                if (screen.BlocksUpdate)
                    break;
            }
        }


        #endregion


        #region Draw Screens

        public void Draw(GameTime gameTime)
        {
            //populate screensToDraw list
            foreach (GameScreen s in screens)
            {
                screensToDraw.Add(s);
            }

            //reverse list to draw "Top Down"
            screensToDraw.Reverse();

            //draws all screens until reaching a 'block draw'
            foreach (GameScreen screen in screensToDraw)
            {
                screen.Draw(gameTime);

                if (screen.BlocksDraw)
                    break;
            }
        

        }


        /// <summary>
        /// Draws a list of active screens in screenmanager
        /// </summary>
        public void DrawScreenList()
        {


            //initialize counter
            int count = 2;
            Globals.SpriteBatch.DrawString(Globals.Font,
                "Active Screens:",
                new Vector2(5, 20),
                Color.Yellow);

            //loop through game screens, drawing each name
            foreach (GameScreen screen in screens)
            {
                Globals.SpriteBatch.DrawString(Globals.Font,
                    screen.Name,
                    new Vector2(5, 15 * count),
                    Color.White);

                count++;
            }

        }

        #endregion
        
    }//end class
}//end namespace
