using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantenimientoHasar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MHTests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestCleanup]
        public void TestCleanUp()
        {
            // Obtener la ruta base del ejecutable
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Nombre del archivo a verificar
            string fileName = "Config.ini";
            string filePath = Path.Combine(basePath, fileName);

            // Verificar si el archivo existe
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void SetConfiguracion_ReturnTrue()
        {
            //Arrange
            string ruta = @"C:\Sistema\proy_nuevo";
            int time = 30;

            Datos datos = new Datos
            {
                RutaProyecto = ruta,
                TimeInterval = time
            };

            //Act
            bool condition = Configuracion.SetConfiguracion(datos);

            //Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void SetConfiguracion_ReturnFalse()
        {
            //Arrange
            string ruta = null;
            int time = 30;

            Datos datos = new Datos
            {
                RutaProyecto = ruta,
                TimeInterval = time
            };

            //Act
            bool condition = Configuracion.SetConfiguracion(datos);

            //Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void GetConfiguracion_ReturnData_OK()
        {
            //Arrange
            string ruta = @"C:\Sistema\proy_nuevo";
            int time = 30;

            // Nombre del archivo a verificar
            string fileName = "Config.ini";

            //Crea el archivo config.ini
            using (StreamWriter outputFile = new StreamWriter(fileName, false))
            {
                outputFile.WriteLine(ruta.Trim());             //1°   Razon Social
                outputFile.WriteLine(time);                    //2°   Timer
            }

            string expected = ruta;

            //Act
            string actual = Configuracion.GetConfiguracion().RutaProyecto;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetConfiguracion_ReturnNull()
        {
            //Arrange

            //Act
            Datos value = Configuracion.GetConfiguracion();

            //Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ExisteConfiguracion_True()
        {
            //Arrange
            string ruta = @"C:\Sistema\proy_nuevo";
            int time = 30;

            // Nombre del archivo a verificar
            string fileName = "Config.ini";

            //Crea el archivo config.ini
            using (StreamWriter outputFile = new StreamWriter(fileName, false))
            {
                outputFile.WriteLine(ruta.Trim());             //1°   Ruta del proyecto
                outputFile.WriteLine(time);                    //2°   Timer
            }

            //Act
            bool condition = Configuracion.ExisteConfiguracion();

            //Assert
            Assert.IsTrue(condition);

        }

        [TestMethod]
        public void ExisteConfiguracion_False()
        {
            //Arrange

            //Act
            bool condition = Configuracion.ExisteConfiguracion();

            //Assert
            Assert.IsFalse(condition);
        }
    }
}
