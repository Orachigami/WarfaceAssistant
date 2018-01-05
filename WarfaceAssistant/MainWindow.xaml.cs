using System;
using System.Windows;

namespace WarfaceAssistant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EventListener el;
        public MainWindow()
        {
            if (WinAPI.FindWindow(null, "Warface Assistant") != IntPtr.Zero)
                Environment.Exit(1);
            InitializeComponent();
            el = new EventListener();
            WarfaceAssistantSettings.FindLogFile();
            el.startListening();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void changeStyles(bool state)
        {
            if (state == true)
            {
                button.Style = Resources["ButtonClicked"] as Style;
                button1.Style = Resources["ButtonStyle"] as Style;
            }
            else
            {
                button1.Style = Resources["ButtonClicked"] as Style;
                button.Style = Resources["ButtonStyle"] as Style;
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            changeStyles(button.IsFocused);
            el.startListening();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            changeStyles(button.IsFocused);
            el.stopListening();
        }
    }
}
