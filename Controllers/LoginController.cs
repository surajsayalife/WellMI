using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WellMI.Data;
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
        public readonly UserContext _usercontext;

        public LoginController ( UserContext userContext )
        {
            _usercontext = userContext;
        }




    }
    
    
}
