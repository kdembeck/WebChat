namespace Kdembeck.ChatData.DataEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Queue")]
    public partial class Queue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Queue()
        {
            AgentQueues = new HashSet<AgentQueue>();
        }

        [Key]
        [StringLength(36)]
        public string GuidId { get; set; }

        [StringLength(36)]
        public string TenantId { get; set; }

        [StringLength(200)]
        public string QueueName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentQueue> AgentQueues { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
