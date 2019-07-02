using Communication.BL.Repositories;
using Communication.BL.Models;
using Communication.DAL;
using System;


namespace Communication.App.ViewModels
{
    class UserViewModel
    {
        private readonly UserRepository repository = new UserRepository(new CommunicationDbContext());
        public UserModel model { get; set; } = new UserModel();
        public void Load(Guid id)
        {
            model = repository.GetById(id);
        }

        public void Save()
        {
            if(model.Id == Guid.Empty)
            {
                repository.Insert(model);
            }
            else
            {
                repository.Update(model);
            }
        }

        public void Delete() => repository.Delete(model);
    }
}
