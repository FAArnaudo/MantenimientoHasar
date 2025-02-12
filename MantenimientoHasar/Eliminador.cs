using System;
using System.Collections.Generic;
using System.IO;
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
                // Cada verificacion, si hay un else, se debe redactar en el Log si hay un error.
                if (VerificarDirectorio(ruta))
                {
                    // Generar variable de la lista y checkear si es distinto de null.
                    foreach (string directorio in ObtenerSubCarpetas(ruta))
                    {
                        Eliminar(directorio, archivo);
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Instance.WriteLog($"Error de acceso denegado al intentar eliminar el archivo. Excepción: {e.Message}", LogType.t_error);
            }
            catch (IOException e)
            {
                Log.Instance.WriteLog($"Error de entrada/salida al intentar eliminar el archivo. Excepción: {e.Message}", LogType.t_error);
            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error en el método EliminarArchivos. Excepción: {e.Message}", LogType.t_error);
            }
        }

        /// <summary>
        /// Verifica que exista la ruta del proyecto.
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public bool VerificarDirectorio(string ruta)
        {
            bool existe = false;

            if (Directory.Exists(ruta))
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Devuelve los nombres de los subdirectorios (con sus rutas de acceso) del directorio especificado.
        /// </summary>
        /// <param name="rutaDirectorio"></param>
        /// <returns></returns>
        public List<string> ObtenerSubCarpetas(string ruta)
        {
            List<string> directorios = null;

            try
            {
                //Inicializacion del objeto posterior a la busqueda de las carpetas
                

                // Esta Ok, pero considerar usar Directory.GetDirectories...

                DirectoryInfo di = new DirectoryInfo(ruta);

                // Verificar si esta vacia antes de crear innecesariamente la lista y retorne una lista null
                DirectoryInfo[] dirs = di.GetDirectories("*", SearchOption.AllDirectories);

                if (dirs.Count() != 0)
                {
                    directorios = new List<string>();
                    foreach (DirectoryInfo dir in dirs)
                    {
                        directorios.Add(dir.FullName);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error al obtener las subcarpetas. Excepción {e.Message}", LogType.t_error);
            }

            return directorios;
        }

        /// <summary>
        /// Dentro de la carpeta, compara archivo por archivo.
        /// Si hay coincidencia, se elimina dicho archivo.
        /// </summary>
        /// <param name="carpeta"></param>
        /// <param name="archivo"></param>
        public void Eliminar(string carpeta, string archivo)
        {
            try
            {
                string[] archivos = Directory.GetFiles(carpeta, "*", SearchOption.TopDirectoryOnly);

                foreach (string item in archivos)
                {
                    // Compara el nombre del archivo actual con el archivo especificado
                    if (Path.GetFileName(item).StartsWith(archivo, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Delete(item);  // Elimina el archivo si coincide
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Instance.WriteLog($"Error de acceso denegado al intentar eliminar el archivo en {carpeta}. Excepción: {e.Message}", LogType.t_error);
            }
            catch (IOException e)
            {
                Log.Instance.WriteLog($"Error de entrada/salida al intentar eliminar el archivo en {carpeta}. Excepción: {e.Message}", LogType.t_error);
            }
            catch (Exception e)
            {
                Log.Instance.WriteLog($"Error en el método Eliminar. Excepción: {e.Message}", LogType.t_error);
            }
        }
    }
}
