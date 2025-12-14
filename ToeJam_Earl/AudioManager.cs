using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeJam_Earl
{
    public class AudioManager
    {
        private Dictionary<string, SoundEffect> soundLibrary;
        private List<Audio> activeAudio;
        private ContentManager content;
        private Dictionary<string, Song> musicLibrary;

        private float masterVolume = 1f;

        public float MasterVolume
        {
            get => masterVolume;
            set
            {
                masterVolume = MathHelper.Clamp(value, 0f, 1f);
                foreach (var audio in activeAudio)
                {
                    audio.Volume = masterVolume;
                }
            }
        }

        public AudioManager(ContentManager content)
        {
            this.content = content;
            soundLibrary = new Dictionary<string, SoundEffect>();
            activeAudio = new List<Audio>();
            musicLibrary = new Dictionary<string, Song>();
        }

        public void LoadSound(string assetName)
        {
            if (!soundLibrary.ContainsKey(assetName))
            {
                SoundEffect soundEffect = content.Load<SoundEffect>(assetName);
                soundLibrary.Add(assetName, soundEffect);
            }
        }

        public void LoadSong(string assetName)
        {
            if (!musicLibrary.ContainsKey(assetName))
            {
                Song song = content.Load<Song>(assetName);
                musicLibrary.Add(assetName, song);
            }
        }

        public Audio PlaySound(string assetName, float volume = 1f, float pitch = 1f, bool isLooped = false)
        {
            if (soundLibrary.ContainsKey(assetName))
            {
                SoundEffect soundEffect = soundLibrary[assetName];
                Audio audio = new Audio(soundEffect, volume * masterVolume, pitch, isLooped);
                audio.Play();
                activeAudio.Add(audio);
                return audio;
            }
            else
            {
                throw new Exception($"Sound '{assetName}' not found in sound library. Make sure to load it first.");
            }
        }

        public Audio PlaySong(string assetName, float volume = 1f, bool isLooped = true)
        {
            if (musicLibrary.ContainsKey(assetName))
            {
                Song song = musicLibrary[assetName];
                MediaPlayer.Volume = volume * masterVolume;
                MediaPlayer.IsRepeating = isLooped;
                MediaPlayer.Play(song);
                return null; // Songs are managed by MediaPlayer, so we return null
            }
            else
            {
                throw new Exception($"Song '{assetName}' not found in music library. Make sure to load it first.");
            }
        }

        public void PauseAll()
        {
            foreach (var audio in activeAudio)
            {
                if (audio.State == SoundState.Playing)
                {
                    audio.Pause();
                }
            }
        }

        public void ResumeAll()
        {
            foreach (var audio in activeAudio)
            {
                if (audio.State == SoundState.Paused)
                {
                    audio.Resume();
                }
            }
        }

        public void StopAll()
        {
            foreach (var audio in activeAudio)
            {
                audio.Stop();
            }
            activeAudio.Clear();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = activeAudio.Count - 1; i >= 0; i--)
            {
                var audio = activeAudio[i];
                if (audio.State == SoundState.Stopped && !audio.isLooped)
                {
                    activeAudio.RemoveAt(i);
                }
            }
        }

        public void UnloadAllSounds()
        {
            StopAll();
            soundLibrary.Clear();
        }

        private Audio[] _Textures;
    }
}