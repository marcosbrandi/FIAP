using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.DTOs;
using TechChallengeFIAP.Errors;

namespace TechChallengeFIAP.Controllers
{
  
    public class ContatosController : BaseApiController
    {
        private readonly IGenericRepository<Contato> _contatoRepository;
        public IMapper _Mapper;

        public ContatosController(IGenericRepository<Contato> contatoRepository, IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _Mapper = mapper;
        }

        [HttpGet(nameof(GetContatos))]
        public async Task<ActionResult<List<ContatoDto>>> GetContatos()
        {
            var contatos = await _contatoRepository.GetAllAsync();
            var data = _Mapper.Map<IReadOnlyList<Contato>, IReadOnlyList<ContatoDto>>(contatos);
            return Ok(data);
        }

        [HttpGet(nameof(GetContatoById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse),StatusCodes.Status404NotFound)]
        public async Task<ContatoDto> GetContatoById([FromQuery] int Id)
        {
            var contatos = await _contatoRepository.GetByIdAsync(Id);
            return _Mapper.Map<ContatoDto>(contatos);
        }

    }
}

