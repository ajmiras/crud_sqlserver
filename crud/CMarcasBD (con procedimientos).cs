using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                sqlCommand.CommandText = "MarcasSeleccionar";
                sqlCommand.Parameters.AddWithValue("@marca_id", marca_id);
                sqlCommand.CommandType = CommandType.StoredProcedure;

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
                sqlCommand.Parameters.Clear();
                sqlDataReader.Close();
                conexionBD.Cerrar();
            }

            return dataTable;
        }
    }
}
