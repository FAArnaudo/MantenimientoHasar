# MantenimientoHasar

Es utilizado para eliminar un archivo residuo, creado por las impresoras Hasar.
Este archivo crece en tamaño y en número, provocando lentitud en la maquina de los usuarios y en el mismo proceso de impresión.

El archivo se denomina "DLL_IHF2G.log", pero al crearce uno nuevo, el anterior se renombra. Este proceso ocurre indefinidas veces.

## Funcionamiento - Limpieza de archivo 🧹

- Se especifica la ruta donde este archivo (y sus derivados) es creado.
- Se indica el tiempo de ejecución del proceso.
- Se inicia el mantenimiento: Aquí, el sistema se ejecuta y comienza el tiempo de espera establecido. Una vez cumplido el lapso de tiempo, se vuelve a ejecutar el proceso. En cada ejecución, la aplicacion busca el archivo en la ruta especificada y donde encuentra coincidencia del mismo y derivados, los elimina.

## Tecnología usada

- **Lenguaje de Programación:**

    <img src="https://img.icons8.com/nolan/512/c-sharp-logo.png" width="45" height="45" />