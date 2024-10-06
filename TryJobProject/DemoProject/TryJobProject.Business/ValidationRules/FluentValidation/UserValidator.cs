using Core.Entites.Concrete;
using FluentValidation;

namespace TryJobProject.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().NotNull()
                .WithMessage("isim kısmı boş geçilemez.");
            RuleFor(u => u.LastName).NotEmpty().NotNull()
                .WithMessage("soyisim kısmı boş geçilemez.");
            RuleFor(u => u.Email).NotEmpty().NotNull()
                .WithMessage("email kısmı boş geçilemez.").EmailAddress()
                .WithMessage("Emaili doğru formatta giriniz.");
        }
    }
}
