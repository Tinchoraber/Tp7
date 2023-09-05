using System.Data.SqlClient;
using Dapper;
public static class BD{
     private static string connectionString = @"Server=DESKTOP-OF64MA2\SQLEXPRESS;DataBase=PreguntadOrt;Trusted_Connection=True;";
     public static List<Categorias> ObtenerCategorias()
    {

        List<Categorias> listCategoria = new List<Categorias>();
        string SQL = "SELECT * FROM Categorias";
        using (SqlConnection db = new SqlConnection(connectionString))
        {
            listCategoria = db.Query<Categorias>(SQL).ToList();
        }
        return listCategoria;

    }

     public static List<Dificultad> ObtenerDificultades()
    {

        List<Dificultad> ListDificultad = new List<Dificultad>();
        string SQL = "SELECT * FROM Dificultad";
        using (SqlConnection db = new SqlConnection(connectionString))
        {
            ListDificultad = db.Query<Dificultad>(SQL).ToList();
        }
        return ListDificultad;
    }

    public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
    {
        List<Preguntas> ListPreguntas = new List<Preguntas>();
        using (SqlConnection db = new SqlConnection(connectionString))
        {
        if (dificultad == -1) 
        {
            if(categoria == -1)
            {
            string SQL = "SELECT * FROM Preguntas";
            ListPreguntas = db.Query<Preguntas>(SQL).ToList(); 
            }
            else 
            {
                string SQL = "SELECT * FROM Preguntas WHERE IdCategoria = @pCategoria";
                ListPreguntas = db.Query<Preguntas>(SQL, new{@pCategoria = categoria}).ToList();
            }
        }
        else if(categoria == -1)
        {
            string SQL = "SELECT * FROM Preguntas WHERE IdDificultad = @pDificultad";
            ListPreguntas = db.Query<Preguntas>(SQL, new{@pDificultad = dificultad}).ToList();
        }
        else
        {
             string SQL = "SELECT * FROM Preguntas WHERE IdDificultad = @pDificultad AND IdCategoria = @pCategoria";
            ListPreguntas = db.Query<Preguntas>(SQL, new{@pDificultad = dificultad, @pCategoria = categoria}).ToList();
        }
        }
     return ListPreguntas;
    }


      public static List<Respuestas> ObtenerRespuestas(List<Preguntas> ListPreguntas)
    {
        List<Respuestas> ListaRespuestas = new List<Respuestas>();
        using (SqlConnection db = new SqlConnection(connectionString))
        {
            foreach (Preguntas pregunta in ListPreguntas)
            {
                string SQL = "SELECT * FROM Respuestas WHERE IdPregunta = @pRespuesta";
                ListaRespuestas.AddRange(db.Query<Respuestas>(SQL, new{pRespuesta = pregunta.IdPregunta}).ToList());
            }
        }
        return ListaRespuestas;
    }
    public static Categorias ObtenerNombreCategoria(int categoria)
    {
        Categorias Cat = new Categorias();
         using (SqlConnection db = new SqlConnection(connectionString))
        {
            string SQL = "SELECT * FROM Categorias WHERE IdCategoria = @pCategoriaa";
            Cat = db.QueryFirstOrDefault<Categorias>(SQL, new{@pCategoriaa = categoria});
        }
        return Cat;
        
    }
}
  

