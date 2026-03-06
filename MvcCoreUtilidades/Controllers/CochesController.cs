using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;
using NuGet.Configuration;

namespace MvcCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        private RepositoryCoches repo;
        public CochesController(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        public IActionResult Details(int idCoche)
        {
            Coche coche = this.repo.FindCoche(idCoche);
            return View(coche);
        }

        
        public IActionResult Index()
        {
            return View();
        }
        //TENDREMOS UN IACTIONRESULT PARCIAL PARA INTEGRAR DENTRO DE INDEX
        public IActionResult _CochesPartial()
        {
            //DEBEMOS DEVOLVER EL DIBUJO QUE DESEEAMOS EN AJAX. INDICAMOS EL NOMBRE DEL FICHERO CSHTML Y SU MODEL
            List<Coche> coches = this.repo.GetCoches();
            return PartialView("_CochesPartial", coches);
        }

        public IActionResult _CochesDetails(int idCoche)
        {
            Coche coche = this.repo.FindCoche(idCoche);
            return PartialView("_CochesDetailsView", coche);
        }
    }
}
