using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace crud
{
    class CCategoriasBD
    {
        private CConexionBD conexionBD = new CConexionBD();
        private SqlCommand sqlCommand = new SqlCommand();
        private SqlDataReader sqlDataReader;

        public int Categoria_id { get; set; }
        public String Categoria { get; set; }

        public DataTable Seleccionar(int categoria_id = 0)
        {
            DataTable dataTable = new DataTable();
            try
            {
                conexionBD.Abrir();

                sqlCommand.Connection = conexionBD.Connection;

                sqlCommand.CommandType = CommandType.Text;

                // Si me indicaron que tenía que obtener todas las categorías, ...
                if (categoria_id == 0)
                    sqlCommand.CommandText = "SELECT * FROM categorias ORDER BY categoria ASC";
                else
                    // En caso contrario solo una categoría.
                    sqlCommand.CommandText = "SELECT * FROM categorias WHERE categoria_id=" + categoria_id;

                sqlDataReader = sqlCommand.ExecuteReader();

                dataTable.Load(sqlDataReader);

                if ((categoria_id != 0) && (dataTable.Rows.Count != 0))
                {
                    DataRow[] rows = dataTable.Select();

                    Categoria_id = categoria_id;
                    Categoria = rows[0]["categoria"].ToString();
                }
            }
            finally
            {
                sqlDataReader.Close();

                conexionBD.Cerrar();
            }

            return dataTable;
        }
    }
}
