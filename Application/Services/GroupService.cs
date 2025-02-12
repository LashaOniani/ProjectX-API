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

    }
}
