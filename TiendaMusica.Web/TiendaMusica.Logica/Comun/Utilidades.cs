using System;
using System.IO;

namespace TiendaMusica.Logica.Comun
{
    internal class Utilidades
    {
        internal static string TransformarNombre(string nombre)
        {
            string nuevoNombre = nombre.Replace("_", " ");
            return nuevoNombre.Replace("-", "/");
        }

      
    }
}