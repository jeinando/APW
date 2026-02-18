using APW.Architecture;
using APW.Architecture.Providers;
using APW.Models;
using APW.Web.Models;
using APW.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace APW.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IWrapperServiceProvider _serviceProvider;

        public ProductController(ILogger<ProductController> logger, IWrapperServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _serviceProvider.GetDataAsync<ComplexObject>("http://localhost:5033/ProductApi") as ComplexObject;
            var content = JsonProvider.Serialize(data.Entities);
            var result = JsonProvider.DeserializeSimple<IEnumerable<ProductViewModel>>(content);
            return View(result);
        }
    }
}