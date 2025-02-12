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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task SavePersonAsync(SignUpDTO person)
        {
            await _personRepository.SavePersonAsync(person);
        }

        public Person GetPerson(LoginRequestDTO request) 
        {
            return _personRepository.GetPerson(request.username, request.password);
        }

        public User GetPersonById(int id)
        {
            return _personRepository.GetPersonById(id);
        }

        public List<User> GetUsers(FindUserDTO user)
        {
            return _personRepository.GetUsers(user.FullName);
        }
    }
}
