using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.DAL;
using System;

namespace Communication.App.ViewModels
{
    class GroupViewModel : ViewModelBase
    {
        private readonly GroupRepository repository = new GroupRepository(new CommunicationDbContext());
        public GroupModel model { get; set; } = new GroupModel();
        public void Load(Guid id)
        {
            model = repository.GetById(id);
        }

        public void Save()
        {
            if (model.Id == Guid.Empty)
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
