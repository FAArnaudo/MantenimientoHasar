using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace MantenimientoHasar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTextBox();
        }

        #region CLOSE AND MINIMIZE
        private void BtnCierre_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion

        #region TIMER
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < listBox.Items.Count - 1)
            {
                currentIndex++;
                UpdateTextBox();
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateTextBox();
            }
        }

        private void UpdateTextBox()
        {
            TextBoxTimer.Text = ((ListBoxItem)listBox.Items[currentIndex]).Content.ToString();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                currentIndex = listBox.SelectedIndex;
                UpdateTextBox();
                listBox.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region BUSCAR
        private void BtnRutaPN_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer, // Carpeta raíz (opcional)
                Description = "Selecciona una carpeta" // Descripción del diálogo (opcional)
            };

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                TextBoxRutaProyecto.Text = folderPath;
            }
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
