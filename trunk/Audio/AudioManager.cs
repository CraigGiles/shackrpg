using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace ShackRPG
{
    public class AudioManager
    {

        #region Audio Data


        /// <summary>
        /// Audio Engine
        /// </summary>
        AudioEngine audioEngine;


        /// <summary>
        /// Games Sound Bank
        /// </summary>
        SoundBank soundBank;


        /// <summary>
        /// Games Wave Bank
        /// </summary>
        WaveBank waveBank;


        #endregion


        #region Constructor(s)


        /// <summary>
        /// Creates a new Audio Manager object
        /// </summary>
        /// <param name="settingsFile">Settings XGS file</param>
        /// <param name="waveBankFile">Wave Bank XWB file</param>
        /// <param name="soundBankFile">Sound Bank XSB file</param>
        public AudioManager(string settingsFile, string waveBankFile, string soundBankFile)
        {
            try
            {
                audioEngine = new AudioEngine(settingsFile);
                waveBank = new WaveBank(audioEngine, waveBankFile);
                soundBank = new SoundBank(audioEngine, soundBankFile);
            }
            catch (NoAudioHardwareException)
            {
                // silently fall back to silence
                audioEngine = null;
                waveBank = null;
                soundBank = null;
            }
        }


        /// <summary>
        /// Destructor
        /// </summary>
        ~AudioManager()
        {
            soundBank.Dispose();
            waveBank.Dispose();
            audioEngine.Dispose();
        }


        #endregion


        #region Cue Methods


        /// <summary>
        /// Gets a cue by cue name
        /// </summary>
        /// <param name="cueName">Cue to obtain</param>
        /// <returns>Cue</returns>
        public Cue GetCue(string cueName)
        {
            //if string is null or any object is null, return null
            if (String.IsNullOrEmpty(cueName) ||
                (audioEngine == null) ||
                (soundBank == null) ||
                (waveBank == null))
            {
                return null;
            }

            //else, return the cue
            return soundBank.GetCue(cueName);
        }


        /// <summary>
        /// Plays a Cue by name
        /// </summary>
        /// <param name="cueName">cue to play</param>
        public void PlayCue(string cueName)
        {
            //if objects are not null, play cue
            if ((audioEngine != null) &&
                (soundBank != null) &&
                (waveBank != null))
            {
                soundBank.PlayCue(cueName);
            }
        }


        #endregion


        #region Music

        /// <summary>
        /// Music Cue
        /// </summary>
        private Cue musicCue;


        /// <summary>
        /// Stack of cues by name
        /// </summary>
        private Stack<string> musicCueNameStack = new Stack<string>();



        /// <summary>
        /// Plays music by cue name
        /// </summary>
        /// <param name="cueName">music to play</param>
        public void PlayMusic(string cueName)
        {
            musicCueNameStack.Clear();
            PushMusic(cueName);
        }


        /// <summary>
        /// Pushes a new cue onto the stack
        /// </summary>
        /// <param name="cueName">cue to push</param>
        public void PushMusic(string cueName)
        {
            //if objects are not null, push cue
            if ((audioEngine != null) &&
                (soundBank != null) &&
                (waveBank != null))
            {
                musicCueNameStack.Push(cueName);

                //if music cue is null, or name doesn't = cueName
                if ((musicCue == null) || musicCue.Name != cueName)
                {
                    //if music cue is null, stop and dispose
                    if (musicCue != null)
                    {
                        musicCue.Stop(AudioStopOptions.AsAuthored);
                        musicCue.Dispose();
                        musicCue = null;
                    }
                                        
                    musicCue = GetCue(cueName);
                    if (musicCue != null)
                    {
                        musicCue.Play();
                    }
                }
            }
        }


        /// <summary>
        /// Pops music off of the stack
        /// </summary>
        public void PopMusic()
        {
            //start the new music cue
            if ((audioEngine != null) &&
                (soundBank != null) &&
                (waveBank != null))
            {
                string cueName = null;
                if (musicCueNameStack.Count > 0)
                {
                    musicCueNameStack.Pop();

                    if (musicCueNameStack.Count > 0)
                    {
                        cueName = musicCueNameStack.Peek();
                    }
                }

                if (musicCue == null || musicCue.Name != cueName)
                {
                    if (musicCue != null)
                    {
                        musicCue.Stop(AudioStopOptions.AsAuthored);
                        musicCue.Dispose();
                        musicCue = null;
                    }

                    if (String.IsNullOrEmpty(cueName))
                    {
                        musicCue = GetCue(cueName);

                        if (musicCue != null)
                        {
                            musicCue.Play();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Clears the music stack, stopping all music
        /// </summary>
        public void StopMusic()
        {
            musicCueNameStack.Clear();

            if (musicCue != null)
            {
                musicCue.Stop(AudioStopOptions.AsAuthored);
                musicCue.Dispose();
                musicCue = null;
            }
        }


        #endregion


        #region Updating Methods


        /// <summary>
        /// Updates the audio manager
        /// </summary>
        /// <param name="gameTime">Game Time</param>
        public void Update(GameTime gameTime)
        {
            //upates the audio engine
            if (audioEngine != null)
            {
                audioEngine.Update();
            }
        }


        #endregion
        
    }
}
