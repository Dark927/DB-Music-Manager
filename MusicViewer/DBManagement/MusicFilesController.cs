using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace MusicViewer.DBManagement
{
    public class MusicFilesController
    {
        private ObservableCollection<Music> _musicDataList;

        public MusicFilesController(object musicTable)
        {
            if (musicTable is ObservableCollection<Music> musicDataList)
            {
                _musicDataList = musicDataList;
            }
            else
            {
                MessageBox.Show($"{this.ToString()} : music table is not correct!");
            }
        }

        public void PushFile(object selectedItem)
        {
            if (_musicDataList is ObservableCollection<Music> musics)
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

                if (music.MusicFile != null)
                {
                    music.MusicFile.mp3 = null;
                    music.MusicFile = null;
                    music.FileID = null;
                    MessageBox.Show($"File deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("Selected item is not a music!");
            }
        }
    }
}
