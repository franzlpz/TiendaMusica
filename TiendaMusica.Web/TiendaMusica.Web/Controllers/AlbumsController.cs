using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TiendaMusica.Infraestructura;
using TiendaMusica.Logica;
using TiendaMusica.Logica.ViewModels;
using TiendaMusica.Web.Utilidades;

namespace TiendaMusica.Web.Controllers
{

    public class AlbumsController : Controller
    {
        #region .:: CONSTRUCTOR ::.
        private readonly AlbumsConsulta servicioAlbums;
        public AlbumsController()
        {
            servicioAlbums = ConstructorServicios.AlbumsConsulta();
        }
        #endregion

        #region .:: GET ::.

        public ActionResult Editar(string artista, string album)
        {
            AlbumsEditarViewModel albums;
            try
            {

                if (!String.IsNullOrEmpty(artista) || !String.IsNullOrEmpty(album))
                {
                    albums = servicioAlbums.Buscar(artista, album);
                    if (albums.ImagenAlbum == null) albums.ImagenAlbum = "default.png";
                    return View(albums);
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
               
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            

        }

        public ActionResult Listar()
        {
            try
            {
                IEnumerable<AlbumsEditarViewModel> listaAlbums =  servicioAlbums.Listar();
                return View(listaAlbums);
            }
            catch (Exception ex)
            {

                return RedirectToAction("AlbumNoFount", "Albums");
            }

            
        }

        public ActionResult Detalle()
        {
            try
            {
                IEnumerable<AlbumsEditarViewModel> listaAlbums = servicioAlbums.Listar();
                return View(listaAlbums);
            }
            catch (Exception ex)
            {

                return RedirectToAction("AlbumNoFount", "Albums");
            }


        }

        #endregion

        #region .:: POST ::.
        [HttpPost]
        public ActionResult Editar(AlbumsEditarViewModel modelo, HttpPostedFileBase archivo, HttpPostedFileBase video, HttpPostedFileBase audio)
        {

            string nombreArchivo = "", nombreAudio = "", nombreVideo = "";

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hubo un Error");
                return View(modelo);
            }

            if (archivo != null)
            {
                nombreArchivo = archivo.FileName.ObtenerMD5() + Path.GetExtension(archivo.FileName);
                if (EsImagen(archivo.ContentType))
                    modelo.ImagenAlbum = GrabarImagen(archivo, nombreArchivo);
            }
            else
            {
                archivo.SaveAs(Path.Combine(Server.MapPath("~/Archivos"), nombreArchivo));
            }
            if (audio!=null)
            {
                 nombreAudio = audio.FileName.ObtenerMD5() + Path.GetExtension(audio.FileName);
                
                if (EsAudio(audio.ContentType))
                    modelo.AudioAlbum = GrabarAudio(audio, nombreAudio);
            }
            else if (video!=null)
            {
                 nombreVideo = video.FileName.ObtenerMD5() + Path.GetExtension(video.FileName);
                if (EsVideo(video.ContentType))
                    modelo.VideoAlbum = GrabarVideo(video, nombreVideo);
            }

            if (modelo.VideoAlbum !=null || modelo.ImagenAlbum != null || modelo.AudioAlbum !=null)
            {
                servicioAlbums.Actualizar(modelo);
                return View(modelo);
            }
          


            return RedirectToAction("Editar", "Albums");

        }


        #endregion

        #region .:: ERROR ::.
        public ActionResult AlbumNoFount()
        {
            return View();

        }

        #endregion

        #region .:: Metodos auxiliares ::.
        private bool EsImagen(string contentType)
        {
            return (contentType.Contains("image"));
        }
        private bool EsVideo(string contentType)
        {
            return (contentType.Contains("video/mp4"));
        }
        private bool EsAudio(string contentType)
        {
            return (contentType.Contains("audio/mpeg"));
        }
        private string GrabarImagen(HttpPostedFileBase archivo, string nombreArchivo)
        {
            string archivoThumbnails = string.Empty;

            MemoryStream ms = new MemoryStream();
            archivo.InputStream.CopyTo(ms);
            Imagen imagen = new Imagen(ms,
                archivo.FileName, archivo.ContentType, Server.MapPath("~/Archivos"));
            imagen.Grabar(nombreArchivo, Server.MapPath("~/Archivos/Thumbnails"));

            archivoThumbnails = Path.Combine(Server.MapPath("~/Archivos/Thumbnails"), "thumbnail_" + nombreArchivo);

            return "thumbnail_" + nombreArchivo;


        }

        private string GrabarVideo(HttpPostedFileBase archivo, string nombreArchivo)
        {
            string archivoThumbnails = string.Empty;

            MemoryStream ms = new MemoryStream();
            archivo.InputStream.CopyTo(ms);
            AudioVideo video = new AudioVideo(ms,
                archivo.FileName, archivo.ContentType, Server.MapPath("~/Archivos/mp4"));
            video.Grabar(nombreArchivo);

            return  nombreArchivo;


        }

        private string GrabarAudio(HttpPostedFileBase archivo, string nombreArchivo)
        {

            MemoryStream ms = new MemoryStream();
            archivo.InputStream.CopyTo(ms);
            AudioVideo audio = new AudioVideo(ms,
                archivo.FileName, archivo.ContentType, Server.MapPath("~/Archivos/mp3"));
            audio.Grabar(nombreArchivo);

            return nombreArchivo;


        }

        #endregion
    }
}
