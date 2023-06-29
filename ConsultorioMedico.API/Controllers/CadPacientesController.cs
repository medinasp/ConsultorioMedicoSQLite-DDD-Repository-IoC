using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadPacientesController : ControllerBase
    {
        private readonly ICadPacientesService _cadPacientesServices;

        public CadPacientesController(ICadPacientesService cadPacientesServices)
        {
            _cadPacientesServices = cadPacientesServices;
        }

        /// <summary>
        /// Cadastrar um novo paciente
        /// </summary>
        /// <remarks>
        /// {"nome": "string", "cpf": "string"}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Requisição Inválida</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CadPacientesInputModel model)
        {
            if (ModelState.IsValid)
            {
                var id = await _cadPacientesServices.Add(model);
                return CreatedAtRoute("GetPacienteById", new { id }, id);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Obter um cadastro específico pelo id
        /// </summary>
        /// <param name="id">Identificador do cadastro de um paciente específico</param>
        /// <returns>Dados do cadastro específico do paciente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}", Name = "GetPacienteById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var cadPacientesViewModel = await _cadPacientesServices.GetByCode(id);

            if (cadPacientesViewModel == null)
                return NotFound();

            return Ok(cadPacientesViewModel);
        }

        /// <summary>
        /// Obter todos cadastros de pacientes em memória
        /// </summary>
        /// <returns>Pacientes Cadastrados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cadPacientesViewModels = await _cadPacientesServices.GetAll();
            return Ok(cadPacientesViewModels);
        }

        /// <summary>
        /// Atualizar cadastro de um paciente específico
        /// </summary>
        /// <remarks>
        /// {"nome": "string", "cpf": "string"}
        /// </remarks>
        /// <param name="id">Identificador do cadastro de um paciente específico</param>
        /// <param name="model">Dados do cadastro específico do paciente</param>
        /// <returns>Ok</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, CadPacientesInputModel model)
        {
            var updated = await _cadPacientesServices.Update(id, model);

            if (updated)
                return Ok();

            return NotFound();
        }

        /// <summary>
        /// Obter somente cadastros de pacientes com status "ativo"
        /// </summary>
        /// <returns>Pacientes Cadastrados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet("actives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActives()
        {
            var activeCadPacientes = await _cadPacientesServices.GetActives();
            return Ok(activeCadPacientes);
        }

        /// <summary>
        /// Soft Delete - Desativa registro
        /// </summary>
        /// <param name="id">Identificador do cadastro de um paciente específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("soft/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var success = await _cadPacientesServices.SoftDelete(id);

            if (!success)
                return NotFound();

            return Ok("SoftDelete Ok");
        }

        /// <summary>
        /// Hard Delete - Remove um cadastro do banco em memória
        /// </summary>
        /// <param name="id">Identificador do cadastro de um paciente específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response> 
        [HttpDelete("hard/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HardDelete(string id)
        {
            var result = await _cadPacientesServices.HardDelete(id);
            if (result)
                return Ok("HardDelete Ok");

            return NotFound();
        }


    }
}
