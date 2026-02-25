using SecHub.Enums;

namespace SecHub.Rest.Usuario;

public class CriarUsuarioDto
{
    public string Login { get; set; }

    public bool PodeAlterarSenha { get; set; }

    public bool TokenNuncaExpira { get; set; }

    public UsuarioNivelAcesso NivelAcesso { get; set; }

    public string Senha { get; set; }
}

public class GetUsuarioDto
{
    public int Id { get; set; }

    public string Login { get; set; }

    public bool PodeAlterarSenha { get; set; }

    public bool TokenNuncaExpira { get; set; }

    public UsuarioNivelAcesso NivelAcesso { get; set; }

#if DEBUG

    public byte[] PasswordHash { get; set; }

#endif
}

public class GetSaltUsuarioRequestDto
{
    public byte[] Salt { get; set; }
}

public class CriarTokenUsuarioRequestDto
{
    public string Login { get; set; }

    public byte[] Hash { get; set; }
}

public class CriarTokenUsuarioResponseDto
{
    public DateTime ExpiraEm { get; set; }

    public string Token { get; set; }
}