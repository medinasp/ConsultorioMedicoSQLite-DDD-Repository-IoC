using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedico.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProntuariosController : ControllerBase
    {
        private readonly IProntuariosService _prontuariosService;

        public ProntuariosController(IProntuariosService prontuariosService)
        {
            _prontuariosService = prontuariosService;
        }

        /// <summary>
        /// Obter todos cadastros ativos de prontuários
        /// </summary>
        /// <returns>Prontuários Cadastrados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var prontuariosViewModel = await _prontuariosService.GetAll();
            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Criar um novo prontuário
        /// </summary>
        /// <remarks>
        /// {"MedicoId": "string", "MedicoNome": "string", "MedicoEspecialidade": "string", "PacienteId": "string", "PacienteNome": "string", "TextoProntuario": "string"}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Requisição Inválida</response>
        [HttpPost("criar-prontuario-por-id")]
        public async Task<IActionResult> CriarProntuarioId([FromBody] ProntuariosInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var prontuarioViewModel = await _prontuariosService.CriarProntuarioPorId(model);
                return Ok(prontuarioViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar o prontuário: " + ex.Message);
            }
        }

        /// <summary>
        /// Obter um cadastro específico pelo nome do Médico
        /// </summary>
        /// <param name="nome">Identificador do cadastro de um médico específico pelo nome</param>
        /// <returns>Dados do cadastro específico do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("nomeMedico/{nome}", Name = "ConsultarProntuarioPorNomeMedico")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarProntuarioPorNomeMedico(string nome)
        {
            var prontuariosViewModel = await _prontuariosService.ConsultarProntuarioPorNomeMedico(nome);

            if (prontuariosViewModel == null) return NotFound();

            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Obter um cadastro específico pelo nome do Paciente
        /// </summary>
        /// <param name="nome">Identificador do cadastro de um paciente específico pelo nome</param>
        /// <returns>Dados do cadastro específico do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("nomePaciente/{nome}", Name = "ConsultarProntuarioPorNomePaciente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConsultarProntuarioPorNomePaciente(string nome)
        {
            var prontuariosViewModel = await _prontuariosService.ConsultarProntuarioPorNomePaciente(nome);

            if (prontuariosViewModel == null) return NotFound();

            return Ok(prontuariosViewModel);
        }

        /// <summary>
        /// Atualizar cadastro de um prontuário específico
        /// </summary>
        /// <remarks>
        /// {"TextoProntuario": "string"}
        /// </remarks>
        /// <param name="id">Identificador do prontuário de um médico específico</param>
        /// <param name="model">Dados do cadastro específico do prontuário</param>
        /// <returns>Ok</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditarProntuario(string id, ProntuariosInputModel model)
        {
            var updated = await _prontuariosService.EditarProntuario(id, model);

            if (!updated) return NotFound();

            return Ok();
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
            if (!success) return NotFound();
            return Ok("SoftDelete Ok");
        }

        /// <summary>
        /// Hard Delete - Remove registro definitivamente
        /// </summary>
        /// <param name="id">Identificador do cadastro de um prontuário específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("hard/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> HardDelete(string id)
        {
            var success = await _prontuariosService.RemoverProntuarioHard(id);
            if (!success) return NotFound();
            return Ok("HardDelete Ok");
        }
    }
}
