using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonRepository
    {
        Task SavePersonAsync(SignUpDTO person);    
        Person GetPerson(string username, string password);
        User GetPersonById(int id);
        List<User> GetUsers(string fullname);
    }
}
