namespace Kdembeck.ChatData.DataEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Agent")]
    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            AgentQueues = new HashSet<AgentQueue>();
        }

        [Key]
        [StringLength(100)]
        public string SipUri { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [StringLength(36)]
        public string TenantId { get; set; }

        public bool? Enabled { get; set; }

        public virtual Tenant Tenant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentQueue> AgentQueues { get; set; }
    }
}
