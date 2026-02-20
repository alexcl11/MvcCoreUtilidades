using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache) 
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult MemoriaPersonalizada()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            //COMO ESTO ES MANUAL DEBEMOS PREGUNTAR SI EXISTE  ALGO EN CACHE O NO
            if (this.memoryCache.Get("FECHA")==null)
            {
                //NO EXISTE CACHE TODAVIA
                this.memoryCache.Set("FECHA", fecha);
                ViewData["MENSAJE"] = "Fecha almacenada correctamente";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            } else
            {
                //EXISTE CACHE Y LO RECUPERAMOS
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Fecha recuperada correctamente";
                ViewData["FECHA"] = fecha;
            }
            
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString()+ " -- "+DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}
