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

        public void WriteLog(string message)
        {

        }
    }
}
