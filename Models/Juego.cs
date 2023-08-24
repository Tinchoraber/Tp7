public static class Juego
{
    private static string Username;
    private static int PuntajeActual;
    private static int CantidadPreguntasCorrectas;
    private static List<Preguntas> LiPreguntas  = new List<Preguntas>();
    private static List<Respuestas> LiRespuestas  = new List<Respuestas>();
    
    public static void InicializarJuego()
    {
        Username = "";
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
    }
    public static List<Categorias> ObtenerCategorias()
    {
        return BD.ObtenerCategorias();
    }
    public static List<Dificultad> ObtenerDificultades()
    {
        return BD.ObtenerDificultades();
    }
    public static void CargarPartida(string username, int dificultad, int categoria)
    {
        Username = username;
        LiPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
        LiRespuestas = BD.ObtenerRespuestas(LiPreguntas);
    }
    
    public static Preguntas ObtenerProximaPregunta()
    {
        if(LiPreguntas.Count > 0)
        {
            Random rnd = new Random();
            return LiPreguntas[rnd.Next(0, LiPreguntas.Count-1)];
        }
        else
        {
            return null;
        }
    }
    public static List<Respuestas> ObtenerProximasRespuestas(int idPregunta){
     List<Respuestas> proxRespuestas = new List<Respuestas>();
     foreach (Respuestas respuesta in LiRespuestas)
     {
        if(respuesta.IdPregunta == idPregunta)
        {
            proxRespuestas.Add(respuesta);
        }
     }
     return proxRespuestas;
    }
    public static bool VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        bool resp = false;
        foreach (Respuestas item in LiRespuestas)
        {
            if (item.Correcta == true)
            {
                PuntajeActual = PuntajeActual + 10;
                CantidadPreguntasCorrectas++;
                resp = true;
            }
            else
            {
                resp = false;
            }
            LiPreguntas.Remove(LiPreguntas[idPregunta]);
        }
        return resp;
    }
}
