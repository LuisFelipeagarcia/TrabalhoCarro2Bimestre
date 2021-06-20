using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trabalho_Bimestral_Carro.Data;
using Trabalho_Bimestral_Carro.Data.DAL;

namespace Trabalho_Bimestral_Carro.Areas.Cadastros.Controllers
{
    [Area(nameof(Cadastros))]
    [Authorize]
    public class CarroController : Controller
    {
        private readonly IESContext _context;
        private readonly CarroDAO _carroDAO;

        #region Construtor
        public CarroController(IESContext context)
        {
            _context = context;
            _carroDAO = new CarroDAO(context);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _carroDAO.ObterCarros()); ;
        }
        #endregion

        #region Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cor, Marca, Fabricante")] Carro carro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _carroDAO.GravarCarro(carro);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "Falha ao inserir");
            }
            return View(carro);
        }
        #endregion

        #region Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoCArroPorId(id);
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("CarroID, Cor, Marca, Fabricante")]
        Carro carro)
        {
            if (id != carro.CarroID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _carroDAO.GravarCarro(carro);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(ex.Message, "Falha ao atualizar");
                }
            }


            return View(carro);
        }
        #endregion

        #region Details - GET
        public async Task<IActionResult> Details(long? id)
        {

            return await ObterVisaoCArroPorId(id);
        }
        #endregion

        #region Delete - GET
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoCArroPorId(id);
        }
        #endregion

        #region Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {

            var carro = await _carroDAO.EliminarCarroPorId((long)id);

            TempData["Message"] = "Carro " + carro.CarroID + " foi removido!";


            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ObterVisaoCarroPorId
        private async Task<IActionResult> ObterVisaoCArroPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _carroDAO.ObterCarroPorId((long)id);

            if (carro == null)
            {
                return NotFound();
            }
            return View(carro);
        }
        #endregion

    }
}
