using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction;
using Template.Application.DTOs.User;
using Template.Application.Results;
using Template.Application.Utilities;
using Template.Domain.DataModels;
using Template.Domain.UnitOfWork;

namespace Template.Application.Services
{
    public partial class Service
    {
 

      



    

        public async Task<List<User_ResponseDTO>> GetAllCars()
        {
            var entities = await _unitOfWork.AutoRepository.GetAllCars();

            var dtos = new List<User_ResponseDTO>();

           

            return dtos;
        }

        public async Task<Result<Auto>> GetUserById(int Id)
        {
            var entity = await _unitOfWork.AutoRepository.GetUserById(Id);

            if (entity == null)
            {
                return Result<Auto>.Failure("Invalid ID. ID must be greater than 0.");
            }
            return Result<Auto>.Success(entity);
        }


       
    }
}
