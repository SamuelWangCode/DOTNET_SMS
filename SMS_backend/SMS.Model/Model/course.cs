namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dotnet.course")]
    public partial class course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public course()
        {
            discussion = new HashSet<discussion>();
            student_class = new HashSet<student_class>();
        }

        [Key]
        public int course_id { get; set; }

        [StringLength(20)]
        public string course_name { get; set; }

        [StringLength(200)]
        public string broadCast { get; set; }

        public int? dep_id { get; set; }

        public int? t_id { get; set; }

        public int? credits { get; set; }

        public virtual department department { get; set; }

        public virtual teacher teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<discussion> discussion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_class> student_class { get; set; }
    }
}
