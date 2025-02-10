using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoHasar
{
    public class Eliminador
    {
        public Eliminador() { }

        /// <summary>
        /// Metodo que se encarga de gestionar la eliminacion del archivo y sus derivados.
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        public void EliminarArchivos(string ruta, string archivo)
        {
            try
            {
                // TODO: Implemetnar la logica de borrado del archivo.
            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error en el metodo EliminarArchivos. Excepción: {e.Message}", LogType.t_error);
            }
        }
    }
}
