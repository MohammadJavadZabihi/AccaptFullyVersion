using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies.Interface
{
    public interface IApiCallServies
    {
        Task<HttpResponseMessage> SendPostReauest(string url, object data);

        Task<HttpResponseMessage> SendPatchRequest(string url, object data);
    }
}
