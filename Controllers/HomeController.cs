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
            return RedirectToAction("Jugar", new {user = username});
        }
        else{
            return RedirectToAction("ConfigurarJuego");
        }
       
    }
    public IActionResult Jugar(string user)
    {
        ViewBag.username = user;
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
        if (ViewBag.EsCorrecta == true)
        {
            ViewBag.PADRE = "padre-correcta";
            ViewBag.SCREEN = "correct-screen";
            ViewBag.imgCORONITA = "/img/coronita.png";
            ViewBag.PARRAFO = "parrafo-correcta";
            ViewBag.STRONG = "strong-correcta";
            ViewBag.RESULTADO = "CORRECTA!!";
            ViewBag.BOTON = "correcta-button";
            ViewBag.BODY = "body-correcta";
            
            

        }
        else{
            ViewBag.PADRE = "padre-incorrecta";
             ViewBag.SCREEN = "incorrect-screen";
             ViewBag.imgCORONITA = "/img/coronitaIncorrecta.png";
             ViewBag.PARRAFO = "parrafo-incorrecta";
             ViewBag.STRONG = "strong-incorrecta";
             ViewBag.RESULTADO = "INCORRECTA!!";
             ViewBag.BOTON = "incorrect-button";
             ViewBag.BODY = "body-incorrecto";
        }
        return View ("Respuesta");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
