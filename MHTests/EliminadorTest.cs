using MantenimientoHasar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHTests
{
    [TestClass]
    public class EliminadorTest
    {
        private string ruta;
        Eliminador eliminador;
        List<string> carpetas;

        [TestInitialize]
        public void TestInitialize()
        {
            // Ruta donde se crea el directorio
            ruta = @"C:\Syst\NewProject";

            eliminador = new Eliminador();

            carpetas = new List<string>
            {
                "Admi1",
                "Admi2",
                "Admi4",
                "Admi8",
                "Admi16"
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (Directory.Exists(@"C:\Syst"))
            {
                Directory.Delete(@"C:\Syst", true);
            }
        }

        [TestMethod]
        public void VerificarDirectorio_Existe()
        {
            // Arange

            Directory.CreateDirectory(ruta);

            // Act

            bool condition = eliminador.VeridicarDirectorio(ruta);

            // Assert

            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void VerificarDirectorio_No_Existe()
        {
            // Arange

            Directory.CreateDirectory(ruta);

            // Act

            bool condition = eliminador.VeridicarDirectorio(ruta);

            // Assert

            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void ObtenerSubCarpetas_ReturnOk()
        {
            // Arange
            foreach (string carpeta in carpetas)
            {
                string subCarpeta = Path.Combine(ruta, carpeta);

                Directory.CreateDirectory(subCarpeta);
            }

            List<string> value;

            // Act
            value = eliminador.ObtenerSubCarpetas(ruta);

            // Assert
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void ObtenerSubCarpetas_Exc_ReturnNull()
        {
            // Arange
            List<string> value;

            // Act
            value = eliminador.ObtenerSubCarpetas(ruta);

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ObtenerSubCarpetas_EsVacia_ReturnNull()
        {
            // Arange
            foreach (string carpeta in carpetas)
            {
                string subCarpeta = Path.Combine(ruta, carpeta);

                Directory.CreateDirectory(subCarpeta);
            }

            List<string> value;

            // Act
            value = eliminador.ObtenerSubCarpetas(ruta);

            // Assert
            Assert.IsNull(value);
        }
    }
}
