using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using NAudio.Codecs;
using static ArmyChat_TestFlight.WaveProvider;


namespace ArmyChat_TestFlight
{
    public partial class MainWindow : Window
    {
        private WaveIn recorder;
        private BufferedWaveProvider bufferedWaveProvider;
        private SavingWaveProvider savingWaveProvider;
        private WaveOut player;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            recorder = new WaveIn();
            recorder.DataAvailable += RecorderOnDataAvailable;

            bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            savingWaveProvider = new SavingWaveProvider(bufferedWaveProvider, "temp.wav");

            player = new WaveOut();
            player.Init(savingWaveProvider);

            player.Play();
            recorder.StartRecording();

            
        }

        public void Checkvisibilty()
        {
            if (label2.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Visible");
            }
        }

        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            recorder.StopRecording();
            player.Stop();
            savingWaveProvider.Dispose();

        }

        private void OnButton_Click(object sender, RoutedEventArgs e)
        {
            label2.Visibility = Visibility.Visible;
        }

        private void OffButton_Click(object sender, RoutedEventArgs e)
        {
            label2.Visibility = Visibility.Hidden;
        }

        private void ChkButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}