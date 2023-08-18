using Library.Model.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Web.HelperMethods
{
	public static class TokenHelper
	{
		public static string TokenGeneration(string parameter, IConfiguration configuration)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, parameter),
			};
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTConfig:Key").Value));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
			var Token = new JwtSecurityToken(
				 claims: claims,
				 expires: DateTime.Now.AddMinutes(10),
				 signingCredentials: credentials
			 );
			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
		public static string TokenGeneration(User user, IConfiguration configuration)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.NameIdentifier,user.id.ToString())
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken
				(
					claims: claims,
					expires: DateTime.Now.AddMinutes(10),
					signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		public static int TokenDecryption(string token)
		{
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);
			var jwtClaims = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

			return jwtClaims != null ? int.Parse(jwtClaims.Value) : -1;
		}
	}
}
