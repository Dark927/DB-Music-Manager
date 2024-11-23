using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MusicViewer.Scripts.Audio
{
    public class AudioPlayerMp3 : IAudioPlayer
    {
        private WaveOutEvent _outputDevice;
        private Mp3FileReader _reader;

        public DispatcherTimer Timer { get; private set; }
        public WaveStream AudioStream { get => _reader; }

        public event EventHandler<AudioArgs> OnAudioLoaded;
        public event EventHandler<AudioArgs> OnTimerTick;


        public AudioPlayerMp3()
        {
            InitTimer();
        }

        private void InitTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1);
            Timer.Tick += OnDispatcherTimerTick;
        }

        public async Task PlayFromBytes(byte[] bytes)
        {
            await Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
                    _reader = new Mp3FileReader(memoryStream);
                    _outputDevice = new WaveOutEvent();
                    _outputDevice.Init(_reader);

                    OnAudioLoaded?.Invoke(this, new AudioArgs { TotalSeconds = _reader.TotalTime.TotalSeconds });

                    _outputDevice.Play();
                    Timer.Start();

                    while (_outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Task.Delay(100).Wait();
                    }
                }
            });
        }

        public void Stop()
        {
            Timer.Stop();
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _reader?.Dispose();
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            OnTimerTick?.Invoke(this, new AudioArgs { TotalSeconds = _reader.CurrentTime.TotalSeconds });
        }
    }
}
