using System;
using System.Collections.Generic;

namespace XogoEngine.Audio
{
    public sealed class SoundManager : IIndexable<Sound, string>
    {
        private Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();

        public void Add(Sound sound, string name)
        {
            if (sounds.ContainsValue(sound))
            {
                throw new ArgumentException(
                    "The given sound is already in the sound manager"
                );
            }
            if (sounds.ContainsKey(name))
            {
                throw new ArgumentException(
                    $"The given name: {name} is already in use in the sound manager"
                );
            }
            sounds.Add(name, sound);
        }

        // time permiting, add remove methods.

        public Sound this[string name] => Get(name);
        public Sound Get(string name)
        {
            if (!sounds.ContainsKey(name))
            {
                throw new ArgumentException(
                    $"The given name: {name} is not in the sound manager"
                );
            }
            return sounds[name];
        }
    }
}
