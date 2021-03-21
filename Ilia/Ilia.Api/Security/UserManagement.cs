using Ilia.CrossCutting.Exceptions;
using Ilia.CrossCutting.Interop.Dto.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ilia.Api.Security
{
    public class UserManagement
    {
        private static IList<UserInfo> _users;

        public static void GetUser(Guid token)
        {
            _users = _users ?? new List<UserInfo>();

            var user = _users.FirstOrDefault(f => f.Token.Equals(token));
            if (user == null)
                throw new ForbiddenException("Without permission");
        }

        public static LoginDto RegisterUser(UserDto profile)
        {
            _users = _users ?? new List<UserInfo>();

            if (!_users.Any(a => a.Id.Equals(profile.Id)))
                _users.Add(new UserInfo(profile.Id, Guid.NewGuid(), null));

            var user = _users.FirstOrDefault(f => f.Id.Equals(profile.Id));
            return new LoginDto
            {
                Username = profile.Username,
                Token = user?.Token.ToString()
            };
        }

        public static void Validate(HttpRequest request)
        {
            var header = request.Headers.FirstOrDefault(f => f.Key.ToLower().Equals("authorization"));

            if (!header.Value.ToList().Any())
                throw new UnauthorizedException("Without authorization");

            var tokenRequest = header.Value.ToArray()[0];
            if (string.IsNullOrEmpty(tokenRequest) || !Guid.TryParse(tokenRequest, out var token) || token == Guid.Empty)
                throw new UnauthorizedException("Without authorization");

            GetUser(Guid.Parse(tokenRequest));
        }
    }

    public class UserInfo
    {
        public UserInfo(Guid id, Guid newGuid, Guid? token)
        {
            Id = id;
            if (token == null) Token = newGuid;
            LastConnection = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid Token { get; set; }
        public DateTime LastConnection { get; set; }
    }
}