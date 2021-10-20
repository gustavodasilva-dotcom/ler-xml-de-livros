using APIdeLivros.Entities;
using APIdeLivros.Models.InputModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APIdeLivros.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly SqlConnection sqlConnection;

        public LivroRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task GravarLog(Retorno retorno, string json)
        {
            #region SQL

            var query = $@"INSERT INTO LOGS VALUES('{json}', '{retorno.Mensagem}', GETDATE());";

            var command = new SqlCommand(query, sqlConnection);

            command.CommandType = CommandType.Text;

            try
            {
                await sqlConnection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }

            #endregion SQL
        }

        public async Task GravarLog(string mensagem, string json)
        {
            #region SQL

            var query = $@"INSERT INTO LOGS VALUES('{json}', '{mensagem}', GETDATE());";

            var command = new SqlCommand(query, sqlConnection);

            command.CommandType = CommandType.Text;

            try
            {
                await sqlConnection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }

            #endregion SQL
        }

        public async Task<bool> LivroExiste(LivroInputModel livro)
        {
            #region SQL

            var resultado = new DataTable();

            var query = $@"SELECT COUNT(*) AS Resultado FROM LIVROS WHERE LIVROS_TITULO_STR = '{livro.Titulo}' AND LIVROS_PRECO_FLOAT = '{livro.Preco}'";

            var command = new SqlCommand(query, sqlConnection);

            command.CommandType = CommandType.Text;

            try
            {
                await sqlConnection.OpenAsync();

                var adapter = new SqlDataAdapter(command);

                adapter.Fill(resultado);

                if ((int)resultado.Rows[0]["Resultado"] == 1)
                    return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }

            return false;
            
            #endregion SQL
        }

        public async Task InserirLivro(Livro livro)
        {
            #region SQL

            var procedure = "[CatalogoLivros].[dbo].[uspInserirLivro]";

            SqlCommand sqlCommand = new SqlCommand(procedure, sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@Autor", SqlDbType.NVarChar).Value = livro.Autor;
            sqlCommand.Parameters.Add("@Titulo", SqlDbType.NVarChar).Value = livro.Titulo;
            sqlCommand.Parameters.Add("@Genero", SqlDbType.NVarChar).Value = livro.Genero;
            sqlCommand.Parameters.Add("@Preco", SqlDbType.NVarChar).Value = livro.Preco;
            sqlCommand.Parameters.Add("@DataPublicacao", SqlDbType.NVarChar).Value = livro.DataPublicacao;
            sqlCommand.Parameters.Add("@Descricao", SqlDbType.NVarChar).Value = livro.Descricao;

            try
            {
                await sqlConnection.OpenAsync();

                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }

            #endregion SQL
        }
    }
}
