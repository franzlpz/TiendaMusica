using TiendaMusica.Data.Repositorio;
using TiendaMusica.Logica.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace TiendaMusica.Logica
{

    public class AlbumsConsulta
    {
        private readonly ITiendaMusicaRepository db;

        public AlbumsConsulta(ITiendaMusicaRepository repositorio)
        {
            db = repositorio;
        }

        public AlbumsEditarViewModel Buscar(string artista, string album)
        {
            // string nombreArtista = Utilidades.TransformarNombre(artista);
            var data = db.Albums.SingleOrDefault(a => a.Title == album && a.Artist.Name == artista);
            if (data != null)
            {
                return new AlbumsEditarViewModel
                {
                    AlbumId = data.AlbumId,
                    Title = data.Title,
                    Artist = data.Artist.Name,
                    ImagenAlbum = data.ImagenAlbum   
                           
                };
               
            }
            else {
                return null;
            }
        }
        public bool Actualizar(AlbumsEditarViewModel oAlbum)
        {
            try
            {
                var modelo = db.Albums.SingleOrDefault(a => a.AlbumId == oAlbum.AlbumId);
                modelo.Title = oAlbum.Title;
                modelo.ImagenAlbum = oAlbum.ImagenAlbum;
                db.Albums.Update(modelo);
                db.Commit();
                
                return true;
            }
            catch (System.Exception ex)
            {

                return false;
            }
            

           
        }
        public IEnumerable<AlbumsEditarViewModel> Listar()
        {
            return db.Albums.GetAll().Select(o => new AlbumsEditarViewModel
            { AlbumId = o.AlbumId,
             Artist = o.Artist.Name,
             Title = o.Title,
             ImagenAlbum = o.ImagenAlbum }).ToList();
        }
    }
}
