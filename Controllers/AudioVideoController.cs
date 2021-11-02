using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioTel.Controllers
{
    public class AudioVideoController : Controller
    {
        public IActionResult AudioVideo()
        {
            return View();
        }
    }
}
