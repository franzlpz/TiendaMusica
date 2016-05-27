using System;
using System.Drawing;
using System.IO;


namespace TiendaMusica.Web.Utilidades
{
    public  class Imagen
    {
        public Imagen(Stream imagen, string archivo, string contentType, string rutaServidor)
        {
            this.Bytes = imagen;
            this.NombreArchivo = archivo;
            this.TipoContenido = contentType;
            this.Ruta = rutaServidor;
        }

        public Stream Bytes { get; private set; }
        public string NombreArchivo { get; private set; }
        public string Ruta { get; private set; }
        public string TipoContenido { get; private set; }

        public void Grabar(string nombre, string thumbnailPath)
        {
            GrabarArchivoOriginal(nombre);
            GuardarThumbnail(nombre, thumbnailPath);
        }

        private void GuardarThumbnail(string nombre, string thumbnailPath)
        {
            string thumbnailNombre = "thumbnail_" + nombre;

            using (var image = Image.FromFile(Path.Combine(Ruta, nombre)))
            {
                var escala = Math.Min((float)200 / image.Width, (float)200 / image.Height);
                var scalaWidth = Convert.ToInt32(image.Width * escala);
                var scalaHeight = Convert.ToInt32(image.Height * escala);
                using (var thumbnail = image.GetThumbnailImage(scalaWidth, scalaHeight, null, IntPtr.Zero))
                {
                    thumbnail.Save(Path.Combine(thumbnailPath, thumbnailNombre));
                }
            }

        }

        private void GrabarArchivoOriginal(string nombre)
        {
            if (File.Exists(Path.Combine(Ruta, nombre)))
            {
                File.Delete(Path.Combine(Ruta, nombre));
                //throw new InvalidOperationException("El archivo ya existe");

            }
            FileStream fs = new FileStream(Path.Combine(Ruta, nombre), FileMode.Create);
            Bytes.Position = 0;
            Bytes.CopyTo(fs);
            fs.Position = 0;
            fs.Flush();
            fs.Close();
        }
    }
}
