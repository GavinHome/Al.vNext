//-----------------------------------------------------------------------------------
// <copyright file="TokenProviderMiddleware.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Al.vNext.Core.Const;
using Al.vNext.Core.Utility;
using Al.vNext.Services.Contracts;
using Al.vNext.Web.Common.TokenProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Al.vNext.Web.Common.Middleware
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly JsonSerializerSettings _serializerSettings;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options)
        {
            GuardUtils.NotNull(options, nameof(options));
            _next = next;

            _options = options.Value;
            ThrowIfInvalidOptions(_options);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context, IAccountService accountService)
        {
            GuardUtils.NotNull(context, nameof(context));
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context, accountService);
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsync("Bad request.");
        }

        private async Task GenerateToken(HttpContext context, IAccountService accountService)
        {
            var auth_success = false;
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var authentication = context.Request.Form["authentication"];
            if (!string.IsNullOrEmpty(authentication))
            {
                var dynamic = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(SafeDecoded(authentication.ToString()));
                SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
                sArray.Add("UserName", dynamic.UserName.ToString());
                sArray.Add("TimeStamp", dynamic.TimeStamp.ToString());
                sArray.Add("Sign", dynamic.Sign.ToString());

                if (Al.vNext.Core.SecuritySign.VerifyWithTimeStamp(sArray, sArray["Sign"], Convert.ToInt64(sArray["TimeStamp"])))
                {
                    auth_success = true;
                    username = dynamic.UserName.ToString();
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid authentication.");
                }
            }

            if (!auth_success)
            {
                var identity = await _options.IdentityResolver(username, password, accountService);
                if (identity == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid username or password.");
                    return;
                }
            }

            var userInfo = await accountService.Get(username);

            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, await _options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtClaimNamesConst.Org, userInfo.OrganizationId.ToString(), ClaimValueTypes.String),
                new Claim(JwtClaimNamesConst.UseName, userInfo.Name, ClaimValueTypes.String),
                new Claim(JwtClaimNamesConst.Func, string.Join(",", userInfo.Functions.Select(x => x.ToString()).ToArray()))
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds,
                user_name = userInfo.Name,
                user_func = string.Join(",", userInfo.Functions.Select(x => x.ToString()).ToArray())
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _serializerSettings));
        }

        private void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Path))
            {
                throw new ArgumentNullException($"{nameof(TokenProviderOptions.Path)}");
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException($"{nameof(TokenProviderOptions.Issuer)}");
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException($"{nameof(TokenProviderOptions.Audience)}");
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", $"{nameof(TokenProviderOptions.Expiration)}");
            }

            if (options.IdentityResolver == null)
            {
                throw new ArgumentNullException($"{nameof(TokenProviderOptions.IdentityResolver)}");
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException($"{nameof(TokenProviderOptions.SigningCredentials)}");
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException($"{nameof(TokenProviderOptions.NonceGenerator)}");
            }
        }

        private string SafeDecoded(string safeBase64)
        {
            string base64EncodedText = System.Web.HttpUtility.UrlDecode(safeBase64).Replace('-', '+').Replace('_', '/');
            base64EncodedText = base64EncodedText.PadRight(base64EncodedText.Length + ((4 - (base64EncodedText.Length % 4)) % 4), '=');
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedText));
        }
    }
}