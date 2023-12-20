using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WellMI.Auth;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

using WellMI.Models;
using WellMI.CommonUtility;
//using Microsoft.EntityFrameworkCore;

namespace WellMI.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public readonly UserContext _UserContext;

        public LoginController ( UserContext userContext )
        {
            _UserContext = userContext;
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
        //public IActionResult GetData ()
        //{
        //    if ( _usercontext.user == null )
        //    {
        //        return NotFound ();
        //    }
        //    return (IActionResult) _usercontext.user.ToList ();
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetDetail ()
        //{
        //    var userList = await _usercontext.user.ToListAsync ();
        //    return Ok ( await _usercontext.user.ToListAsync () );
        //}

        [HttpPost]
        public async Task<IActionResult> CreateNewEmployer ( User user )
        {

            var User = new User ()
            {              
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailId = user.EmailId,
                UserType = user.UserType,
                IsVerify = false,
                IsDeleted = false,
                IsActive = false,
                Password = user.Password,
                CreateDate = DateTime.Now,
                ModifyDate = null,

            };
             await _UserContext.User.AddAsync ( User );
            if (user.UserType == 2 )
            {
                
                string url = Environment.GetEnvironmentVariable ( "RegistrationOfEmployerUrl" ) ?? string.Empty;

                string confirmationLink = url + user.FirstName+ ' '+user.LastName;

                string name = user.FirstName + " " + user.LastName;
                string subject = "Registration of Employer";
                string body = " Link for Registration of Employer" + confirmationLink;

                EmailService emailService = new EmailService (  );
                emailService.SendEmail ( user.EmailId, subject, body );             
            }
           await _UserContext.SaveChangesAsync ();
            return Ok ( User );

        }

        [HttpPut]
        [Route ( "{Id:long}" )]
        public async Task<IActionResult> UpdateData ( [FromRoute] int Id, Employer employer ,string Password )
        {
            var Data = _UserContext.User.Find ( Id );
            if ( Data != null )
            {
                Data.Password = Password;
                Data.IsVerify = true;
                Data.IsActive = true;

                await _UserContext.SaveChangesAsync ();

                if (Password != null )
                {
                    var emp = new Employer ();

                    emp.UserId = Id;
                    emp.CompanyName = employer.CompanyName;
                    emp.UserRegistrationInviteCode = Convert.ToString ( Guid.NewGuid () );
                    emp.EmployerRegistrationInviteCode = Convert.ToString ( Guid.NewGuid () );
                    emp.CreateDate = DateTime.Now;
                    emp.ModifyDate = null;

                    await _UserContext.Employer.AddAsync ( emp );
                    await _UserContext.SaveChangesAsync ();

                }
                return Ok ( Data );
            }
            return NotFound ();
        }
        //[HttpPut]
        //[Route ( "{Id:long}" )]
        //public async Task<IActionResult> DeleteData ( [FromRoute] long Id )
        //{
        //    var Data = _usercontext.user.Find ( Id );
        //    if ( Data != null )
        //    {
        //        Data.IsActive = false;
        //        Data.IsActive = true;

        //        await _usercontext.SaveChangesAsync ();

        //        return Ok ( Data );
        //    }
        //    return NotFound ();
        //}
    }


}
