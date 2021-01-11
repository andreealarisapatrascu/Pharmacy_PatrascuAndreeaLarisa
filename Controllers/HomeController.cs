﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmacyModel.Models;
using Microsoft.EntityFrameworkCore;
using PharmacyModel.Data;
using Pharmacy_PatrascuAndreeaLarisa.Models;
using Pharmacy_PatrascuAndreeaLarisa.Models.PharmacyViewModels;

namespace Pharmacy_PatrascuAndreeaLarisa.Controllers
{
    public class HomeController : Controller
    {
        private readonly PharmacyContext _context;
        public HomeController(PharmacyContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from order in _context.Produse
            group order by order.DataExpirare into dateGroup
            select new OrderGroup()
            {
                ExpirationDate = dateGroup.Key,
                NumarProduse = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Chat()
        {
            return View();
        }
    }
}
