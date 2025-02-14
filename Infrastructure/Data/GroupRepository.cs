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

        public List<Group> GetGroups()
        {
            List<Group> groups = new List<Group>();
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projectx_groups.select_all_groups";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Group group = new Group();
                group.Id = int.Parse(reader["id"].ToString());
                group.G_Number = reader["G_NUMBER"].ToString();
                group.G_Description = reader["G_DESCRIPTION"].ToString();
                groups.Add(group);
            }

            connection.Close(); 

            return groups;
        }

        public List<Group> GetGroupByGroupNumber(string groupNumber)
        {
            List<Group> groups = new List<Group>();
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projectx_groups.find_group_by_group_number";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_g_number", OracleDbType.Varchar2).Value = groupNumber;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Group group = new Group();
                group.Id = int.Parse(reader["id"].ToString());
                group.G_Number = reader["G_NUMBER"].ToString();
                group.G_Description = reader["G_DESCRIPTION"].ToString();
                groups.Add(group);
            }

            connection.Close();

            return groups;
        }

        public void AddGroup(Group group)
        {
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projectx_groups.add_group";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_g_number", OracleDbType.Varchar2).Value = group.G_Number;
            cmd.Parameters.Add("p_g_description", OracleDbType.Varchar2).Value = group.G_Description;

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteGroup(int groupId)
        {
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = "olerning.pkg_lo_projectx_groups.delete_group_by_group_number";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = groupId;

            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
