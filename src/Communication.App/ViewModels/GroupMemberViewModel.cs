using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.DAL;
using System;

namespace Communication.App.ViewModels
{
    class GroupMemberViewModel
    {
        private readonly GroupMemberRepository repository = new GroupMemberRepository(new CommunicationDbContext());
        public GroupMemberModel model { get; set; } = new GroupMemberModel();
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
