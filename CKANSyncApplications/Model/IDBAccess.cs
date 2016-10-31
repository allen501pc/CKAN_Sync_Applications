using System;
namespace CKANSyncApplications.Model
{
    interface IDBAccess
    {
        void ObtainFromDB();
        bool WriteToDB();
    }
}
