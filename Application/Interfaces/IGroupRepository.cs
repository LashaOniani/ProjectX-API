using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGroupRepository
    {
        List<Faculty> GetFaculties();
        List<Group> GetGroups();
        List<Group> GetGroupByGroupNumber(string groupNumber);
        void AddGroup(Group group);
        void DeleteGroup(int groupId);

    }
}
