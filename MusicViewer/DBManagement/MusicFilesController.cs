using Microsoft.Win32;
using MusicViewer.Scripts.Audio;
using System.IO;
using System.Linq;
using System.Windows;

namespace MusicViewer.DBManagement
{
    public class MusicFilesController
    {
        private MusicDataClassesDataContext _dataContext;
        private IAudioPlayer _activeAudioPlayer;

        public IAudioPlayer ActiveAudioPlayer { get => _activeAudioPlayer; }

        public MusicFilesController(MusicDataClassesDataContext context)
        {
            _dataContext = context;
        }

        public void PushFile(object selectedItem)
        {
            Music selectedMusic = selectedItem as Music;

            if (selectedMusic == null)
            {
                return;
            }

            var openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3",
                Title = "Select MP3 File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] fileData = File.ReadAllBytes(openFileDialog.FileName);
                selectedMusic.MusicFile = new MusicFile();
                selectedMusic.MusicFile.mp3 = fileData;

                MessageBox.Show($"Music File '{Path.GetFileName(openFileDialog.FileName)}' loaded successfully.");
            }
        }

        public void RequestFile(object selectedItem)
        {
            if (selectedItem is Music music)
            {
                if (music.FileID == null)
                {
                    MessageBox.Show("MP3 is empty.");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "MP3 files (*.mp3)|*.mp3",
                    FileName = $"{music.Title}.mp3",
                    Title = "Select folder to save MP3."
                };


                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, music.MusicFile.mp3);
                    MessageBox.Show($"File saved successfully as {saveFileDialog.FileName}.");
                }
            }
            else
            {
                MessageBox.Show("Selected item is not a music!");
            }
        }

        public void DeleteFile(object selectedItem)
        {
            if (selectedItem is Music music)
            {
                if (music.FileID == null)
                {
                    MessageBox.Show("MP3 is already empty.");
                    return;
                }

                var fileToDelete = _dataContext.MusicFiles.FirstOrDefault(f => f.Id == music.FileID);

                if (fileToDelete != null)
                {
                    _dataContext.MusicFiles.DeleteOnSubmit(fileToDelete);
                    music.MusicFile = null;
                    music.FileID = null;
                }
                else
                {
                    MessageBox.Show("Can not find the file to delete");
                }
            }
            else
            {
                MessageBox.Show("Selected item is not a music!");
            }
        }

        public void PlaySong(object selectedItem)
        {
            if (selectedItem is Music music)
            {
                if (music.FileID == null)
                {
                    MessageBox.Show("MP3 is empty.");

                    return;
                }

                _activeAudioPlayer?.Stop();
                _activeAudioPlayer = new AudioPlayerMp3();
                _activeAudioPlayer.PlayFromBytes(music.MusicFile.mp3);
            }
        }
    }
}
