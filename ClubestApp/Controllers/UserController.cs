﻿namespace ClubestApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}