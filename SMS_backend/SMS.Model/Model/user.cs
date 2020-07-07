namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dotnet.user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            discussion = new HashSet<discussion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        public int? dep_id { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(11)]
        public string tel { get; set; }

        [Required]
        [StringLength(20)]
        public string role { get; set; }

        public virtual department department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<discussion> discussion { get; set; }

        public virtual student student { get; set; }

        public virtual teacher teacher { get; set; }
    }
}
