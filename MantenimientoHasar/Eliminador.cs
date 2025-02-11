﻿using System;
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
                // TODO: Implemetnar la logica de borrado del archivo.
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
        public bool VeridicarDirectorio(string ruta)
        {
            bool existe = false;

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
