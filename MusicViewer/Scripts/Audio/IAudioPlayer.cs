

using NAudio.Wave;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MusicViewer.Scripts.Audio
{
    public interface IAudioPlayer
    {
        DispatcherTimer Timer { get; }
        WaveStream AudioStream { get; }
        event EventHandler<AudioArgs> OnAudioLoaded;
        event EventHandler<AudioArgs> OnTimerTick;

        Task PlayFromBytes(byte[] bytes);
        void Stop();
    }
}
