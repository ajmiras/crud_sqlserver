using System;
using System.Data;
using System.Data.SqlClient;

namespace crud
{
    class CMarcasBD
    {
        private CConexionBD conexionBD = new CConexionBD();
        private SqlCommand sqlCommand = new SqlCommand();
        private SqlDataReader sqlDataReader;

        public int Marca_id { get; set; }
        public String Marca { get; set; }

        public DataTable Seleccionar(int marca_id = 0)
        {
            DataTable dataTable = new DataTable();

            try
            {
                conexionBD.Abrir();

                sqlCommand.Connection = conexionBD.Connection;

                sqlCommand.CommandType = CommandType.Text;

                // Si me indicaron que tenía que obtener todas las marcas, ...
                if (marca_id == 0)
                    sqlCommand.CommandText = "SELECT * FROM marcas ORDER BY marca ASC";
                else
                    // En caso contrario solo una marca.
                    sqlCommand.CommandText = "SELECT * FROM marcas WHERE marca_id = " + marca_id;

                sqlDataReader = sqlCommand.ExecuteReader();

                dataTable.Load(sqlDataReader);

                if ((marca_id != 0) && (dataTable.Rows.Count != 0))
                {
                    DataRow[] rows = dataTable.Select();

                    Marca_id = marca_id;
                    Marca = rows[0]["marca"].ToString();
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