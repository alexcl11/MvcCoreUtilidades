using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreUtilidades.Helpers;

//ENUMERACION CON LAS CARPETAS QUE DESEEMOS SUBIR FICHEROS
public enum Folders {Uploads, Images, Facturas, Productos}
public class HelperPathProvider
{
    private IWebHostEnvironment hostEnvironment;
    private IServer server;
    public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
    {
        this.hostEnvironment = hostEnvironment;
        this.server = server;
    }

    // TENDREMOS UN METODO QUE SE ENCARGARA DE RESOLVER LA RUTA 
    // COMO STRING CUANDO RECIBAMOS EL FICHERO Y LA RUTA
    public string MapPath(string fileName, Folders folders)
    {
        string carpeta = "";
        if (folders == Folders.Uploads) 
        {
            carpeta = "uploads";
        }
        else if (folders == Folders.Images)
        {
            carpeta = "images";
        }
        else if (folders == Folders.Facturas)
        {
            carpeta = "facturas";
        }
        string rootPath = this.hostEnvironment.WebRootPath;
        string path = Path.Combine(rootPath, carpeta, fileName);
        return path;
    }

    public string MapUrlPath(string fileName, Folders folders)
    {
        string carpeta = "";
        if (folders == Folders.Uploads)
        {
            carpeta = "uploads";
        }
        else if (folders == Folders.Images)
        {
            carpeta = "images";
        }
        else if (folders == Folders.Facturas)
        {
            carpeta = "facturas";
        }
        else if (folders == Folders.Productos)
        {
            //ESTA SI VA A CAMBIAR PORQUE ESTO SI ES SISTEMA DE FICHEROS
            // NECESITAMOS WEB
            carpeta = "images/productos";
        }
        //http://localhost:999/images/productos/1.png
        //Quiero buscar la forma de recuperar la URL de nuestro Server en 
        //en MVC Net Core
        var addresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
        string serverURL = addresses.FirstOrDefault();
        //DEVOLVEMOS LA RUTA URL
        string urlPath = serverURL + "/" + carpeta + "/" + fileName;
        return urlPath;

    }
}
