﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TunisBrandCo.Domain.Features.Clients;

namespace TunisBrandCo.Infra.Data.Features.Clients
{
    public class ClientDAO
    {
        private const string _connectionString = @"Data Source=.\SQLEXPRESS;initial catalog=TUNISBRANDCO_DB;uid=sa;pwd=tunico;";

        //TUNISBRANDCO_DB

        public void AddClient(Client newClient)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var DoCommand = new SqlCommand())
                {
                    DoCommand.Connection = connection;
                    string sql = @"INSERT INTO CLIENT (CPF, CLIENT_NAME, BIRTHDATE) VALUES (@CPF, @CLIENT_NAME, @BIRTHDATE);";
                    ConvertObjectToSql(newClient, DoCommand);
                    DoCommand.CommandText = sql;
                    DoCommand.ExecuteNonQuery(); 
                }
            }
        }

        public Client GetClientById(int clientId)
        {
            var client = new Client();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM CLIENT WHERE ID = @ID";
                    comando.CommandText = sql;
                    comando.Parameters.AddWithValue("@ID", clientId);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        client = ConvertSqlToObjetc(reader);
                    };
                    return client;
                }
            }
        }

        public List<Client> GetAllClients()
        {
            var clientList = new List<Client>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var DoCommand = new SqlCommand())
                {
                    DoCommand.Connection = connection;
                    string sql = @"SELECT * FROM CLIENT";
                    DoCommand.CommandText = sql;
                    SqlDataReader reader = DoCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Client wantedClient = ConvertSqlToObjetc(reader);
                        clientList.Add(wantedClient);
                    }
                }
            }
            return clientList;
        }

        public void UpdateLoyaltyPoint(int clientId, decimal points)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var DoCommand = new SqlCommand())
                {
                    DoCommand.Connection = connection;
                    string sql = @"UPDATE CLIENT SET            
                                        LOYALTY_POINTS = @LOYALTY_POINTS
                                        WHERE ID = @ID;";
                    DoCommand.Parameters.AddWithValue("@ID", clientId);
                    DoCommand.Parameters.AddWithValue("@LOYALTY_POINTS", points);
                    DoCommand.CommandText = sql;
                    DoCommand.ExecuteNonQuery();
                }
            }
        }

        public Client GetClientByCpf(string cpf)
        {
            var wantedClient = new Client();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT * FROM CLIENT WHERE CPF = @CPF";
                    comando.CommandText = sql;
                    comando.Parameters.AddWithValue("@CPF", cpf);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        wantedClient = ConvertSqlToObjetc(reader);
                    };
                    return wantedClient;
                }
            }
        }

        public void DeleteCliente(int clientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var DoCommand = new SqlCommand())
                {
                    DoCommand.Connection = connection;
                    string sql = @"DELETE FROM CLIENT WHERE ID = @ID;";
                    DoCommand.Parameters.AddWithValue("@ID", clientId);
                    DoCommand.CommandText = sql;
                    DoCommand.ExecuteNonQuery();
                }
            }
        }

        public void UpdateClient(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var DoCommand = new SqlCommand())
                {
                    DoCommand.Connection = connection;
                    string sql = @"UPDATE CLIENT SET            
                                        CPF = @CPF,
                                        CLIENT_NAME = @CLIENT_NAME,
                                        BIRTHDATE = @BIRTHDATE
                                        WHERE ID = @ID;";
                    DoCommand.Parameters.AddWithValue("@ID", client.Id);
                    ConvertObjectToSql(client, DoCommand);
                    DoCommand.CommandText = sql;
                    DoCommand.ExecuteNonQuery();
                }
            }
        }

        private Client ConvertSqlToObjetc(SqlDataReader reader)
        {
            Client client = new Client();

            client.Id = Convert.ToInt32(reader["ID"].ToString());
            client.Cpf = reader["CPF"].ToString();
            client.Name = reader["CLIENT_NAME"].ToString();
            client.BirthDate = Convert.ToDateTime(reader["BIRTHDATE"].ToString());
            client.LoyaltyPoints = Convert.ToDecimal(reader["LOYALTY_POINTS"].ToString());


            return client;
        }

        private void ConvertObjectToSql(Client client, SqlCommand doCommand)
        {
            doCommand.Parameters.AddWithValue("@CPF", client.Cpf);
            doCommand.Parameters.AddWithValue("@CLIENT_NAME", client.Name);
            doCommand.Parameters.AddWithValue("@BIRTHDATE", client.BirthDate);
            
        }
    }
}
