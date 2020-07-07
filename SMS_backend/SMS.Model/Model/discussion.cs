namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dotnet.discussion")]
    public partial class discussion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public discussion()
        {
            discussion1 = new HashSet<discussion>();
        }

        [Key]
        public int dis_id { get; set; }

        public int course_id { get; set; }

        public int user_id { get; set; }

        [Required]
        [StringLength(200)]
        public string content { get; set; }

        public int? dis_dis_id { get; set; }

        public int is_comment { get; set; }

        public virtual course course { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<discussion> discussion1 { get; set; }

        public virtual discussion discussion2 { get; set; }

        public virtual user user { get; set; }
    }
}
