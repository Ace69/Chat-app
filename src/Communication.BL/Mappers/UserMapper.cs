using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    public class UserMapper : AbstractMapper<UserEntity,UserModel> 
    {
        public override UserModel EntityToModel(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email,
                Password = entity.Password,
                Photo = entity.Photo,
                TelephoneNumber = entity.TelephoneNumber,
                isEnabled = entity.isEnabled
            };
        }

        public override UserEntity ModelToEntity(UserModel model)
        {
            if(model == null)
            {
                return null;
            }

            return new UserEntity
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password,
                Photo = model.Photo,
                TelephoneNumber = model.TelephoneNumber,
                isEnabled = model.isEnabled
            };
        }
       
    }
}
