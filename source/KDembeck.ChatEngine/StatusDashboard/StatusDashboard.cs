using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Data;
using KDembeck.UcwaWebApiClient.Resources;


namespace KDembeck.ChatEngine.Dashboard
{
    public class StatusDashboard : IStatusDashboard
    {
        //public Action<>
        public event EventHandler<QueueStatusUpdatedEventArgs> QueueStatusUpdated;

        //private List<TenantConversation> tenantConversations;
        private List<TenantAgent> tenantAgents;
        private List<QueueAgent> queueAgents;
        private List<TenantQueue> tenantQueues;
        private List<IChatTenant> chatTenants;

        internal StatusDashboard(List<IChatTenant> tenantChatList)
        {
            this.chatTenants = tenantChatList;
            tenantQueues = new List<TenantQueue>();
            //foreach (ChatTenant chatTenant in tenantChatList)
            //{   
            //    //chatTenant.agentDispatcher.TenantAgentStatusUpdated += Handle_OnTenantAgentStatusUpdated;
            //    //chatTenant.agentDispatcher.QueueAgentStatusUpdated += Handle_OnQueueAgentStatusUpdated;
            //}
        }

        private void getAllTenantQueues()
        {
            if (tenantQueues == null)
                tenantQueues = new List<TenantQueue>();

            foreach (IChatTenant chatTenant in chatTenants)
            {
                foreach (IChatQueue chatQueue in chatTenant.chatQueueManager.chatQueues)
                {
                    TenantQueue tenantQueue = new TenantQueue(chatQueue.queueId, chatTenant.tenantId);
                    if (!tenantQueues.Contains(tenantQueue))
                    {
                        tenantQueues.Add(tenantQueue);
                    }
                }
            }
        }

        private void getTenantQueuesForTenant(string tenantId)
        {
            if (tenantQueues == null)
                tenantQueues = new List<TenantQueue>();

            IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
            if (chatTenant != null)
            {
                foreach (IChatQueue chatQueue in chatTenant.chatQueueManager.chatQueues)
                {
                    TenantQueue tenantQueue = new TenantQueue(chatQueue.queueId, chatTenant.tenantId);
                    if (!tenantQueues.Contains(tenantQueue))
                    {
                        tenantQueues.Add(tenantQueue);
                    }
                }
            }
        }

        private void getTenantAgents()
        {
            if (tenantAgents == null)
                tenantAgents = new List<TenantAgent>();

            foreach (IChatTenant chatTenant in chatTenants)
            {
                List<IChatAgent> chatAgents = chatTenant.chatAgentManager.getAllAgentsForTenant();
                foreach (IChatAgent chatAgent in chatAgents)
                {
                    TenantAgent tenantAgent = new TenantAgent(chatTenant.tenantId, chatAgent.sipUri);
                    if (!tenantAgents.Contains(tenantAgent))
                    {
                        tenantAgents.Add(tenantAgent);
                    }
                }
            }
        }

        private void getQueueAgents()
        {
            if (queueAgents == null)
                queueAgents = new List<QueueAgent>();

            getAllTenantQueues();
            foreach (IChatTenant chatTenant in chatTenants)
            {
                List<TenantQueue> thisTenantQueues = tenantQueues.Where(x => x.tenantId == chatTenant.tenantId).ToList();

                foreach (TenantQueue tenantQueue in thisTenantQueues)
                {
                    List<IChatAgent> queueChatAgents = chatTenant.chatAgentManager.getAllAgentsForQueue(tenantQueue.queueId);
                    foreach (IChatAgent chatAgent in queueChatAgents)
                    {
                        QueueAgent queueAgent = new QueueAgent(chatAgent.sipUri, tenantQueue.queueId);
                        if (!queueAgents.Contains(queueAgent))
                        {
                            queueAgents.Add(queueAgent);
                        }
                    }
                }
            }
        }

