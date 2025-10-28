﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UserNotFoundException(string Email):NotFoundException($"This User{Email} Is Not Found")
    {
        
    }
}
