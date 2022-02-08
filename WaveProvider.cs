using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NAudio.Wave;

namespace ArmyChat_TestFlight
{
    class WaveProvider
    {
        public class SavingWaveProvider : IWaveProvider, IDisposable
        {
            private readonly IWaveProvider sourceWaveProvider;
            private readonly WaveFileWriter writer;
            private bool isWriterDisposed;

            private readonly MainWindow _mainWindow = new MainWindow();

            public SavingWaveProvider(IWaveProvider sourceWaveProvider, string wavFilePath)
            {
                this.sourceWaveProvider = sourceWaveProvider;
                writer = new WaveFileWriter(wavFilePath, sourceWaveProvider.WaveFormat);
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                var read = sourceWaveProvider.Read(buffer, offset, count);
                
                if(read > 5)Debug.WriteLine("Number of Threads Exceeds Target Number");
                if (count > 0 && !isWriterDisposed)
                {
                    writer.Write(buffer, offset, read);
                }

                if (count == 0)
                {
                    if (Windows.VisibilityProperty.Name == "returnval")
                    {
                        Console.WriteLine("123");
                    }
                    Dispose();
                }

                return read;
            }

            public class Windows : MainWindow
            {
                public void ReturnValue()
                {
                    label2.Visibility = Visibility.Hidden;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void Prop(string PropName)
            {
            }

            public virtual string ReturnVal()
            {
                string val = "";

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).label2.Visibility = Visibility.Hidden;
                    }
                }

                if (_mainWindow.label2.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("True");
                    var a = 1;
                    var b = 2;
                    if (_mainWindow.Visibility == Visibility.Visible) b = a + b;
                    Console.WriteLine(b);
                    val = "true";
                    

                }

                if (_mainWindow.label2.Visibility == Visibility.Hidden)
                {
                    MessageBox.Show("Invisible");
                    val = "false";
                }

                return val;
            }

            public WaveFormat WaveFormat
            {
                get { return sourceWaveProvider.WaveFormat; }
            }

            public void Dispose()
            {
                if (!isWriterDisposed)
                {
                    isWriterDisposed = true;
                    writer.Dispose();
                }
            }
        }
    }
}