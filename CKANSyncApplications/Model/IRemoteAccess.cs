using System;
namespace CKANSyncApplications.Model
{
    interface IRemoteAccess
    {
        void ObtainFromRemote(string url, string apiKey);
    }
}
