using System;
using System.Windows;
using System.Windows.Input;

namespace WarfaceAssistant
{
    /// <summary>
    /// Логика взаимодействия для Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        public Notification()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = WinAPI.FindWindow(null, "Warface");
            if (hWnd == IntPtr.Zero) return;
            Visibility = Visibility.Hidden;
            WinAPI.ShowWindow(hWnd, 3);
        }
    }
}
