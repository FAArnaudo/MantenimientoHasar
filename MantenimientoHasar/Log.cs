using System;
using System.Collections.Generic;
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
            // TODO: Metodo de implementacion para crear si no existe y escribir en el log.
        }
    }

    public enum LogType
    {
        t_info = 0,
        t_error
    }
}
