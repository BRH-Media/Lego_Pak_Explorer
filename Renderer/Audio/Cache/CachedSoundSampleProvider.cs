﻿using NAudio.Wave;
using System;

namespace TT_Games_Explorer.Renderer.Audio.Cache
{
    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound _cachedSound;
        private long _position;

        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            _cachedSound = cachedSound;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = _cachedSound.AudioData.Length - _position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(_cachedSound.AudioData, _position, buffer, offset, samplesToCopy);
            _position += samplesToCopy;
            return (int)samplesToCopy;
        }

        public WaveFormat WaveFormat => _cachedSound.WaveFormat;
    }
}