
// PulseHub.Domain/Entities/User.cs

using PulseHub.Domain.Exceptions;

namespace PulseHub.Domain.Entities;

///<summary>
/// Entidade de usuário — coração do domínio.
/// Não conhece banco de dados, não conhece HTTP.
/// Apenas regras de negócio.
///</summary>
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Construtor privado — evita criação sem validação
    private User() { }

    ///<summary>
    /// Factory method — única forma de criar um usuário válido.
    /// Garante que NUNCA existe um User inválido no sistema.
    ///</summary>
    public static User Create(string name, string email)
    {
        // Validação de domínio — regra de negócio pura
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new DomainException("Email inválido");

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.ToLower().Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    ///<summary>
    /// Comportamento de domínio — desativar usuário.
    /// Repare: a entidade encapsula sua própria lógica.
    ///</summary>
    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException("Usuário já está inativo");

        IsActive = false;
    }
}