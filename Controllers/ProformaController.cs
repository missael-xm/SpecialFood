using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpecialFood.Models;
using SpecialFood.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using SpecialFood.util;

namespace SpecialFood.Controllers
{    
    public class ProformaController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; 

        public ProformaController(ApplicationDbContext context,
            ILogger<CatalogoController> logger,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(){
            var producto  = util.SessionExtensions.Get<Producto>(HttpContext.Session,"Producto"); 

            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProforma select o;
            items = items.
                Include(p => p.Producto).
                Where(w => w.UserID.Equals(userID) && w.Status.Equals("PENDIENTE"));

            var carrito = await items.ToListAsync();
            var total = carrito.Sum(c => c.Cantidad * c.Precio);

            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.elementosCarrito = carrito;

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id){
            Proforma proformaModel = await _context.DataProforma.FindAsync(id);

            if(proformaModel == null){
                return NotFound();
            }
            _context.DataProforma.Remove(proformaModel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id){
            Proforma proformaModel = await _context.DataProforma.FindAsync(id);

            if(proformaModel == null){
                return NotFound();
            }

            return View(proformaModel);
        }

        public async Task<IActionResult> Update(int? id){
            Proforma proformaModel = await _context.DataProforma.FindAsync(id);

            if(proformaModel == null){
                return NotFound();
            }
            //falta revisar el tema del editar
            proformaModel.Cantidad = 5;
            _context.DataProforma.Update(proformaModel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit",new {id =proformaModel.Id} );
        }
    }
}