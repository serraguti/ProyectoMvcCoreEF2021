using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ProyectoMvcCoreEF.Models;

namespace ProyectoMvcCoreEF.Data
{
    public class DepartamentosContextSQLServer: IDepartamentosContext
    {
        private DataTable tabla;
        private SqlDataAdapter addept;
        private String CadenaConexion;
        private SqlConnection cn;
        private SqlCommand com;

        public DepartamentosContextSQLServer(string cadenaconexion)
        {
            this.CadenaConexion = cadenaconexion;
            this.cn = new SqlConnection(this.CadenaConexion);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.CargarDatos();
        }

        public void InsertarDepartamento(int numero, string nombre, string localidad)
        {
            string sql = "insert into dept values (@numero, @nombre, @localidad)";
            this.com.Parameters.AddWithValue("@numero", numero);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@localidad", localidad);
            this.com.CommandText = sql;
            this.com.CommandType = CommandType.Text;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            this.CargarDatos();
        }

        private void CargarDatos()
        {
            this.addept = new SqlDataAdapter("select * from dept", this.CadenaConexion);
            this.tabla = new DataTable();
            this.addept.Fill(this.tabla);
        }

        public List<Departamento> GetDepartamentos()
        {
            List<Departamento> lista = new List<Departamento>();
            var consulta = from datos in this.tabla.AsEnumerable()
                           select datos;
            foreach (var fila in consulta)
            {
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = fila.Field<int>("DEPT_NO");
                departamento.Nombre = fila.Field<String>("DNOMBRE");
                departamento.Localidad = fila.Field<String>("LOC");
                lista.Add(departamento);
            }
            return lista;
        }
    }
}
