using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonService
    {
        Task SavePersonAsync(SignUpDTO person);
        Person GetPerson(LoginRequestDTO request);
        User GetPersonById(int id);
        List<User> GetUsers(FindUserDTO user);
    }
}
