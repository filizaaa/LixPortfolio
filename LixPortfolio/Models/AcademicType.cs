//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LixPortfolio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AcademicType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicType()
        {
            this.AcademicLives = new HashSet<AcademicLife>();
        }
    
        public int ID { get; set; }
        public string AcademyType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicLife> AcademicLives { get; set; }
    }
}
