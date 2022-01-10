using Microsoft.AspNetCore.Mvc;
using ProyectoMvcCoreEF.Data;
using ProyectoMvcCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMvcCoreEF.Controllers
{
    public class DepartamentosController : Controller
    {
        private IDepartamentosContext context;

        public DepartamentosController(IDepartamentosContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Departamento> departamentos = this.context.GetDepartamentos();
            return View(departamentos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Departamento departamento)
        {
            this.context.InsertarDepartamento(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Index");
        }
    }
}
