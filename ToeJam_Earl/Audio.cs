using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ToeJam_Earl
{
    public class Audio : Component
    {
        private SoundEffectInstance soundInstance;
        private Audio quedAudio;
        private bool audioHasPlayed = false;

        public float Volume
        {
            get => soundInstance.Volume;
            set => soundInstance.Volume = MathHelper.Clamp(value, 0f, 1f);
        }

        public float Pitch
        {
            get => soundInstance.Pitch;
            set => soundInstance.Pitch = MathHelper.Clamp(value, -1f, 1f);
        }

        public bool isLooped
        {
            get => soundInstance.IsLooped;
            set => soundInstance.IsLooped = value;
        }

        public SoundState State => soundInstance.State;

        public Audio(SoundEffect soundEffect, float volume = 1f, float pitch = 0f, bool isLooped = false)
        {
            soundInstance = soundEffect.CreateInstance();
            Volume = volume;
            Pitch = pitch;
            this.isLooped = isLooped;
        }

        public void Play()
        {
            if (State != SoundState.Playing)
            {
                soundInstance.Play();
                audioHasPlayed = true;
            }
        }

        public void Pause()
        {
            if (State == SoundState.Playing)
            {
                soundInstance.Pause();
            }
        }

        public void Resume()
        {
            if (State == SoundState.Paused)
            {
                soundInstance.Resume();
            }
        }

        public void Stop()
        {
            if (State != SoundState.Stopped)
            {
                soundInstance.Stop();
            }
        }

        public void SetQuedAudio(Audio audio)
        {
            quedAudio = audio;
        }

        public void Update(GameTime gameTime)
        {
            if (State == SoundState.Stopped && audioHasPlayed && quedAudio != null)
            {
                quedAudio.Play();
                audioHasPlayed = false;
            }
        }

        public bool isFinished()
        {
            return !soundInstance.IsLooped && soundInstance.State == SoundState.Stopped;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                soundInstance?.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}