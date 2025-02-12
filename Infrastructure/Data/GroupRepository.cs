using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly string _connectionString;

        public GroupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Faculty> GetFaculties()
        {
            List<Faculty> faculties = new List<Faculty>();
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projectx_faculties.get_faculties";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                Faculty faculty = new Faculty();
                faculty.FacultyId = int.Parse(reader["id"].ToString());
                faculty.FacultyName = reader["FACULTY_NAME"].ToString();
                faculties.Add(faculty);
            }
            connection.Close();

            return faculties;
        }

    }
}
