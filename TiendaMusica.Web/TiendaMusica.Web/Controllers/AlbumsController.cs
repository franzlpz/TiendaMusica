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
        public ActionResult Editar(AlbumsEditarViewModel modelo, HttpPostedFileBase archivo)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hubo un Error");
                return View(modelo);
            }

            string nombreArchivo = archivo.FileName.ObtenerMD5() + Path.GetExtension(archivo.FileName);

            if (EsImagen(archivo.ContentType))
            {

                modelo.ImagenAlbum = GrabarImagen(archivo, nombreArchivo);
                servicioAlbums.Actualizar(modelo);

                return View(modelo);

            }
            else
            {
                archivo.SaveAs(Path.Combine(Server.MapPath("~/Archivos"), nombreArchivo));
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

        #endregion
    }
}
