using LerXML.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LerXML.Repository
{
    public class PastaRepository : IPastaRepository
    {
        private readonly SqlConnection sqlConnection;

        public PastaRepository()
        {
            sqlConnection = new SqlConnection("Data Source=DESKTOP-8J62RD3\\SQLEXPRESS;Initial Catalog=CatalogoLivros;Integrated Security=True;Connect Timeout=30");
        }

        public async Task InserirLivro(Livro livro)
        {
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
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }
    }
}
