using Riok.Mapperly.Abstractions;
using SecHub.Models;
using SecHub.Rest.Alunos;
using SecHub.Rest.Responsaveis;

namespace SecHub.Mappers;

[Mapper]
public static partial class AlunoMapper
{
    public static partial GetAlunoDto ToRest(Aluno entity);

    public static partial Aluno FromRest(CreateAlunoDto entity);

    public static partial void ApplyUpdate(UpdateAlunoDto state, Aluno entity);
}

[Mapper]
public static partial class ResponsavelMapper
{
    public static partial GetResponsavelDto ToRest(Responsavel entity);
}