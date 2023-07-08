using ConsultorioMedico.Dominio.Entities;
using ConsultorioMedico.Infra.Configuration;
using ConsultorioMedico.Infra.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedico.Infra.Repositories
{
    public class CadMedicosRepository : ICadMedicosRepository
    {
        private readonly ContextBase _context;

        public CadMedicosRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task Add(CadMedicos cadMedico)
        {
            await _context.CadMedicos.AddAsync(cadMedico);
            await _context.SaveChangesAsync();
        }

        public async Task<CadMedicos> GetByCode(string code)
        {
            if (!Guid.TryParse(code, out var guid))
                return null;

            var cadMedico = await _context.CadMedicos.FindAsync(guid);

            return cadMedico;
        }

        public async Task<List<CadMedicos>> GetByName(string name)
        {
            var cadMedico = await _context.CadMedicos.Where(c => EF.Functions.Like(c.Nome, $"%{name}%")).ToListAsync();

            return cadMedico;
        }

        public async Task<IEnumerable<CadMedicos>> GetAll()
        {
            return await _context.CadMedicos.ToListAsync();
        }

        public async Task Update(CadMedicos cadMedico)
        {
            _context.CadMedicos.Update(cadMedico);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CadMedicos>> GetActives()
        {
            return await _context.CadMedicos.Where(c => c.Ativo).ToListAsync();
        }

        public async Task<bool> SoftDelete(CadMedicos cadMedico)
        {
            if (cadMedico == null)
                throw new ArgumentNullException(nameof(cadMedico));

            cadMedico.Excluir();

            _context.CadMedicos.Update(cadMedico);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task HardDelete(CadMedicos cadMedico)
        {
            _context.CadMedicos.Remove(cadMedico);
            await _context.SaveChangesAsync();
        }
    }
}
