using System.IO;

namespace TiendaMusica.Web.Utilidades
{
    public class AudioVideo
    {
        public AudioVideo(Stream audioVideo, string archivo, string contentType, string rutaServidor)
        {
            this.Bytes = audioVideo;
            this.NombreArchivo = archivo;
            this.TipoContenido = contentType;
            this.Ruta = rutaServidor;
        }

        public Stream Bytes { get; private set; }
        public string NombreArchivo { get; private set; }
        public string Ruta { get; private set; }
        public string TipoContenido { get; private set; }

        public void Grabar(string nombre)
        {
            GrabarArchivoOriginal(nombre);
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