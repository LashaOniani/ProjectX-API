using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupService : IGroupService
    {

        private readonly IGroupRepository _groupRepository;
    
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public List<Faculty> GetFaculties()
        {
                return _groupRepository.GetFaculties();
        }

        public List<Group> GetGroups() 
        { 
            return _groupRepository.GetGroups();
        }

        public List<Group> GetGroupByGroupNumber(GroupDTO group) 
        { 
            return _groupRepository.GetGroupByGroupNumber(group.G_Number);
        }

        public void AddGroup(GroupDTO group)
        {   
            Group repoGroup = new Group();
            repoGroup.Id = group.Id;
            repoGroup.G_Number = group.G_Number;
            repoGroup.G_Description = group.G_Description;  

            _groupRepository.AddGroup(repoGroup);
        }

        public void DeleteGroup(GroupDTO group)
        {
            _groupRepository.DeleteGroup(group.Id);
        }

    }
}
