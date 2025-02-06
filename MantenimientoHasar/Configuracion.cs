using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoHasar
{
    public class Configuracion
    {
        public Configuracion() { }
        public void SetConfiguracion()
        {

        }
        public Datos GetConfiguracion()
        {
            return null;
        }

        public bool ExisteConfiguracion()
        {
            return false;
        }
    }

    public class Datos
    {
        private string rutaProyecto = "";
        private int timeInterval = 0;
        public Datos() { }

        public string RutaProyecto
        {
            get => rutaProyecto;
            set
            {
                if (rutaProyecto != value)
                {
                    rutaProyecto = value;
                }
            }
        }

        public string Archivo => "DLL_IFH2G.log";

        public int TimeInterval
        {
            get => timeInterval;
            set
            {
                if (timeInterval != value)
                {
                    timeInterval = value;
                }
            }
        }
    }
}
