//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiendaMusica.DominioDBFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Track
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Track()
        {
            this.InvoiceLine = new HashSet<InvoiceLine>();
            this.Playlist = new HashSet<Playlist>();
        }
    
        public int TrackId { get; set; }
        public string Name { get; set; }
        public Nullable<int> AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public Nullable<int> GenreId { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public Nullable<int> Bytes { get; set; }
        public decimal UnitPrice { get; set; }
    
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine> InvoiceLine { get; set; }
        public virtual MediaType MediaType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}
