using ProyectoMvcCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoMvcCoreEF.Data
{
    public class DepartamentosContextMySql : IDepartamentosContext
    {
        private MySqlDataAdapter addept;
        private DataTable tabla;
        private MySqlConnection cn;
        private MySqlCommand com;
        private String CadenaConexion;

        public DepartamentosContextMySql(string cadenaconexion)
        {
            this.CadenaConexion = cadenaconexion;
            this.CargarDatos();
            this.cn = new MySqlConnection(this.CadenaConexion);
            this.com = new MySqlCommand();
            this.com.Connection = this.cn;
        }

        private void CargarDatos()
        {
            this.addept = new MySqlDataAdapter("select * from dept", this.CadenaConexion);
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
                departamento.Nombre = fila.Field<string>("DNOMBRE");
                departamento.Localidad = fila.Field<string>("LOC");
                lista.Add(departamento);
            }
            return lista;
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
    }
}
