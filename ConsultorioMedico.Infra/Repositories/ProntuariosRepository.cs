using Microsoft.EntityFrameworkCore;
using ConsultorioMedico.Dominio.Entities;
using ConsultorioMedico.Infra.Configuration;

namespace ConsultorioMedico.Infra.Repositories
{
    public class ProntuariosRepository : IProntuariosRepository
    {
        private readonly ContextBase _context;

        public ProntuariosRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<Prontuarios> GetById(string code)
        {
            if (!Guid.TryParse(code, out var guid))
                return null;

            var prontuarios = await _context.Prontuarios.FindAsync(guid);

            return prontuarios;
        }

        public async Task<IEnumerable<Prontuarios>> GetAllActives()
        {
            return await _context.Prontuarios.Where(c => c.Ativo).ToListAsync();
        }

        public async Task<List<Prontuarios>> Add(Prontuarios prontuarios)
        {
            var registroExistente = _context.Prontuarios.Any(p => p.MedicoId == prontuarios.MedicoId && p.PacienteId == prontuarios.PacienteId);
            if (registroExistente)
            {
                throw new ArgumentException("Essa combinação de Médico e Paciente já possui um prontuário.");
            }

            // Buscar informações do Médico
            var medico = await _context.CadMedicos.FindAsync(prontuarios.MedicoId);

            if (medico == null)
            {
                throw new ArgumentException("Médico não encontrado.");
            }

            prontuarios.MedicoNome = medico.Nome;
            prontuarios.MedicoEspecialidade = medico.Especialidade;

            // Buscar informações do Paciente
            var paciente = await _context.CadPacientes.FindAsync(prontuarios.PacienteId);

            if (paciente == null)
            {
                throw new ArgumentException("Paciente não encontrado.");
            }

            prontuarios.PacienteNome = paciente.Nome;

            await _context.Prontuarios.AddAsync(prontuarios);
            await _context.SaveChangesAsync();

            var cadProntuarios = _context.Prontuarios.Where(c => c.MedicoId == prontuarios.MedicoId && c.PacienteId == prontuarios.PacienteId).ToList();
            return cadProntuarios;
        }


        public async Task Update(Prontuarios prontuarios)
        {
            _context.Prontuarios.Update(prontuarios);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SoftDelete(Prontuarios prontuarios)
        {
            if (prontuarios == null)
                throw new ArgumentNullException(nameof(prontuarios));

            prontuarios.Excluir();

            _context.Prontuarios.Update(prontuarios);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task HardDelete(Prontuarios prontuarios)
        {
            _context.Prontuarios.Remove(prontuarios);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Prontuarios>> GetByNomeMedico(string name)
        {
            var prontuario = await _context.Prontuarios.Where(c => EF.Functions.Like(c.MedicoNome, $"%{name}%")).ToListAsync();
            return prontuario;
        }

        public async Task<List<Prontuarios>> GetByNomePaciente(string name)
        {
            var prontuarios = await _context.Prontuarios.Where(c => EF.Functions.Like(c.PacienteNome, $"%{name}%")).ToListAsync();
            return prontuarios;
        }
    }
}
