using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WellMI.Data;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

using WellMI.Models;
//using Microsoft.EntityFrameworkCore;

namespace WellMI.Controllers
{
        [Route ( "api/[controller]" )]
        [ApiController]
        public class LoginController : ControllerBase
        {
            //private IConfiguration _config;
            public readonly UserContext _usercontext; 

             public LoginController( UserContext userContext )
        {
                _usercontext = userContext;
        }
        //public LoginController ( IConfiguration config )
        //{
        //    _config = config;
        //}

        //[HttpPost]
        //public IActionResult GenrateToken ( [FromBody] User loginRequest )
        //{
        //    //your logic for login process
        //    //If login usrename and password are correct then proceed to generate token

        //    var securityKey = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( _config ["Jwt:Key"] ) );
        //    var credentials = new SigningCredentials ( securityKey, SecurityAlgorithms.HmacSha256 );

        //    var Sectoken = new JwtSecurityToken ( _config ["Jwt:Issuer"],
        //      _config ["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.Now.AddMinutes ( 120 ),
        //      signingCredentials: credentials );

        //    var token = new JwtSecurityTokenHandler ().WriteToken ( Sectoken );

        //    return Ok ( token );
        //}
        //[HttpGet]
        //public IActionResult GetData (  )
        //{
        //    if(_usercontext.Users == null )
        //    {
        //        return NotFound ();
        //    }
        //    return _usercontext.Users.ToList ();
        //}
        [HttpGet]
        public async Task<IActionResult> GetDetail ()
        {
            //var userList = await _usercontext.user.ToListAsync ();
            return Ok ( await _usercontext.user.ToListAsync() );
        }

        [HttpPost]
            public async Task<IActionResult> Registration(User user )
            {

            var User = new User ()
            {
                Id = user.Id,
                Username = user.Username,
                EmailId = user.EmailId,
                AccountType = user.AccountType,
                Password = user.Password
              

            };
              await _usercontext.user.AddAsync ( User );
               await _usercontext.SaveChangesAsync();

               return Ok ( User );

            }

        [HttpPut]
        [Route ("{Id:long}")]
        public async Task<IActionResult> UpdateData ( [FromRoute] long Id , UpdateData data )
        {
           var Data = _usercontext.user.Find ( Id );
            if( Data  != null )
            {
                Data.Username = data.Username;
                Data.EmailId = data.EmailId;
                Data.Password = data.Password;
                Data.AccountType = data.AccountType;

                await _usercontext.SaveChangesAsync();

                return Ok ( Data );
            }
            return NotFound ();
        }
    }
    
}
