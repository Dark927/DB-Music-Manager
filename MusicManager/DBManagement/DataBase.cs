using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Text.RegularExpressions;
using MusicManager.DBManagement.Query;

namespace MusicManager.DBManagement
{
    internal sealed class DataBase
    {
        private readonly string _connectionString;
        private SqlConnection _connection;


        public DataBase(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        public DataTable SendQuery(DBQuery query)
        {
            DataTable resultDataTable = new DataTable();

            try
            {
                SqlCommand queryCommand = GenerateQueryCommand(query);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryCommand);

                using (sqlDataAdapter)
                {
                    sqlDataAdapter.Fill(resultDataTable);
                }

                return resultDataTable;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this.ToString() + " " + ex.Message);
                return resultDataTable;
            }
        }

        public DBQuery CreateQuery(string text, params string[] parameters)
        {
            return new DBQuery(text, parameters);
        }

        public DataTable CreateAndSendQuery(string text, params string[] parameters)
        {
            return SendQuery(new DBQuery(text, parameters));
        }

        private SqlCommand GenerateQueryCommand(DBQuery query)
        {
            SqlCommand queryCommand = new SqlCommand(query.Text, _connection);

            if (query.Parameters.Length != 0)
            {
                MatchCollection paramMatches = FindAllQueryParameters(query.Text);
                bool invalidParamsCount = query.Parameters.Length != paramMatches.Count;

                if (invalidParamsCount)
                {
                    throw new ArgumentException($"{nameof(query.Parameters)} length != {nameof(paramMatches)} length! Wrong query.");
                }

                for (int currentParam = 0; currentParam < query.Parameters.Length; currentParam++)
                {
                    queryCommand.Parameters.AddWithValue(paramMatches[currentParam].Value, query.Parameters[currentParam]);
                }
            }

            return queryCommand;
        }

        private MatchCollection FindAllQueryParameters(string queryText)
        {
            string paramPattern = @"@\w+";
            return Regex.Matches(queryText, paramPattern);
        }
    }
}
