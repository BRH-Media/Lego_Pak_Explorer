using NAudio.Wave;
using System.IO;

namespace TT_Games_Explorer.Renderer.Audio.Cache
{
    public class CachedSound
    {
        public float[] AudioData { get; set; }
        public WaveFormat WaveFormat { get; set; }

        public CachedSound(string fileName)
        {
            //blank constructor
        }

        public CachedSound(Stream sound)
        {
            //blank constructor
        }

        public CachedSound()
        {
            //blank constructor
        }
    }
}