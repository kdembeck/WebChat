namespace Kdembeck.ChatData.DataEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgentQueue")]
    public partial class AgentQueue
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string AgentSipUri { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string QueueId { get; set; }

        public int? PriorityLevel { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual Queue Queue { get; set; }
    }
}
