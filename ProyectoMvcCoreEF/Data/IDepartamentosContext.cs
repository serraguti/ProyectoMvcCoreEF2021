using ProyectoMvcCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMvcCoreEF.Data
{
    public interface IDepartamentosContext
    {
        List<Departamento> GetDepartamentos();
        void InsertarDepartamento(int numero, String nombre, String localidad);
    }
}
