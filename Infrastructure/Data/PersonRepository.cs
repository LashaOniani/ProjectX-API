using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Application.DTOs;

namespace Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SavePersonAsync(SignUpDTO person)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("olerning.pkg_lo_projcetx_persons.save_person", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = person.FirstName;
                    command.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = person.LastName;
                    command.Parameters.Add("p_gender", OracleDbType.Int32).Value = person.Gender;
                    command.Parameters.Add("p_birthday", OracleDbType.Date).Value = Convert.ToDateTime(person.Birthdate);
                    command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = person.Email;
                    command.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = person.Phone;
                    command.Parameters.Add("p_geolocation", OracleDbType.Varchar2).Value = "testgeo";
                    command.Parameters.Add("p_ip", OracleDbType.Varchar2).Value = "testip";
                    command.Parameters.Add("p_user_name", OracleDbType.Varchar2).Value = person.UserName;
                    command.Parameters.Add("p_password", OracleDbType.Varchar2).Value = person.Password;


                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public Person GetPerson(string username, string password)
        {
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projcetx_persons.auth_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
            cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;

            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Person person = new Person();
                person.Id = int.Parse(reader["p_id"].ToString());
                person.R_id = int.Parse(reader["r_id"].ToString());
                return person;
            }
            return null;
        }

        public User GetPersonById(int id)
        {
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projcetx_persons.get_auth_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;

            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                User person = new User();
                person.Id = int.Parse(reader["p_id"].ToString());
                person.FirstName = reader["FIRST_NAME"].ToString();
                person.LastName = reader["LAST_NAME"].ToString();
                person.GenderStr = reader["GENDER"].ToString();
                person.Birthday = Convert.ToDateTime(reader["BIRTHDAY"].ToString());
                person.Email = reader["EMAIL"].ToString();
                person.Phone = reader["PHONE"].ToString();
                person.UserName = reader["USER_NAME"].ToString();
                person.Password = reader["PASSWORD"].ToString();    
                return person;
            }
            return null;
        }

        public List<User> GetUsers(string fullname)
        { 
            List<User> users = new List<User>();

            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projcetx_persons.find_person_by_full_name";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_full_name", OracleDbType.Varchar2).Value = fullname;

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            { 
                User person = new User();

                person.Id = int.Parse(reader["p_id"].ToString());
                person.FirstName = reader["FIRST_NAME"].ToString();
                person.LastName = reader["LAST_NAME"].ToString();
                person.Email = reader["EMAIL"].ToString();
                person.Phone = reader["PHONE"].ToString();
                person.UserName = reader["USER_NAME"].ToString();

                users.Add(person);
            } 

            return users;
        }
    }
}
