using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Utils
{
    class Audio
    {
        SoundEffectInstance song;
        SoundEffectInstance sfxInstance;
        private Audio()
        {

        }

        private static Audio instance;
        public static Audio GetInstance()
        {
            if (instance == null)
            {
                instance = new Audio();
            }

            return instance;
        }

        public void PlaySong(SoundEffect song_)
        {
            StopSong();
            song = song_.CreateInstance();
            song.Play();
        }

        public void StopSong()
        {
            if (song != null)
            {
                song.Stop();
            }
        }

        public void Play(SoundEffect sfx)
        {
            sfxInstance = sfx.CreateInstance();
            sfxInstance.Play();
        }

        public void Stop()
        {
            sfxInstance.Stop();
        }
    }
}
