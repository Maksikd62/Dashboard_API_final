using Dashboard.DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Вкажіть назву продукту");
            RuleFor(m => m.ShortDescription)
                .NotEmpty().WithMessage("Опис обов'язковий");
            RuleFor(m => m.Price)
                .NotEmpty().WithMessage("Ціна обов'язкова");
        }
    }
}
