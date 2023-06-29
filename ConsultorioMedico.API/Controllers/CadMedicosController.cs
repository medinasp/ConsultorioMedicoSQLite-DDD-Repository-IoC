using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadMedicosController : ControllerBase
    {
        private readonly ICadMedicosService _cadMedicosServices;

        public CadMedicosController(ICadMedicosService cadMedicosServices)
        {
            _cadMedicosServices = cadMedicosServices;
        }

        /// <summary>
        /// Cadastrar um novo médico
        /// </summary>
        /// <remarks>
        /// {"nome": "string", "cpf": "string", "especialidade": "string"}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Requisição Inválida</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CadMedicosInputModel model)
        {
            if (ModelState.IsValid)
            {
                var id = await _cadMedicosServices.Add(model);
                return CreatedAtRoute("GetMedicoById", new { id }, id);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Obter um cadastro específico pelo id
        /// </summary>
        /// <param name="id">Identificador do cadastro de um médico específico</param>
        /// <returns>Dados do cadastro específico do médico</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}", Name = "GetMedicoById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var cadMedicosViewModel = await _cadMedicosServices.GetByCode(id);

            if (cadMedicosViewModel == null)
                return NotFound();

            return Ok(cadMedicosViewModel);
        }

        /// <summary>
        /// Obter todos cadastros de médicos em memória
        /// </summary>
        /// <returns>Médicos Cadastrados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cadMedicosViewModels = await _cadMedicosServices.GetAll();

            return Ok(cadMedicosViewModels);
        }

        /// <summary>
        /// Atualizar cadastro de um médico específico
        /// </summary>
        /// <remarks>
        /// {"nome": "string", "cpf": "string", "especialidade": "string"}
        /// </remarks>
        /// <param name="id">Identificador do cadastro de um médico específico</param>
        /// <param name="model">Dados do cadastro específico do médico</param>
        /// <returns>Ok</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, CadMedicosInputModel model)
        {
            var updated = await _cadMedicosServices.Update(id, model);

            if (updated)
                return Ok();

            return NotFound();
        }

        /// <summary>
        /// Obter somente cadastros de médicos com status "ativo"
        /// </summary>
        /// <returns>Médicos Cadastrados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet("actives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActives()
        {
            var activeCadMedicos = await _cadMedicosServices.GetActives();
            return Ok(activeCadMedicos);
        }

        /// <summary>
        /// Soft Delete - Desativa registro
        /// </summary>
        /// <param name="id">Identificador do cadastro de um médico específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("soft/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var success = await _cadMedicosServices.SoftDelete(id);
            if (!success)
                return NotFound();

            return Ok("SoftDelete Ok");
        }

        /// <summary>
        /// Hard Delete - Remove um cadastro do banco em memória
        /// </summary>
        /// <param name="id">Identificador do cadastro de um médico específico</param>
        /// <returns>Nada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("hard/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HardDelete(string id)
        {
            var result = await _cadMedicosServices.HardDelete(id);
            if (result)
                return Ok("HardDelete Ok");

            return NotFound();
        }
    }
}
