using Models.Common;
using Models.DatabaseModels;
using Models.Dtos;
using Models.Enums;
using Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Common.Utils;
using Models.Constant;
using Database.Users.Interfaces;

namespace Services.UserServices
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly string AesConstantKey = AesConstant.AESKEY;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly string AesConstantIV = AesConstant.AESIV;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly AppSettings _appSettings;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly IUserDbContext _userDbContext;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="appSettings"></param>
        public UserService(
            IOptions<AppSettings> appSettings,
            IUserDbContext userDbContext
        )
        {
            _appSettings = appSettings.Value;
            _userDbContext = userDbContext;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AuthenticateDto Authenticate(AuthenticateRequest model)
        {
            var username = CryptographyUtil.DecryptStringAES(AesConstantKey, AesConstantIV, model.Username).Replace("\"", "");
            var password = CryptographyUtil.DecryptStringAES(AesConstantKey, AesConstantIV, model.Password).Replace("\"", "");
            var user = _userDbContext.Login(username, password);
            if (user == null) return null;
            var token = generateJwtToken(user);
            return new AuthenticateDto(user, token, AuthenticateEnum.Login);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public AuthenticateDto Authenticate(string payload)
        {
            var decrypPayload = CryptographyUtil.DecryptStringAES(AesConstantKey, AesConstantIV, payload);
            var payloadObj = JsonConvert.DeserializeObject<AuthenticateRequest>(decrypPayload);
            var user = _userDbContext.Login(payloadObj?.Username, payloadObj?.Password);
            if (user == null) 
                return null;
            var token = generateJwtToken(user);
            var usr = new AuthenticateDto(user, token, AuthenticateEnum.Login);

            return usr;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticateDto Authenticate(string username, string password)
        {
            var user = _userDbContext.Login(username, password);
            if (user == null) return null;
            var token = generateJwtToken(user);
            var usr = new AuthenticateDto(user, token, AuthenticateEnum.Login);
            return usr;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            return new List<User>();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Logout(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
