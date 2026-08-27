using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string name, string email);
    }
}
