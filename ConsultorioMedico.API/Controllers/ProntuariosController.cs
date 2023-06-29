using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProntuariosController : ControllerBase
    {
        private readonly IProntuariosService _prontuariosService;

        public ProntuariosController(IProntuariosService prontuariosService)
        {
            _prontuariosService = prontuariosService;
        }

        /// <summary>
        /// Cadastrar um novo prontuario buscando pelo id do médico e do paciente
        /// </summary>
        /// <remarks>
        /// {"Medico": "string", "Paciente": "string", "TextoProntuarios": "string"}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Requisição Inválida</response>
        [HttpPost("create-prontuario-by-id")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProntuarioById(ProntuariosInputModel model)
        {
            var prontuarioViewModel = await _prontuariosService.CriarProntuarioPorId(model);

            if (prontuarioViewModel == null)
            {
                return BadRequest("Paciente ou médico não encontrado.");
            }

            return Ok(prontuarioViewModel);
        }

        /// <summary>
        /// Cadastrar um novo prontuario buscando pelo nome do médico e do paciente
        /// </summary>
        /// <remarks>
        /// {"Medico": "string", "Paciente": "string", "TextoProntuarios": "string"}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Requisição Inválida</response>
        [HttpPost("create-prontuario-by-name")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProntuarioByName(ProntuariosInputModel model)
        {
            var prontuarioViewModel = await _prontuariosService.CriarProntuarioPorNome(model);

            if (prontuarioViewModel == null)
            {
                return BadRequest("Paciente ou médico não encontrado.");
            }

            return Ok(prontuarioViewModel);
        }

        /// <summary>
        /// Obter um prontuário específico pelo nome do médico
        /// </summary>
        /// <param name="name">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Dados de um prontuário específico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("get-by-name-medico")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameMedico(string name)
        {
            var prontuariosViewModel = await _prontuariosService.ConsultarProntuarioPorNomeMedico(name);

            if (prontuariosViewModel == null)
            {
                return BadRequest("Prontuário não encontrado.");
            }

            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Obter um prontuário específico pelo nome do paciente
        /// </summary>
        /// <param name="name">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Dados de um prontuário específico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("get-by-name-paciente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNamePaciente(string name)
        {
            var prontuariosViewModel = await _prontuariosService.ConsultarProntuarioPorNomePaciente(name);

            if (prontuariosViewModel == null)
            {
                return BadRequest("Prontuário não encontrado.");
            }

            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Obter um prontuário ativo pelo nome do médico
        /// </summary>
        /// <param name="name">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Dados de um prontuário específico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("get-by-name-medico-ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameMedicoActives(string name)
        {
            var prontuariosViewModel = await _prontuariosService.ConsultarProntuarioPorNomeMedicoAtivos(name);

            if (prontuariosViewModel == null)
            {
                return BadRequest("Prontuário não encontrado.");
            }

            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Obter um prontuário ativo pelo nome do paciente
        /// </summary>
        /// <param name="name">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Dados de um prontuário específico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("get-by-name-paciente-ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNamePacienteActives(string name)
        {
            var prontuariosViewModel = await _prontuariosService.ConsultarProntuarioPorNomePacienteAtivos(name);

            if (prontuariosViewModel == null)
            {
                return BadRequest("Prontuário não encontrado.");
            }

            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Atualizar um prontuário específico
        /// </summary>
        /// <remarks>
        /// {"Medico": "string", "Paciente": "string", "TextoProntuarios": "string"}
        /// </remarks>
        /// <param name="id">Identificador do cadastro de um prontuário específico</param>
        /// <param name="model">Dados do cadastro específico do prontuário</param>
        /// <returns>Ok</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, ProntuariosInputModel model)
        {
            var updated = await _prontuariosService.EditarProntuario(id, model);

            if (updated)
                return Ok();

            return NotFound();
        }

        /// <summary>
        /// Soft Delete - Desativa registro
        /// </summary>
        /// <param name="id">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("soft/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var success = await _prontuariosService.RemoverProntuarioSoft(id);
            if (!success)
                return NotFound();

            return Ok("SoftDelete Ok");
        }

        /// <summary>
        /// Hard Delete - Remove um cadastro do banco em memória
        /// </summary>
        /// <param name="id">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("hard/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HardDelete(string id)
        {
            var result = await _prontuariosService.RemoverProntuarioHard(id);
            if (result)
                return Ok("HardDelete Ok");

            return NotFound();
        }
    }
}
