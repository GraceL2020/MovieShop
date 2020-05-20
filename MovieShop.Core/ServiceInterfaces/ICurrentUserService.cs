using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAuthenticated { get; }
        string Name { get; }
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
