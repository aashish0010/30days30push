using Entity.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entity.Validator
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is empty..!");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Password is empty..!");
        }

        
    }
    
}
