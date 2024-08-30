using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {
        // Método que obtiene todos los registros de la tabla "Customers" de la base de datos
        public List<customers> ObtenerTodos()
        {
            // Se establece una conexión a la base de datos utilizando el método GetSql()
            using (var conexion = DataBase.GetSql())
            {
                // Construcción de la consulta SQL para obtener todas las columnas de la tabla "Customers"
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT " + "\n";
                selectFrom = selectFrom + "      [CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Se ejecuta el comando SQL a través de SqlCommand
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    // Se ejecuta la consulta y se almacena el resultado en SqlDataReader
                    SqlDataReader reader = comando.ExecuteReader();

                    // Lista para almacenar todos los registros de la tabla "Customers"
                    List<customers> Customers = new List<customers>();

                    // Se recorre el SqlDataReader para leer cada fila del resultado
                    while (reader.Read())
                    {
                        // Se convierte cada fila en un objeto "customers" y se agrega a la lista
                        var customers = LeerDelDataReader(reader);
                        Customers.Add(customers);
                    }
                    return Customers; // Se retorna la lista completa de clientes
                }
            }
        }

        // Método que obtiene un cliente específico basado en su ID
        public customers ObtenerPorID(string id)
        {
            // Se establece una conexión a la base de datos utilizando el método GetSql()
            using (var conexion = DataBase.GetSql())
            {
                // Construcción de la consulta SQL para obtener un cliente específico por su ID
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + $"  WHERE CustomerID = @customerId";

                // Se ejecuta el comando SQL a través de SqlCommand, con un parámetro para el ID del cliente
                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    // Se añade el parámetro del ID del cliente a la consulta
                    comando.Parameters.AddWithValue("customerId", id);

                    // Se ejecuta la consulta y se almacena el resultado en SqlDataReader
                    var reader = comando.ExecuteReader();

                    customers customers = null;

                    // Valida si hay resultados y asigna el valor del cliente encontrado
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader); // Convierte la fila en un objeto "customers"
                    }
                    return customers; // Retorna el cliente encontrado
                }
            }
        }

        // Método que mapea los datos de una fila obtenida de SqlDataReader a un objeto "customers"
        public customers LeerDelDataReader(SqlDataReader reader)
        {
            // Se crea un nuevo objeto "customers" y se asignan los valores leídos desde SqlDataReader
            customers customers = new customers();
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

            return customers; // Retorna el objeto con los valores del cliente
        }
    }
}
