using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP7.Models;

namespace TP7.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego(){
        Juego.InicializarJuego();
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
        return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria){
        Juego.CargarPartida(username, dificultad, categoria);
        if (Juego.ObtenerProximaPregunta() != null)
        {
            return RedirectToAction("Jugar");
        }
        else{
            return RedirectToAction("ConfigurarJuego");
        }
       
    }
    public IActionResult Jugar()
    {
        ViewBag.pregunta = Juego.ObtenerProximaPregunta();
        if(ViewBag.pregunta == null)
        {
            return View ("Fin");
        }
        else
        {
            ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.pregunta.IdPregunta);
            return View ("Jugar");
        }
    }
    [HttpPost] public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
        ViewBag.EsCorrecta = Juego.VerificarRespuesta(idPregunta, idRespuesta);
        if (Juego.VerificarRespuesta(idPregunta, idRespuesta) == true)
        {
            ViewBag.RESPUESTA = "LA RESPUESTA ES CORRECTA";
        }
        else{
            ViewBag.RESPUESTA = "LA RESPUESTA ES INCORRECTA";
        }
        return View ("Respuesta");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
