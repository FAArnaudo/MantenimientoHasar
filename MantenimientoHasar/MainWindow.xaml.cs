using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MessageBox = System.Windows.MessageBox;

namespace MantenimientoHasar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon;
        private int currentIndex = 0;
        public DispatcherTimer DispacherTimer { get; set; }
        public Eliminador Eliminador { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Eliminador = new Eliminador();

            //TODO: Implementar logica de verificacion en la configuración.
            if (Configuracion.ExisteConfiguracion())
            {
                int timer = Configuracion.GetConfiguracion().TimeInterval;

                foreach (ListBoxItem listBoxItem in listBox.Items)
                {
                    int item = Convert.ToInt32(listBoxItem.Content) * 60;

                    if (item == timer)
                    {
                        UpdateTextBox();
                    }
                    currentIndex++;
                }

                TB_ProyNuevo.Text = Configuracion.GetConfiguracion().RutaProyecto;

                StartTask(timer);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTextBox();
            CB_Habilita.IsEnabled = false;

            Icon = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "data-cleansing.ico")));
            SetupNotifyIcon();
        }

        /// <summary>
        /// Inicio del timer y seteo de configuracion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implementar la logica de inicio con el button.

            if (!TB_ProyNuevo.Text.Equals("") && TB_ProyNuevo.Text.Trim().ToLowerInvariant().EndsWith(@"sistema\proy_nuevo"))
            {
                MessageBoxResult result = MessageBox.Show($"Datos ingresador:" +
                                                     $"\n\tRuta:\t{TB_ProyNuevo.Text}" +
                                                     $"\n\tTiempo:\t{TB_Timer.Text}" +
                                                     $"\n¿Los datos son correctos?", "Confirmación",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Question);
                // Verificar la respuesta del usuario
                if (result == MessageBoxResult.Yes)
                {
                    decimal timer = Convert.ToDecimal(TB_Timer.Text) * 60;

                    Datos datos = new Datos
                    {
                        RutaProyecto = TB_ProyNuevo.Text,
                        TimeInterval = Convert.ToInt32(timer)
                    };

                    if (Configuracion.SetConfiguracion(datos))
                    {
                        _ = MessageBox.Show("Datos guardados correctamente.");

                        CB_Habilita.IsEnabled = true;
                        CB_Habilita.IsChecked = true;
                        GMid.IsEnabled = false;

                        StartTask(datos.TimeInterval);
                    }
                    else
                    {
                        _ = MessageBox.Show("Error al guardar los datos. Verifique los formatos.");
                    }
                }
            }
            else
            {
                _ = MessageBox.Show($"La ruta ingresada es incorrecta: {TB_ProyNuevo.Text}");
            }
        }

        /// <summary>
        /// Método para iniciar la tarea periódica.
        /// </summary>
        private void StartTask(int interval)
        {
            // TODO: Implementacion de la clase Timer
            // Si ya existe un timer en ejecución, se detiene para reiniciarlo
            if (DispacherTimer != null)
            {
                DispacherTimer.Stop();
            }

            DispacherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(interval)
            };
            DispacherTimer.Tick += Timer_Tick;
            DispacherTimer.Start();
        }

        /// <summary>
        /// Método que se ejecuta en cada tick del timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            string ruta = Configuracion.GetConfiguracion().RutaProyecto;
            string archivo = Configuracion.GetConfiguracion().Archivo;

            Eliminador.EliminarArchivos(ruta, archivo);
            Log.Instance.WriteLog($"Tarea ejecutada a las: {DateTime.Now.ToLongTimeString()}", LogType.t_info);
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
            TB_Timer.Text = ((ListBoxItem)listBox.Items[currentIndex]).Content.ToString();
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
                TB_ProyNuevo.Text = folderPath;
            }
        }
        #endregion

        #region PROCESO EN SEGUNDO PLANO
        private void SetupNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "data-cleansing.ico")),
                Visible = false,
                Text = "Controlador De Surtidores"
            };

            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            ContextMenu contextMenu = new ContextMenu();
            _ = contextMenu.MenuItems.Add("Restaurar", (s, e) => RestoreFromTray());
            _ = contextMenu.MenuItems.Add("Salir", (s, e) => ExitApplication());

            notifyIcon.ContextMenu = contextMenu;
        }
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            RestoreFromTray();
        }
        private void RestoreFromTray()
        {
            Show();
            WindowState = WindowState.Normal;
            notifyIcon.Visible = false;
        }
        private void ExitApplication()
        {
            notifyIcon.Visible = false;
            System.Windows.Application.Current.Shutdown();
        }
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            notifyIcon.Dispose();
            base.OnClosed(e);
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CB_Habilita_Unchecked(object sender, RoutedEventArgs e)
        {
            GMid.IsEnabled = true;
            CB_Habilita.IsEnabled = false;
        }
    }
}
