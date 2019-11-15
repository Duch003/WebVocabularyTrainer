using Microsoft.AspNetCore.Identity;
using RestApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public class UserService
    {
        public async Task<Result<IdentityUser>> CreateUser()
        {
            return null;
        }

        public async Task<Result<IdentityUser>> AuthenticateUser()
        {
            return null;
        }

        public async Task<Result<IdentityUser>> UpdateUser()
        {
            return null;
        }
    }
}
