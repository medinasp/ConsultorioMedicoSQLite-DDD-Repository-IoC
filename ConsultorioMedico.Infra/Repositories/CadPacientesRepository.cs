using ConsultorioMedico.Dominio.Entities;
using ConsultorioMedico.Infra.Configuration;
using ConsultorioMedico.Infra.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedico.Infra.Repositories
{
    public class CadPacientesRepository : ICadPacientesRepository
    {
        private readonly ContextBase _context;

        public CadPacientesRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task Add(CadPacientes cadPaciente)
        {
            await _context.CadPacientes.AddAsync(cadPaciente);
            await _context.SaveChangesAsync();
        }

        public async Task<CadPacientes> GetByCode(string code)
        {
            if (!Guid.TryParse(code, out var guid))
                return null;

            var cadCliente = await _context.CadPacientes.FindAsync(guid);

            return cadCliente;
        }

        public async Task<IEnumerable<CadPacientes>> GetAll()
        {
            return await _context.CadPacientes.ToListAsync();
        }

        public async Task Update(CadPacientes cadPaciente)
        {
            _context.CadPacientes.Update(cadPaciente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CadPacientes>> GetActives()
        {
            return await _context.CadPacientes.Where(c => c.Ativo).ToListAsync();
        }

        public async Task<bool> SoftDelete(CadPacientes cadPaciente)
        {
            if (cadPaciente == null)
                throw new ArgumentNullException(nameof(cadPaciente));

            cadPaciente.Excluir();

            _context.CadPacientes.Update(cadPaciente);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task HardDelete(CadPacientes cadPaciente)
        {
            _context.CadPacientes.Remove(cadPaciente);
            await _context.SaveChangesAsync();
        }
    }
}

