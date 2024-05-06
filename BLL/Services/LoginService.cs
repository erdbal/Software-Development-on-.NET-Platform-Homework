using System;
using System.Security.Cryptography;
using System.Text;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using System.Security.Cryptography;
using Szoftverfejlesztés_dotnet_hw.DAL;
using Microsoft.EntityFrameworkCore;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class LoginService : ILoginService
    {

        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;

        private static object syncobject = new object();

        public LoginService(AppDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;

        }

        private static Dictionary<string,Login> nameLoginPairs = new Dictionary<string, Login>();


        public byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }


        public int GetIdforToken(string token)
        {
            lock (syncobject)
            {
                foreach (var item in nameLoginPairs)
                {
                    if (item.Value.Token == token)
                    {
                        return item.Value.UserId;
                    }
                }
            }
            return -1;
        }

        public bool IsTokenValid(string token)
        {
            lock (syncobject)
            {
                foreach (var item in nameLoginPairs)
                {
                    if (item.Value.Token == token)
                    {
                        if (item.Value.Expiration > DateTime.Now)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public string RefreshOrLogin(User user, string password)
        {
            lock (syncobject)
            {
                var givenPassword = GetHashString(password);

                var storeduser = _dbcontext.Users.SingleOrDefault(u => u.Id == user.Id)
                    ??throw new EntityByIdNotFoundException("No user by id found",user.Id);

                var storedPassword = GetHashString(storeduser.Password);
                if (storedPassword != givenPassword)
                {
                    throw new UnauthorizedException("Invalid password");
                }

                if (nameLoginPairs.TryGetValue(user.Username, out var login))
                {
                    login.Expiration = DateTime.Now.AddHours(1);
                    
                    return System.Text.Json.JsonSerializer.Serialize(nameLoginPairs[user.Username].Token);
                }

                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");

                var newLogin = new Login
                {
                    UserId = user.Id,
                    Token = GuidString,
                    Expiration = DateTime.Now.AddHours(1)
                };

                nameLoginPairs.Add(user.Username, newLogin);
                return System.Text.Json.JsonSerializer.Serialize(nameLoginPairs[user.Username].Token);
            }

            

        }

        



    }

    public class Login
    {
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }

    }
}
