using AdoDotNetProj.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoDotNetProj.Manager.Interface
{
   public interface ILoginManager
    {
        Task<AuthResult> Login(LoginVm loginVm);
    }
}
