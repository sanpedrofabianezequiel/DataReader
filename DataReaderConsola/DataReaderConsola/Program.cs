using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;    //Importar esta Clase

namespace DataReaderConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            int valorPrecio = 5;        //Variable

            string connectionString =   //Coneccion con la base de datos
                "Data Source=DESKTOP-5ILIF80;Initial Catalog=NORTHWND; Integrated Security=true";

            string queryString =        //Query String  //Consulta con un PARAMETRO para Filtrar
                "SELECT ProductID,UnitPrice,ProductName " +
                "FROM DBO.Products " +
                "WHERE UnitPrice > @Precio " +
                "ORDER BY UnitPrice DESC;";


            using (SqlConnection connection = new SqlConnection(connectionString))//Crear y abrir la conexion
            {   //Instancion un SqlConnection y le envio por Parametros la connectionString

                //Crear el Cursor/Command
                //Se le envia por parametro la QUERY + la Connection instanciada
                SqlCommand command = new SqlCommand(queryString, connection);

                // Setiamos el parametro con el valor de una variable(el obj)
                command.Parameters.AddWithValue("@Precio", valorPrecio);
                



                try//Se abre la conexion con un try y catch
                {
                    connection.Open();

                    //Creamos un objeto de tipo DataReader que ejecute command. executeReader()
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())    //Mientras sea true Read
                    {
                        Console.WriteLine(" {0} , {1} , {2}", //Lee las columnas
                                            reader[0], reader[1], reader[2]);
                    }

                    reader.Close(); //Cierro la coneccion
                    Console.ReadKey();  //Para que no se cierre el Programa

                }
                catch (Exception exp)        //Mensaje de error
                {
                    Console.WriteLine(exp.Message);
                }
                Console.ReadLine();



            }
        }
    }
}
