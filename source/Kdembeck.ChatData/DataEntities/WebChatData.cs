namespace Kdembeck.ChatData.DataEntities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WebChatData : DbContext
    {
        public WebChatData()
            : base("name=WebChatData")
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentQueue> AgentQueues { get; set; }
        public virtual DbSet<Queue> Queues { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>()
                .Property(e => e.SipUri)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.TenantId)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .HasMany(e => e.AgentQueues)
                .WithRequired(e => e.Agent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AgentQueue>()
                .Property(e => e.AgentSipUri)
                .IsUnicode(false);

            modelBuilder.Entity<AgentQueue>()
                .Property(e => e.QueueId)
                .IsUnicode(false);

            modelBuilder.Entity<Queue>()
                .Property(e => e.GuidId)
                .IsUnicode(false);

            modelBuilder.Entity<Queue>()
                .Property(e => e.TenantId)
                .IsUnicode(false);

            modelBuilder.Entity<Queue>()
                .Property(e => e.QueueName)
                .IsUnicode(false);

            modelBuilder.Entity<Queue>()
                .HasMany(e => e.AgentQueues)
                .WithRequired(e => e.Queue)
                .HasForeignKey(e => e.QueueId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tenant>()
                .Property(e => e.GuidId)
                .IsUnicode(false);

            modelBuilder.Entity<Tenant>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Tenant>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Tenant>()
                .Property(e => e.TenantDomain)
                .IsUnicode(false);

            modelBuilder.Entity<Tenant>()
                .Property(e => e.ClientId)
                .IsUnicode(false);

            modelBuilder.Entity<Tenant>()
                .Property(e => e.InstanceId)
                .IsUnicode(false);

            modelBuilder.Entity<Tenant>()
                .HasMany(e => e.Agents)
                .WithOptional(e => e.Tenant)
                .HasForeignKey(e => e.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasMany(e => e.Queues)
                .WithOptional(e => e.Tenant)
                .HasForeignKey(e => e.TenantId);
        }
    }
}
