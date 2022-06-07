using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpecialFood.Models;
using SpecialFood.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SpecialFood.util;

namespace SpecialFood.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        public CatalogoController(ApplicationDbContext context,
            ILogger<CatalogoController> logger,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Catalogo(string? searchString)
        {
            var productos = from o in _context.DataProductos select o;
            if(!String.IsNullOrEmpty(searchString)){
                productos = productos.Where(s => s.Name.Contains(searchString));
            }
            return View(await productos.ToListAsync());
        }

         public async Task<IActionResult> Details(int? id){
            Producto objProduct = await _context.DataProductos.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }

            return View(objProduct);
        }

        public async Task<IActionResult> Agregar(int? id){

            var userID = _userManager.GetUserName(User);

            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return  View("Catalogo",productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);

                util.SessionExtensions.Set<Producto>(HttpContext.Session,"Producto", producto);

                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Precio = producto.Precio;
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                ViewData["Message"] = $"Se agrego {proforma.Producto.Descripcion}";
                return RedirectToAction("Catalogo");
            }

        }
    }
}