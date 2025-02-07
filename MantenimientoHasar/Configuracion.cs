using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoHasar
{
    public class Configuracion
    {
        private static readonly string configFile = Environment.CurrentDirectory + "/Config.ini";

        /// <summary>
        /// Este metodo crea si es necesario y guarda los datos ingresados en un registro/archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static bool SetConfiguracion(Datos datos)
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error en el metodo SetConfiguracion. Excepción: {e.Message}");
            }

            return false;
        }
        public static Datos GetConfiguracion()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error en el metodo GetConfiguracion. Excepción: {e.Message}");
            }

            return null;
        }

        public static bool ExisteConfiguracion()
        {
            try
            {

            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error en el metodo ExisteConfiguracion. Excepción: {e.Message}");
            }

            return false;
        }
    }

    public class Datos
    {
        private string rutaProyecto = "";
        private int timeInterval = 0;
        public Datos() { }

        /// <summary>
        /// La ruta donde se encuentra el archivo a eliminarse.
        /// </summary>
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

        /// <summary>
        /// Nombre del archivo.
        /// </summary>
        public string Archivo => "DLL_IFH2G.log";

        /// <summary>
        /// Almacena el tiempo de ejecucion, en minutos.
        /// </summary>
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
