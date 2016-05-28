using System.Web.Mvc;
using System.Web.Routing;

namespace TiendaMusica.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Albums",
             url: "{artista}/{album}/{action}",
             defaults: new { controller = "Albums", action = "Editar"  }
             );


            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
            
          

           
          
           

            //routes.MapRoute(
            //    name:"Artista",
            //    url: "tienda/{artista}/{action}",
            //    defaults: new { controller="Artistas", action="Perfil"}
            //    );

            
        }
    }
}
