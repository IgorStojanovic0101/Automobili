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
 

      



    

        public async Task<List<Auto>> GetAllCars()
        {
            var entities = await _unitOfWork.AutoRepository.GetAllCars();

            var list = new List<Auto>();

           if(entities.Count == 0)
            {
                list.Add(Auto.Create(1, 2020, "Benzin", "BMW"));
                list.Add(Auto.Create(2, 2020, "Benzin", "Audi"));

                return list;
            }

           

            return entities;
        }

        public async Task<Result<Auto>> GetAutoById(int Id)
        {
            var entity = await _unitOfWork.AutoRepository.GetUserById(Id);

            if (entity == null)
            {
                return Result<Auto>.Failure("Invalid ID. ID must be greater than 0.");
            }
            return Result<Auto>.Success(entity);
        }


        public async Task<Result<Auto>> UpdateAuto(Auto auto)
        {
            var entity = await _unitOfWork.AutoRepository.UpdateAsync(auto);

         
            return Result<Auto>.Success(entity);
        }


    }
}
