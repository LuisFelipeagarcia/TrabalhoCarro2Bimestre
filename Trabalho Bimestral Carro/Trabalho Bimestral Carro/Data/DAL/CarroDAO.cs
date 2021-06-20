using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trabalho_Bimestral_Carro.Data.DAL
{
    public class CarroDAO
    {
        private readonly IESContext _context;

        #region Construtor
        public CarroDAO(IESContext context)
        {
            _context = context;
        }
        #endregion

        #region ObterCarros
        public async Task<List<Carro>> ObterCarros()
        {
            return await _context.Carros.OrderBy(c => c.CarroID).ToListAsync();
        }
        #endregion

        #region ObterCarroPorId
        public async Task<Carro> ObterCarroPorId(long id)
        {
            return await _context.Carros.FindAsync(id);
        }
        #endregion

        #region GravarCarro
        public async Task<Carro> GravarCarro(Carro carro)
        {
            if (carro.CarroID == null)
            {
                _context.Carros.Add(carro);
            }
            else
            {
                _context.Update(carro);
            }
            await _context.SaveChangesAsync();

            return carro;
        }
        #endregion

        #region EliminarCarroPorId
        public async Task<Carro> EliminarCarroPorId(long id)
        {
            Carro carro = await ObterCarroPorId(id);
            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();
            return carro;
        }
        #endregion
    }
}
