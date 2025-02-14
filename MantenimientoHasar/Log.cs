using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoHasar
{
    public class Log
    {
        private static Log instance = null;
        private Log() { }
        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log();
                }
                return instance;
            }
        }

        /// <summary>
        /// Es el log informativo. Crea una carpeta y un archivo, en el que informa las eliminaciones
        /// y errores que puedan ocurrir
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logType"></param>
        public void WriteLog(string message, LogType logType)
        {
            try
            {
                // Crea la carpeta si no existe, en la carpeta base del programa.
                string path = AppDomain.CurrentDomain.BaseDirectory + "/Log";
                string logFile = "log" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

                if (!Directory.Exists(path))
                {
                    _ = Directory.CreateDirectory(path);
                }

                // Borra el log del dia anterior
                string deleteFile = "log" + DateTime.Now.Subtract(new TimeSpan(3, 0, 0, 0)).ToString("dd-MM-yyyy") + ".txt";
                if (File.Exists(Environment.CurrentDirectory + "/Log/" + deleteFile))
                {
                    File.Delete(Environment.CurrentDirectory + "/Log/" + deleteFile);
                }

                // Log message with correspondant log level
                switch (logType)
                {
                    case LogType.t_info:
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, logFile), true))
                        {
                            outputFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "  INFO:    " + message);
                        }
                        break;
                    case LogType.t_error:
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, logFile), true))
                        {
                            outputFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "  ERROR:   " + message);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // No escribe
            }
        }
    }

    public enum LogType
    {
        t_info = 0,
        t_error
    }
}
