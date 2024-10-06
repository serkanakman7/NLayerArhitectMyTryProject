using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Customers;
using TryJobProject.Entities.Concrete;

namespace TryJobProject.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty().NotNull()
                .WithMessage("İsim alanını boş bırakmayınız.");
            RuleFor(x => x.LastName).NotEmpty().NotNull()
                .WithMessage("Soyad alanını boş bırakmayınız.");
            RuleFor(x => x.Phone).NotEmpty().NotNull()
                .WithMessage("Telefon kısmını boş bırakmayınız").Length(11)
                .WithMessage("Lütfen 11 karakter giriniz").Must(NumericCharacter)
                .WithMessage("Lütfen sayısal değer giriniz");
        }

        private bool NumericCharacter(string arg)
        {
            char[] chars = arg.ToCharArray();
            char[] number = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            for (int i = 0; i < chars.Length; i++)
            {
                for(int j =  0; j < number.Length; j++)
                {
                    if (chars[i] != number[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
