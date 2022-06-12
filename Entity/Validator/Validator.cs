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
    public class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator()
        {
            RuleFor(x=>x.username).NotEmpty().WithMessage("UserName is empty..!");
            RuleFor(x=>x.password).NotEmpty().WithMessage("Password is empty..!");
            RuleFor(x=>x.citizenshipnumber).NotEmpty().WithMessage("CitizenShip Number is empty..!");
            RuleFor(x=>x.firstname).NotEmpty().WithMessage("FirstName is empty..!");
            RuleFor(x=>x.lastname).NotEmpty().WithMessage("LastName is empty..!");
            RuleFor(x=>x.email).NotEmpty().WithMessage("Email is empty..!");
            RuleFor(x=>x.phonenumber).NotEmpty().WithMessage("Phone Number is empty..!");
            RuleFor(x => x.email).EmailAddress().WithMessage("Please Provide Valid Email Address ..!");
            RuleFor(x => x.password).MinimumLength(8).WithMessage("Please Provide the Password More then 8 Digits");
        }
    }
    
}
