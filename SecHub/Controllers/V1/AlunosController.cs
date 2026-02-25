using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecHub.Mappers;
using SecHub.Rest.Abstractions;
using SecHub.Rest.Alunos;

namespace SecHub.Controllers.V1;

[ApiController]
[Route(Routes.Alunos)]
public class AlunosController(
    EscolaDbContext db,
    ILogger<AlunosController> logger
) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> Obter([FromQuery] int pagina = 1)
    {
        var query = db.Aluno.AsQueryable();

        var totalCount = await query.CountAsync();

        var totalPages = totalCount / PagedQuery.PageSize;

        if (pagina > totalPages)
            return TypedResults.BadRequest("Página inválida.");

        var state = await query
            //.Include(x => x.Responsaveis)
            .OrderByDescending(x => x.DataCriado)
            .ApplyPagination(pagina)
            .ToListAsync();

        var response = new PagedQuery<GetAlunoDto>
        {
            CurrentPage = pagina,
            PageCount = totalCount / PagedQuery.PageSize,
            Items = state.Select(AlunoMapper.ToRest),
        };

        return TypedResults.Ok(response);
    }

    [HttpGet("pesquisar")]
    public async Task<IResult> Pesquisar(string pesquisa, [FromQuery] int pagina = 1, [FromQuery] bool buscarExato = false)
    {
        var query = db.Aluno.AsQueryable();

        if (buscarExato)
            query = query.Where(x => x.Nome.Contains(pesquisa));
        else
            query = query.Where(x => EF.Functions.Like(x.Nome, $"%{pesquisa}%"));

        var totalCount = await query.CountAsync();

        var totalPages = totalCount / PagedQuery.PageSize;

        var result = await query/*.Include(x => x.Responsaveis)*/
            .OrderByDescending(x => x.DataCriado)
            .ApplyPagination(pagina)
            .ToListAsync();

        return TypedResults.Ok(new PagedQuery<GetAlunoDto>
        {
            CurrentPage = pagina,
            PageCount = totalPages,
            Items = result.Select(AlunoMapper.ToRest),
        });
    }

    [HttpPost("cadatrar")]
    public async Task<IResult> Cadastrar([FromBody] CreateAlunoDto state)
    {

        try
        {
            var aluno = AlunoMapper.FromRest(state);

            db.Aluno.Add(aluno);

            await db.SaveChangesAsync();

            return TypedResults.Ok(AlunoMapper.ToRest(aluno));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao cadastrar aluno {@Entity}", state);

            return TypedResults.InternalServerError(ex.Message);
        }
    }

    [HttpGet("detalhes/{alunoId:int}")]
    public async Task<IResult> Detalhes(int alunoId)
    {
        var result = await db.Aluno
            .Include(x => x.Responsaveis)
            .FirstOrDefaultAsync(x => x.Id == alunoId);

        if (result == null)
            return TypedResults.BadRequest("Aluno não encontrado.");

        return TypedResults.Ok(AlunoMapper.ToRest(result));
    }

    [HttpPut("atualizar/{alunoId:int}")]
    public async Task<IResult> Atualizar(int alunoId, [FromBody] UpdateAlunoDto state)
    {
        try
        {
            var entity = await db.Aluno.FirstOrDefaultAsync(x => x.Id == alunoId);

            if (entity == null)
                return TypedResults.BadRequest("Aluno não encontrado.");

            AlunoMapper.ApplyUpdate(state, entity);

            await db.SaveChangesAsync();

            return TypedResults.Ok(AlunoMapper.ToRest(entity));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar aluno {@Entity}", state);

            return TypedResults.InternalServerError(ex.Message);
        }
    }
}