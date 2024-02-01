using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.ATR.Portal.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoomList()
        {
            return View();
        }

        public IActionResult RoomDetails()
        {
            return View();
        }
    }
}
