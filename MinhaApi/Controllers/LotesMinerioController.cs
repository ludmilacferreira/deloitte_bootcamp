using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/LotesMinerio")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LotesMinerioController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLoteMinerioDto input)
        {
            if (string.IsNullOrWhiteSpace(input.CodigoLote))
                return BadRequest("CodigoLote é obrigatório.");
            if (string.IsNullOrWhiteSpace(input.MinaOrigem))
                return BadRequest("MinaOrigem é obrigatória.");
            if (string.IsNullOrWhiteSpace(input.LocalizacaoAtual))
                return BadRequest("LocalizacaoAtual é obrigatória.");
            if (input.TeorFe is < 0 or > 100)
                return BadRequest("TeorFe deve estar entre 0 e 100 (%).");
            if (input.Umidade is < 0 or > 100)
                return BadRequest("Umidade deve estar entre 0 e 100 (%).");
            if (input.Toneladas <= 0)
                return BadRequest("Toneladas deve ser > 0.");
            if (input.Status is < 0 or > 2)
                return BadRequest("Status inválido (use 0, 1 ou 2).");

            var exists = await _db.LotesMinerio.AnyAsync(x => x.CodigoLote == input.CodigoLote);
            if (exists)
                return Conflict($"Já existe um lote com CodigoLote '{input.CodigoLote}'.");

            var lote = new LoteMinerio
            {
                CodigoLote = input.CodigoLote,
                MinaOrigem = input.MinaOrigem,
                TeorFe = input.TeorFe,
                Umidade = input.Umidade,
                SiO2 = input.SiO2,
                P = input.P,
                Toneladas = input.Toneladas,
                DataProducao = input.DataProducao ?? DateTime.UtcNow,
                Status = (StatusLote)input.Status,
                LocalizacaoAtual = input.LocalizacaoAtual
            };

            _db.LotesMinerio.Add(lote);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = lote.Id }, lote);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            return lote is null ? NotFound() : Ok(lote);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteMinerioResponseDto>>> GetAll()
        {
            var lotes = await _db.LotesMinerio.ToListAsync();

           
            var response = lotes.Select(l => new LoteMinerioResponseDto(
                l.Id,
                l.CodigoLote,
                l.MinaOrigem,
                l.TeorFe,
                l.Umidade,
                l.SiO2,
                l.P,
                l.Toneladas,
                l.DataProducao,
                l.Status,
                l.LocalizacaoAtual
            ));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoteMinerioResponseDto>> GetLote(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);

            if (lote == null)
            {
                return NotFound(new { message = "Lote não encontrado." });
            }

            var response = new LoteMinerioResponseDto(
                lote.Id,
                lote.CodigoLote,
                lote.MinaOrigem,
                lote.TeorFe,
                lote.Umidade,
                lote.SiO2,
                lote.P,
                lote.Toneladas,
                lote.DataProducao,
                lote.Status,
                lote.LocalizacaoAtual
            );

            return Ok(response);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateLoteMinerioDto input)
        {
            // 1. Validações manuais (iguais ao Create)
            if (string.IsNullOrWhiteSpace(input.CodigoLote))
                return BadRequest("CodigoLote é obrigatório.");
            if (input.TeorFe is < 0 or > 100)
                return BadRequest("TeorFe deve estar entre 0 e 100 (%).");
            if (input.Toneladas <= 0)
                return BadRequest("Toneladas deve ser > 0.");
            if (input.Status is < 0 or > 2)
                return BadRequest("Status inválido (use 0, 1 ou 2).");

            // 2. Busca o lote no banco pelo ID da URL
            var lote = await _db.LotesMinerio.FindAsync(id);

            // 3. Se não achar, devolve 404
            if (lote == null)
            {
                return NotFound(new { message = "Lote não encontrado para atualização." });
            }

            // 4. Verificação Especial: Duplicidade de Código
            // Se o usuário mudou o código do lote, verificamos se o NOVO código já existe em OUTRO registro
            if (lote.CodigoLote != input.CodigoLote)
            {
                var codigoJaExiste = await _db.LotesMinerio
                    .AnyAsync(x => x.CodigoLote == input.CodigoLote);

                if (codigoJaExiste)
                {
                    return Conflict($"Já existe outro lote com o código '{input.CodigoLote}'.");
                }
            }

            // 5. Atualiza os campos (Mapeamento)
            // Jogamos os dados do DTO (input) para a Entidade do Banco (lote)
            lote.CodigoLote = input.CodigoLote;
            lote.MinaOrigem = input.MinaOrigem;
            lote.TeorFe = input.TeorFe;
            lote.Umidade = input.Umidade;
            lote.SiO2 = input.SiO2;
            lote.P = input.P;
            lote.Toneladas = input.Toneladas;
            // Se DataProducao vier null no update, mantemos a original ou atualizamos? 
            // Abaixo assumi que se vier null, mantemos a que já estava no banco:
            if (input.DataProducao.HasValue) 
                lote.DataProducao = input.DataProducao.Value;
                
            lote.Status = (StatusLote)input.Status;
            lote.LocalizacaoAtual = input.LocalizacaoAtual;

            // 6. Salva as alterações
            // O EF Core detecta que o objeto 'lote' mudou e gera o UPDATE sozinho
            await _db.SaveChangesAsync();

            // 7. Retorna 204 No Content (Padrão de sucesso para Updates)
            return NoContent();
        }
         
         [HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    // 1. Busca o lote no banco
    var lote = await _db.LotesMinerio.FindAsync(id);

    // 2. Se não existir, retorna 404 Not Found
    if (lote == null)
    {
        return NotFound("Lote não encontrado para exclusão.");
    }

    // 3. Remove do contexto (marca para deleção)
    _db.LotesMinerio.Remove(lote);

    // 4. Efetiva a exclusão no banco de dados (DELETE FROM...)
    await _db.SaveChangesAsync();

    // 5. Retorna sucesso sem conteúdo
    return NoContent();
}
    
    }
}

