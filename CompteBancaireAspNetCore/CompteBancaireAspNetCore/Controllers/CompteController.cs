using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CompteBancaireAspNetCore.Controllers
{
    public class CompteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Deposite()
        {
            return View();
        }
        public IActionResult WithDrawal()
        {
            return View();
        }

        public IActionResult GetOperations()
        {
            return View();
        }
    }
}