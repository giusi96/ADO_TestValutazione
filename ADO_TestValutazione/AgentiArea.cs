using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_TestValutazione
{
    public class AgentiArea
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security =true; Initial Catalog=Polizia; Server = WINAPPM2JMGI3WG\SQLEXPRESS; ";

        public static void ConnectedAgentiArea()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // inserimento del valore del parametro da riga di comando: 
                Console.WriteLine("Inserisci il codice dell'area: ");
                string Codice;
                Codice = Console.ReadLine();

                //Aprire la connessione
                connection.Open();

                //Creare un command
                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "Select agente.Nome, agente.Cognome, agente.CodFiscale, agente.Nascita, agente.Servizio from" +
                    "(select * from(SELECT * FROM AreaMetropolitana WHERE Codice=@Codice) as q inner join Ponte on q.Id=Ponte.AreaID) as c inner join Agente on c.AgenteID=Agente.ID ";
  

                //creazione parametro
                SqlParameter codiceParam = new SqlParameter();
                codiceParam.ParameterName = "@Codice";
                codiceParam.Value = Codice;
                command1.Parameters.Add(codiceParam);

                SqlDataReader reader = command1.ExecuteReader();

                //Lettura dati
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3} {4}", reader["Nome"], reader["Cognome"], reader["CodFiscale"], reader["Nascita"], reader["Servizio"]);
                }

                //chiusura connessione
                reader.Close();
                connection.Close();
            }
        }
    }
}
