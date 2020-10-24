using NAudio.Wave;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TT_Games_Explorer.Renderer.Audio.Cache
{
    public class WaveCachedSound : CachedSound
    {
        public WaveCachedSound(string audioFileName)
        {
            using (var audioFileReader = new AudioFileReader(audioFileName))
            {
                // TODO: could add resampling in here if required
                WaveFormat = audioFileReader.WaveFormat;

                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;

                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    wholeFile.AddRange(readBuffer.Take(samplesRead));

                AudioData = wholeFile.ToArray();
            }
        }

        public WaveCachedSound(Stream sound)
        {
            using (var audioFileReader = new WaveFileReader(sound))
            {
                // TODO: could add resampling in here if required
                WaveFormat = audioFileReader.WaveFormat;

                var sp = audioFileReader.ToSampleProvider();

                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var sourceSamples = (int)(audioFileReader.Length / (audioFileReader.WaveFormat.BitsPerSample / 8));
                var sampleData = new float[sourceSamples];
                int samplesRead;

                while ((samplesRead = sp.Read(sampleData, 0, sourceSamples)) > 0)
                    wholeFile.AddRange(sampleData.Take(samplesRead));

                AudioData = wholeFile.ToArray();
            }
        }
    }
}