using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGroupService
    {
        List<Faculty> GetFaculties();
        List<Group> GetGroups();
        List<Group> GetGroupByGroupNumber(GroupDTO group);
        void AddGroup(GroupDTO group);
        void DeleteGroup(GroupDTO group);
    }
}