        public Dictionary<string, string> getTenantsIdsAndDomainNames()
        {
            Dictionary<string, string> tenants = new Dictionary<string, string>();
            foreach (ChatTenant chatTenant in chatTenants)
            {
                tenants.Add(chatTenant.tenantId, chatTenant.tenantDomainName);
            }
            return tenants;
        }

        public Dictionary<string, string> getQueueIdsAndNamesForTenant(string tenantId)
        {
            Dictionary<string, string> queues = new Dictionary<string, string>();
            IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
            if (chatTenant != null && chatTenant.initialized == true)
            {
                foreach (IChatQueue chatQueue in chatTenant.chatQueueManager.chatQueues)
                {
                    queues.Add(chatQueue.queueId, chatQueue.queueDisplayName);
                }
            }
            return queues;
        }

        //public AgentStatus getAgentStatus(string agentSipUri)
        //{
        //    AgentStatus agentStatus = null;

        //    getTenantAgents();
        //    string tenantId = tenantAgents.Where(x => x.agentSipUri == agentSipUri).Select(x => x.tenantId).FirstOrDefault();
        //    if (tenantId != null)
        //    {
        //        IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
        //        if (chatTenant != null)
        //        {
        //            IChatAgent chatAgent = chatTenant.agentDispatcher.chatAgents.Where(x => x.sipUri == agentSipUri).FirstOrDefault();
        //            if (chatAgent != null)
        //            {
        //                agentStatus = new AgentStatus();
        //                agentStatus.agentDisplayName = chatAgent.displayName;
        //                agentStatus.agentSipUri = chatAgent.sipUri;
        //                agentStatus.agentState = chatAgent.agentState;                        
        //            }
        //        }
        //    }
        //    return agentStatus;
        //}

        public List<AgentStatus> getAgentsStatusesForQueue(string queueId)
        {
            List<AgentStatus> agentStatuses = new List<AgentStatus>();

            getAllTenantQueues();
            string tenantId = tenantQueues.Where(x => x.queueId == queueId).Select(x => x.tenantId).FirstOrDefault();
            if (tenantId != null)
            {
                IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
                if (chatTenant != null)
                {
                    List<IChatAgent> chatAgents = chatTenant.chatAgentManager.getAllAgentsForQueue(queueId);
                    foreach (IChatAgent chatAgent in chatAgents)
                    {
                        AgentStatus agentStatus = new AgentStatus();
                        agentStatus.agentDisplayName = chatAgent.displayName;
                        agentStatus.agentSipUri = chatAgent.sipUri;
                        agentStatus.agentState = chatAgent.agentState;
                        agentStatuses.Add(agentStatus);
                    }
                }
            }
            return agentStatuses;
        }

        public List<ChatSessionStatus> getWaitingChatSessionStatusesForQueue(string queueId)
        {
            List<ChatSessionStatus> chatSessionStatuses = new List<ChatSessionStatus>();

            getAllTenantQueues();
            string tenantId = tenantQueues.Where(x => x?.queueId == queueId).Select(x => x?.tenantId).FirstOrDefault();
            if (tenantId != null)
            {
                IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
                if (chatTenant != null)
                {
                    IChatQueue chatQueue = chatTenant.chatQueueManager.chatQueues.Where(x => x.queueId == queueId).FirstOrDefault();
                    if (chatQueue != null)
                    {
                        if (chatQueue.currentlyBeingProcessedChatSession != null)
                        {
                            ChatSessionStatus chatSessionStatus = new ChatSessionStatus();
                            chatSessionStatus.queuedTime = chatQueue.currentlyBeingProcessedChatSession.queuedTime;
                            chatSessionStatus.webUserName = chatQueue.currentlyBeingProcessedChatSession.webUserName;
                            chatSessionStatuses.Add(chatSessionStatus);
                        }
                        List<IChatSession> chatSessions = chatQueue.waitingChatSessions.ToList();
                        foreach (IChatSession chatSession in chatSessions)
                        {
                            ChatSessionStatus chatSessionStatus = new ChatSessionStatus();
                            chatSessionStatus.queuedTime = chatSession.queuedTime;
                            chatSessionStatus.webUserName = chatSession.webUserName;
                            chatSessionStatuses.Add(chatSessionStatus);
                        }
                    }
                }
            }
            return chatSessionStatuses;
        }

