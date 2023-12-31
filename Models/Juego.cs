public static class Juego
{
   
    public static string Username;
    public static int PuntajeActual;
    public static int CantidadPreguntasCorrectas;
     public static int CantidadPreguntas;
    private static int PreguntaAEliminar;
    private static List<Preguntas> LiPreguntas  = new List<Preguntas>();
    private static List<Respuestas> LiRespuestas  = new List<Respuestas>();
    
    public static void InicializarJuego()
    {
        Username = "";
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        CantidadPreguntas = 1;
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
            PreguntaAEliminar = rnd.Next(0, LiPreguntas.Count-1);
            return LiPreguntas[PreguntaAEliminar];
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
            if (item.Correcta == true && item.IdRespuesta == idRespuesta)
            {
                
                PuntajeActual = PuntajeActual + 10;
                CantidadPreguntasCorrectas++;
                resp = true;
            }
        }
        LiPreguntas.RemoveAt(PreguntaAEliminar);
        return resp;
    }
    public static int DevolverCantPreguntas()
    {
        return CantidadPreguntas;
    }
    public static int DevolverPreguntas()
    {
        return LiPreguntas.Count;
    }
    public static string ObtenerRespuestaCorrecta(int idPregunta)
    {
        List<Respuestas> Respuesta1Preg = ObtenerProximasRespuestas(idPregunta);
        foreach (Respuestas item in Respuesta1Preg)
        {
            if(item.IdPregunta == idPregunta && item.Correcta == true)
            {
                return item.Contenido;
            }
        }
        return "";
    }
}

