using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace KDembeck.UcwaWebApiClient
{
    public enum ClientState { LoggedIn, LoggedOut, LogginIn, LoggingOut };
    public interface IUcwaClient
    {
        ClientState state { get; }
        IApplicationResource application { get; }
        IEventHandler events { get; }
        Task<bool> loginAs(string Username, string Password, string Tenant, string ClientId, string AutoDiscoverUrl, string LoginBaseUrl, string UserAgent, string EndpointId, string Culture, string instanceId,
            AudioPreference? audioPreference = null, TimeSpan? inactiveTimeout = null, string phoneNumber = null, PreferredAvailability? signInAs = null, List<MessageFormat> supportedMessagingFormats = null, List<ModalityType> supportedModalities = null, TimeSpan? voipFallbackToPhoneAudioTimeOut = null);
        Task logOut();
        Task<bool> testCredentials(string Username, string Password, string Tenant, string ClientId, string AutoDiscoverUrl, string LoginBaseUrl);

    }
}
