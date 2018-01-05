using System;
using System.Text;
using System.Threading;
using System.IO;
namespace WarfaceAssistant
{
    class EventListener
    {
        private Thread listener;
        public EventListener() { }
        public bool startListening()
        {
            if (listener == null || listener.IsAlive == false)
            {
                listener = new Thread(listenToEvents);
                listener.SetApartmentState(ApartmentState.STA);
                listener.Priority = ThreadPriority.Lowest;
                listener.Start();
            }
            return true;
        }
        public bool stopListening()
        {
            listener.Abort();
            return true;
        }
        
        private bool GetActiveWindowTitle(string wTitle)
        {
            const int nChars = 64;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = WinAPI.GetForegroundWindow();
            if (WinAPI.GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString().Equals(wTitle);
            }
            return false;
        }
        private void listenToEvents()
        {
            Notification notification_window = new Notification();
            DateTime last_write_time;
            FileStream logfile_stream = null;
            byte[] temp = new byte[256];
            string line;
            string buffer;
            bool caught_event = false;
            long cur_len;
            logfile_stream = new FileStream(WarfaceAssistantSettings.path_log, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            while (true)
            {
                cur_len = logfile_stream.Length;
                if (cur_len < 256) continue;
                logfile_stream.Seek(cur_len - 256, SeekOrigin.Begin);
                cur_len = logfile_stream.Read(temp, 0, 256);
                last_write_time = File.GetLastWriteTimeUtc(WarfaceAssistantSettings.path_log);
                line = System.Text.Encoding.Default.GetString(temp);
                for (int i = 0; i < cur_len - 46;)
                {
                    if (line[i++] == ']')
                    {
                        if (line.Substring(++i, 4).Equals("Open"))
                        {
                            i += 14;
                            buffer = line.Substring(i, 30); 
                            if (/*buffer.StartsWith("lobby_inventory") /*||*/
                                buffer.StartsWith("lobby_preinvite_accept") ||
                                buffer.StartsWith("lobby_invite_accept")) //lobby_inventory
                            {
                                caught_event = true;
                                i += 30;
                            }
                            else caught_event = false;
                        }
                        else caught_event = false;
                    }
                }
                if (caught_event)
                {
                    if (DateTime.UtcNow - last_write_time < TimeSpan.FromSeconds(0.3))
                    {
                        if (GetActiveWindowTitle("Warface") == false)
                            notification_window.ShowDialog();
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
