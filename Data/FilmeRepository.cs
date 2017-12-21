using Dapper;
using Npgsql;
using System.Data;
using System.Linq;

namespace Data
{
    public class FilmeRepository : BaseRepository, IFilmeRepository
    {
        public bool ChecaDisponibilidade(int filmeId)
        {
            bool ret;
            using (var db = new NpgsqlConnection(Connstring))
            {
                const string sql = @"SELECT ""DataDevolucao"" FROM ""tbLocacao"" WHERE @Id = ""tbFilmeID"" ORDER BY ""dataLocacao"" DESC LIMIT 1;";

                var result = db.Query<string>(sql, new { Id = filmeId }, commandType: CommandType.Text).Single();
                ret = result != null;
            }

            return ret;
        }

        public bool Reservar(int filme, int clienteId)
        {
            bool ret;
            using (var db = new NpgsqlConnection(Connstring))
            {
                const string sql = @"INSERT INTO ""tbLocacao""
                (""tbClienteID"", ""tbFilmeID"", ""dataLocacao"", ""DataDevolucao"")
                VALUES (@ClienteID, @FilmeID, NOW(), NULL);";

                ret = db.Query<bool>(sql, new { FilmeID = filme, ClienteID = clienteId }, commandType: CommandType.Text).Any();
            }

            return ret;
        }

        public void Devolver(int filmeId)
        {
            using (var db = new NpgsqlConnection(Connstring))
            {
                const string sql = @"UPDATE ""tbLocacao""
                SET  ""DataDevolucao"" = NOW()
                WHERE ""tbFilmeID"" = @Id";

                db.Execute(sql, new { Id = filmeId }, commandType: CommandType.Text);
            }
        }
    }
}