using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace KDembeck.ChatEngine.Data
{
    public class DataUtil : IDataUtil
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SqlConnection conn;
        private string sqlConnectionString;  
        public DataUtil()
        {
            sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebChat"].ConnectionString;
            conn = new SqlConnection(sqlConnectionString);
        }

        public string getSystemConfigSettingValue(string settingName)
        {
            string queryString = "SELECT SettingValuefrom SystemConfig WHERE SettingName = '" + settingName + "'";
            SqlDataReader rdr = null;
            string settingValue = string.Empty;       
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    settingValue = rdr["SettingValue"].ToString();
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
            return settingValue;
        }

        public List<SystemConfigInfo> getAllSystemConfigSettingsAndValued()
        {
            string queryString = "SELECT SettingName, SettingValue, Description from SystemConfig";
            SqlDataReader rdr = null;
            List<SystemConfigInfo> systemConfigInfoList = new List<SystemConfigInfo>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    SystemConfigInfo systemConfigInfo = new SystemConfigInfo();
                    systemConfigInfo.settingName = rdr["SettingName"].ToString();
                    systemConfigInfo.settingValue = rdr["SettingValue"].ToString();
                    systemConfigInfo.description = rdr["Description"].ToString();
                    systemConfigInfoList.Add(systemConfigInfo);
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
            return systemConfigInfoList;
        }

        public List<TenantInfo> getAllTenants()
        {
            string queryString = "SELECT GuidId, Username, Password, TenantDomain, ClientId, Enabled, InstanceId FROM Tenant WHERE Enabled = 1";
            SqlDataReader rdr = null;
            List<TenantInfo> tenantInfoList = new List<TenantInfo>();
            try
            {   
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);                
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TenantInfo tenantInfo = new TenantInfo();
                    tenantInfo.guidId = rdr["GuidId"].ToString();
                    tenantInfo.clientId = rdr["ClientId"].ToString();
                    tenantInfo.instanceId = rdr["InstanceId"].ToString();
                    tenantInfo.tenantDomain = rdr["TenantDomain"].ToString();
                    tenantInfo.username = rdr["Username"].ToString();
                    tenantInfo.password = rdr["Password"].ToString();

                    //get tenantQueues here

                    tenantInfoList.Add(tenantInfo);
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
            return tenantInfoList;
        }

        public List<QueueInfo> getAllQueuesForTenant(string tenantGuidId)
        {
            string queryString = "SELECT GuidID, QueueName FROM Queue WHERE TenantId = '" + tenantGuidId + "'";
            SqlDataReader rdr = null;
            List<QueueInfo> queueInfoList = new List<QueueInfo>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    QueueInfo queueInfo = new QueueInfo();

                    queueInfo.queueId = rdr["GuidId"].ToString();
                    queueInfo.queueName = rdr["QueueName"].ToString();

                    //get queueAgents here

                    queueInfoList.Add(queueInfo);
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
            return queueInfoList;
        }

        public List<QueueAgentInfo> getAllAgentsForQueue(string queueGuidId)
        {
            string queryString = "SELECT Agent.SipUri, Agent.DisplayName, Agent.LastSessionOffered, Agent.LastSessionEnded, AgentQueue.PriorityLevel, Agent.Locked " +
                    "FROM AgentQueue JOIN Agent ON Agent.SipUri = AgentQueue.AgentSipUri " +
                    "WHERE AgentQueue.QueueId = '" + queueGuidId + "' AND Agent.Enabled = 1";
            SqlDataReader rdr = null;
            List<QueueAgentInfo> queueAgentsList = new List<QueueAgentInfo>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {   
                    QueueAgentInfo queueAgentInfo = new QueueAgentInfo();

                    queueAgentInfo.displayName = rdr["DisplayName"].ToString();
                    if (rdr["LastSessionEnded"] != null)
                        queueAgentInfo.lastSessionEnded = (DateTime)rdr["LastSessionEnded"];
                    else
                        queueAgentInfo.lastSessionEnded = DateTime.MinValue;
                    if (rdr["LastSessionOffered"] != null)
                        queueAgentInfo.lastSessionOffered = (DateTime)rdr["LastSessionOffered"];
                    else
                        queueAgentInfo.lastSessionOffered = DateTime.MinValue;
                    queueAgentInfo.sipUri = rdr["SipUri"].ToString();
                    queueAgentInfo.locked = (bool)rdr["Locked"];
                    queueAgentsList.Add(queueAgentInfo);
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
            return queueAgentsList;
        }

        public List<TenantAgentInfo> getAllAgentsForTenant(string tenantGuidId)
        {
            

            string queryString = "SELECT Agent.SipUri, Agent.DisplayName, Agent.LastSessionOffered, Agent.LastSessionEnded, Agent.Locked " +
                    "FROM Agent " +
                    "WHERE Agent.TenantId = '" + tenantGuidId + "' AND Agent.Enabled = 1";

            SqlDataReader rdr = null;
            List<TenantAgentInfo> tenantAgentsList = new List<TenantAgentInfo>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TenantAgentInfo tenantAgentInfo = new TenantAgentInfo();

                    tenantAgentInfo.displayName = rdr["DisplayName"].ToString();
                    if (rdr["LastSessionEnded"] != DBNull.Value)
                        tenantAgentInfo.lastSessionEnded = (DateTime)rdr["LastSessionEnded"];
                    else
                        tenantAgentInfo.lastSessionEnded = DateTime.MinValue;
                    if (rdr["LastSessionOffered"] != DBNull.Value)
                        tenantAgentInfo.lastSessionOffered = (DateTime)rdr["LastSessionOffered"];
                    else
                        tenantAgentInfo.lastSessionOffered = DateTime.MinValue;
                    tenantAgentInfo.sipUri = rdr["SipUri"].ToString();
                    tenantAgentInfo.locked = (bool)rdr["Locked"];                 

                    tenantAgentsList.Add(tenantAgentInfo);
                }
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();                
            }

            foreach (TenantAgentInfo tenantAgentInfo in tenantAgentsList)
            {
                SqlDataReader rdr2 = null;
                try
                {

                    string queueLevelsQueryString = "SELECT QueueId, PriorityLevel FROM AgentQueue WHERE AgentSipUri = '" + tenantAgentInfo.sipUri + "'";
                    List<QueueLevel> queueLevels = new List<QueueLevel>();
                    SqlCommand cmd2 = new SqlCommand(queueLevelsQueryString, conn);
                    rdr2 = cmd2.ExecuteReader();
                    while (rdr2.Read())
                    {
                        QueueLevel queueLevel = new QueueLevel();
                        queueLevel.queueId = rdr2["QueueId"].ToString();
                        queueLevel.level = (int)rdr2["PriorityLevel"];
                        queueLevels.Add(queueLevel);
                    }

                    tenantAgentInfo.queueLevels = queueLevels;
                }
                finally
                {
                    if (rdr2 != null)
                        rdr2.Close();
                }
            }

            if (conn != null)
                conn.Close();

            return tenantAgentsList;
        }

        public void unlockAllAgents()
        {
            string queryString = "UPDATE Agent SET Locked = 0";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

        }

        public void unlockAgent(string agentSipUri)
        {
            string queryString = "UPDATE Agent SET Locked = 0 WHERE AgentSipUri = '" + agentSipUri + "'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public void lockAgent(string agentSipUri)
        {
            string queryString = "UPDATE Agent SET Locked = 1 WHERE AgentSipUri = '" + agentSipUri + "'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public List<TenantIdInfo> getTenantAndQueueIds()
        {
            List<TenantIdInfo> tenantResources = new List<TenantIdInfo>();
            List<TenantInfo>  tenantInfos = getAllTenants();
            foreach (TenantInfo tenantInfo in tenantInfos)
            {
                TenantIdInfo tenantResource = new TenantIdInfo();
                tenantResource.tenantName = tenantInfo.tenantDomain;
                tenantResource.tenantGuidId = tenantInfo.guidId;

                List<QueueIdInfo> queueResources = new List<QueueIdInfo>();
                List<QueueInfo> queueInfos = getAllQueuesForTenant(tenantInfo.guidId);
                foreach (QueueInfo queueInfo in queueInfos)
                {
                    QueueIdInfo queueResource = new QueueIdInfo();
                    queueResource.queueName = queueInfo.queueName;
                    queueResource.queueGuidId = queueInfo.queueId;
                    queueResources.Add(queueResource);
                }
                tenantResource.tenantQueues = queueResources;
                tenantResources.Add(tenantResource);                
            }
            return tenantResources;
        }
    }
}
