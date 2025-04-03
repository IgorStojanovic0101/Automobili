using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Abstraction
{
    public interface IHubNotifier
    {
        Task NotifyUser(string message, string connectionId);
    }

}
