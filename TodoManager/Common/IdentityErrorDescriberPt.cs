using Microsoft.AspNetCore.Identity;

namespace TodoManager.Common
{
    public class IdentityErrorDescriberPt
    {
        public static string GetFirstErrorMessage(IEnumerable<IdentityError> errors)
        {
            if (errors == null || !errors.Any())
                return "Ocorreu um erro desconhecido.";

            var firstError = errors.First();

            return firstError.Code switch
            {
                nameof(IdentityErrorDescriber.DuplicateUserName) => $"O nome de usuário já está em uso.",
                nameof(IdentityErrorDescriber.DuplicateEmail) => $"O email informado já está cadastrado.",
                nameof(IdentityErrorDescriber.PasswordTooShort) => $"A senha é muito curta. Deve ter pelo menos 6 caracteres.",
                nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => $"A senha deve conter pelo menos um caractere especial.",
                nameof(IdentityErrorDescriber.PasswordRequiresDigit) => $"A senha deve conter pelo menos um número.",
                nameof(IdentityErrorDescriber.PasswordRequiresUpper) => $"A senha deve conter pelo menos uma letra maiúscula.",
                nameof(IdentityErrorDescriber.PasswordRequiresLower) => $"A senha deve conter pelo menos uma letra minúscula.",
                nameof(IdentityErrorDescriber.InvalidEmail) => $"O email precisa ser válido.",
                _ => firstError.Description
            };
        }
    }
}
