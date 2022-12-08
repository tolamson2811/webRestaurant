using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Shared.Enum
{
    public enum UserRole
    {
        Admin = 1,
        User = 2,
    }
    public enum StatusCodeEnum
    {
        Accepted = 202,
        AlreadyReported = 208,
        NotFound = 404,
        OK = 200,
    }
}