        public List<ChatSessionStatus> getActiveChatSessionStatusesForQueue(string queueId)
        {
            List<ChatSessionStatus> chatSessionStatuses = new List<ChatSessionStatus>();

            getAllTenantQueues();
            string tenantId = tenantQueues.Where(x => x.queueId == queueId).Select(x => x.tenantId).FirstOrDefault();
            if (tenantId != null)
            {
                IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
                if (chatTenant != null)
                {
                    foreach (IChatQueue chatQueue in chatTenant.chatQueueManager.chatQueues)
                    {
                        foreach (IChatSession chatSession in chatQueue.activeChatSessions)
                        {
                            ChatSessionStatus chatSessionStatus = new ChatSessionStatus();
                            chatSessionStatus.messagingStartTime = chatSession.messagingStartTime;
                            chatSessionStatus.invitedAgentDisplayName = chatSession.invitedAgentDisplayName;
                            chatSessionStatus.queuedTime = chatSession.queuedTime;
                            chatSessionStatus.webUserName = chatSession.webUserName;
                            foreach (string participantName in chatSession.participantDisplayNames)
                            {
                                chatSessionStatus.participantDisplayNames.Add(participantName);
                            }
                            chatSessionStatuses.Add(chatSessionStatus);
                        }
                    }
                }
            }
            return chatSessionStatuses;
        }

        public QueueStatus getQueueStatus(string queueId)
        {
            QueueStatus queueStatus = new QueueStatus();

            getAllTenantQueues();

            string tenantId = tenantQueues.Where(x => x.queueId == queueId).Select(x => x.tenantId).FirstOrDefault();
            if (tenantId != null)
            {
                IChatTenant chatTenant = chatTenants.Where(x => x.tenantId == tenantId).FirstOrDefault();
                if (chatTenant != null && chatTenant.state == TenantState.Started)
                {
                    IChatQueue chatQueue = chatTenant.chatQueueManager.chatQueues.Where(x => x.queueId == queueId).FirstOrDefault();
                    if (chatQueue != null)
                    {
                        queueStatus.queueId = chatQueue.queueId;
                        queueStatus.queueDisplayName = chatQueue.queueDisplayName;
                        queueStatus.numberOfWaitingSessions = chatQueue.numberOfWaitingSessions;
                        queueStatus.numberOfActiveSessions = chatQueue.numberOfActiveSessions;
                    }
                    List<AgentStatus> agentStatuses = getAgentsStatusesForQueue(queueId);
                    foreach (AgentStatus agentStatus in agentStatuses)
                    {
                        switch (agentStatus.agentState)
                        {
                            case AgentState.Available:
                                queueStatus.numberOfAgentsAvailable++;
                                break;
                            case AgentState.InSession:
                                queueStatus.numberOfAgentsInSession++;
                                break;
                            case AgentState.InvitationPending:
                                queueStatus.numberOfAgentsInvitationPending++;
                                break;
                            case AgentState.Offline:
                                queueStatus.numberOfAgentsOffline++;
                                break;
                            case AgentState.PostSession:
                                queueStatus.numberOfAgentsPostSession++;
                                break;
                            case AgentState.Unavailable:
                                queueStatus.numberOfAgentsUnavailable++;
                                break;
                        }
                    }
                    queueStatus.agentStatuses = agentStatuses;
                    queueStatus.waitingChatSessionStatuses = getWaitingChatSessionStatusesForQueue(queueId);
                    queueStatus.activeChatSessionStatuses = getActiveChatSessionStatusesForQueue(queueId);
                }
            }
            return queueStatus;
        }

        //public async Task startWatchingStatuses()
        //{
        //    //get all the statuses for all the tenants, queues, and agents
        //    //start a loop that constantly gets updated status info for all the tenants, queues, and agents
        //    //raise events for changes in status
        //    //fuck this, we'll just use polling for now
        //    return;
        //}

    }
}
