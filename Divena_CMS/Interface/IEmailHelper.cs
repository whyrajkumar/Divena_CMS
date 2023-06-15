using System.Collections.Generic;
using System.Threading.Tasks;

namespace Divena_CMS.Interface
{
    public interface IEmailHelper
    {
        Task SendCustomEmailAsync(string visitName, string email, string subject, string message);
        IEnumerable<string> GetClientIP(string localIp);
        void SendMail(string email, string subject, string messagebody);

    }
}

