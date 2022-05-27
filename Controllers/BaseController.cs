using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderForMeProject.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderForMeProject.Controllers
{
    [Authorize(Roles = AuthConst.Administrator)]
    public class BaseController: Controller
    {
        public BaseController()
        {

        }
    }
}
