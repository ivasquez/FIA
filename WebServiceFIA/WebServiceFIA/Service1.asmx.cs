using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;


namespace WebServiceFIA
{
    
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://localhost")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        /*
         Consulta todas las categorias de los eventos.
         * 
         * **NOTA** Si ocupa que sólo le retone una lista con los nombres de las categorias para mayor facilidad me 
         * avisa. ATT: Génesis :D
         */
        [WebMethod]
        public List<List<string>> ConsultarCategorias()
        {
            MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<List<string>> categorias = new List<List<string>>();

            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("ConsultarCategorias", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    List<string> DatosCategoria = new List<string>();
                    string idCategoria = (string)rdr[0].ToString();
                    string nombre = (string)rdr[1].ToString();

                    Console.WriteLine(idCategoria + " "+nombre);

                    DatosCategoria.Add(idCategoria);
                    DatosCategoria.Add(nombre);

                    categorias.Add(DatosCategoria);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista de listas. Cada sublista contiene los datos de las categorias en el siguiente orden:
            // idCategoria, nombre
            return categorias;
            
        }
		
        /*
         Consulta todos los eventos de una categoría en específico.
         */
		[WebMethod]
        public List<List<string>> EventosPorCategoria(int idCategoria)
        {
			MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<List<string>> eventosCategorias = new List<List<string>>();

            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("EventosPorCategoria", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("idCategoria", idCategoria));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    List<string> DatosEventosCategoria = new List<string>();
                    string idEvento = (string)rdr[0].ToString();
                    string titulo = (string)rdr[1].ToString();
                    string fecha = (string)rdr[2].ToString();
                    string hora = (string)rdr[3].ToString();

                    Console.WriteLine(idEvento+" "+titulo+" "+fecha+" "+hora);

                    DatosEventosCategoria.Add(idEvento);
                    DatosEventosCategoria.Add(titulo);
                    DatosEventosCategoria.Add(fecha);
                    DatosEventosCategoria.Add(hora);

                    eventosCategorias.Add(DatosEventosCategoria);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista de listas. Cada sublista contiene los datos de los eventos en el siguiente orden:
            // idEvento, titulo, fecha, hora
            return eventosCategorias;
		}
		
        /*
         Consulta toda la información de un evento en específico.
         */
		[WebMethod]
        public List<string> InformacionEvento(int idEvento)
        {
			MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<string> informacionEvento = new List<string>();

            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("InformacionEvento", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("idEvento", idEvento));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                if (rdr.Read())
                {
                    string categoria = (string)rdr[0].ToString();
                    string titulo = (string)rdr[1].ToString();
                    string lugar = (string)rdr[2].ToString();
                    string fecha = (string)rdr[3].ToString();
                    string hora = (string)rdr[4].ToString();
                    string TotalCupo = (string)rdr[5].ToString();
                    string CupoDisponible = (string)rdr[6].ToString();
                    string Precio = (string)rdr[7].ToString();

                    Console.WriteLine(categoria + " " + titulo + " " + lugar + " " + fecha + " " + hora + " " + TotalCupo + " "+
                        CupoDisponible + " "+Precio);

                    informacionEvento.Add(categoria);
                    informacionEvento.Add(titulo);
                    informacionEvento.Add(lugar);
                    informacionEvento.Add(fecha);
                    informacionEvento.Add(hora);
                    informacionEvento.Add(TotalCupo);
                    informacionEvento.Add(CupoDisponible);
                    informacionEvento.Add(Precio);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista que contiene los datos del evento en el siguiente orden:
            // categoria, titulo, lugar, fecha, hora, totalCupo, cupoDisponible, precio
            return informacionEvento;
		}
		
        /*
         Registra una reservación de algún evento.
         */
		[WebMethod]
        public List<string> IngresarReservacion(int cantidad, int idUsuario, int idEvento)
        {
			MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<string> respuesta = new List<string>();
            

            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("IngresarReservacion", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("cantidad", cantidad));
                command.Parameters.Add(new MySqlParameter("idUsuario", idUsuario));
                command.Parameters.Add(new MySqlParameter("idEvento", idEvento));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                if (rdr.Read())
                {
                    
                    string confirmacion = (string)rdr[0].ToString();

                    Console.WriteLine(confirmacion);

                    respuesta.Add(confirmacion);

                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                //rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista con un 0 o -1, donde
            // 0 indica que se realizó la reservación correctamente
            // -1 indica que no se realizó la reservación por falta de cupo
            return respuesta;
		}
		
        /*
         Consulta todas las reservaciones realizadas.
         */
		[WebMethod]
        public List<List<string>> MostrarReservaciones()
        {
			MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<List<string>> reservaciones = new List<List<string>>();

            try
            {

                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("MostrarReservaciones", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //command.Parameters.Add(new MySqlParameter("idCategoria",1));
                

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    List<string> DatosReservaciones = new List<string>();
                    string cantidad = (string)rdr[0].ToString();
                    string fechaHora = (string)rdr[1].ToString();
                    string estado = (string)rdr[2].ToString();
                    string total = (string)rdr[3].ToString();
                    string usuario = (string)rdr[4].ToString();

                    Console.WriteLine(usuario+" "+cantidad+" "+estado+" "+total+" "+fechaHora);

                    DatosReservaciones.Add(cantidad);
                    DatosReservaciones.Add(fechaHora);
                    DatosReservaciones.Add(estado);
                    DatosReservaciones.Add(total);
                    DatosReservaciones.Add(usuario);

                    reservaciones.Add(DatosReservaciones);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista de listas. Cada sublista contiene los datos de las reservaciones en el siguiente orden:
            // cantidad, fecha y hora, estado, monto total, nombre del usuario
            return reservaciones;
		}
		
        /*
         Consulta todas las reservaciones de un usuario en específico.
         */
		[WebMethod]
        public List<List<string>> MostrarReservacionesUsuario(int idUsuario)
        {
			MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<List<string>> reservacionesUsuario = new List<List<string>>();
            

            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("ReservacionesUsuario", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("idUsuario", idUsuario));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    List<string> DatosReservacion = new List<string>();
                    string cantidad = (string)rdr[0].ToString();
                    string fechaHora = (string)rdr[1].ToString();
                    string estado = (string)rdr[2].ToString();
                    string total = (string)rdr[3].ToString();
                    string usuario = (string)rdr[4].ToString();

                    Console.WriteLine(cantidad+" "+fechaHora+" "+estado+" "+total+" "+usuario);

                    DatosReservacion.Add(cantidad);
                    DatosReservacion.Add(fechaHora);
                    DatosReservacion.Add(estado);
                    DatosReservacion.Add(total);
                    DatosReservacion.Add(usuario);

                    reservacionesUsuario.Add(DatosReservacion);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista de listas. Cada sublista contiene los datos de las reservaciones en el siguiente orden:
            // cantidad, fecha y hora, estado, monto total, nombre del usuario
            return reservacionesUsuario;
		}

        /*
        Consulta todos los comentarios realizados para un evento.
        */
        [WebMethod]
        public List<List<string>> MostrarComentariosEvento(int idEvento)
        {
            MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<List<string>> comentarios = new List<List<string>>();


            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("MostrarComentariosEvento", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("idEven", idEvento));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    List<string> DatosComentario = new List<string>();
                    string descripcion = (string)rdr[0].ToString();
                    string fechaHora = (string)rdr[1].ToString();
                    string nombre = (string)rdr[2].ToString();
                    string apellidos = (string)rdr[3].ToString();

                    Console.WriteLine(descripcion + " " + fechaHora + " " + nombre + " " + apellidos);

                    DatosComentario.Add(descripcion);
                    DatosComentario.Add(fechaHora);
                    DatosComentario.Add(nombre);
                    DatosComentario.Add(apellidos);

                    comentarios.Add(DatosComentario);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista de listas. Cada sublista contiene los datos del comentario del evento en el siguiente orden:
            // descripcion (contenido), fecha y hora, nombre, apellidos
            return comentarios;
        }

        /*
        Consulta todos los eventos favoritos de un usuario.
        */
        [WebMethod]
        public List<string> MostrarFavoritos(int idUsuario)
        {
            MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<string> FavoritosUsuario = new List<string>();


            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("MostrarFavoritos", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("idUser", idUsuario));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    string titulo = (string)rdr[0].ToString();

                    Console.WriteLine(titulo);

                    FavoritosUsuario.Add(titulo);
                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            // Retorna una lista que contiene los titulos de los eventos favoritos de un usuario.
            return FavoritosUsuario;
        }

        /*
        Registra un comentario de un evento.
        */
        [WebMethod]
        public List<string> IngresarComentario(string descripcion, int idUsuario, int idEvento)
        {
            MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<string> respuesta = new List<string>();


            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("AgregarComentario", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("descrip", descripcion));
                command.Parameters.Add(new MySqlParameter("idUser", idUsuario));
                command.Parameters.Add(new MySqlParameter("idEven", idEvento));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                if (rdr.Read())
                {

                    string confirmacion = (string)rdr[0].ToString();

                    Console.WriteLine(confirmacion);

                    respuesta.Add(confirmacion);

                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                //rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            return respuesta;
        }

        /*
        Registra un evento favorito de un usuario.
        */
        [WebMethod]
        public List<string> IngresarFavorito(int idUsuario, int idEvento)
        {
            MySqlConnection Connection = new MySqlConnection("Server=127.0.0.1;Database=fia;User id=root;Pwd=ilenia");

            MySqlCommand command;

            List<string> respuesta = new List<string>();


            try
            {
                Connection.Open();
                Console.WriteLine("Conectado");

                Console.Read();
                command = new MySqlCommand("AgregarFavorito", Connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("idUser", idUsuario));
                command.Parameters.Add(new MySqlParameter("idEven", idEvento));
                command.ExecuteNonQuery();

                MySqlDataReader rdr;
                rdr = command.ExecuteReader();
                if (rdr.Read())
                {

                    string confirmacion = (string)rdr[0].ToString();

                    Console.WriteLine(confirmacion);

                    respuesta.Add(confirmacion);

                }

                Connection.Close();
                Console.WriteLine("Cerro conexion");
                //rdr.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No conectado" + ex);
            }
            return respuesta;
        }

    }
}