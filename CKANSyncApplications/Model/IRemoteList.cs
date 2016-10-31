using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKANSyncApplications.Model
{
    interface IRemoteList: IRemoteAccess
    {
        IDictionary<string, object> GetList();
    }
}
