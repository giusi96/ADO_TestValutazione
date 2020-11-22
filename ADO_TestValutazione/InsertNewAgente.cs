using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_TestValutazione
{
    public class InsertNewAgente
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security =true; Initial Catalog=Polizia; Server = WINAPPM2JMGI3WG\SQLEXPRESS; ";

        public static void ConnectedNewAgente()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Inserisci i dati del nuovo agente: ");
                Console.WriteLine("Nome: ");
                string nome;
                nome = Console.ReadLine();
                Console.WriteLine("Cognome: ");
                string cognome;
                cognome = Console.ReadLine();
                Console.WriteLine("Codice fiscale: ");
                string codfiscale;
                codfiscale = Console.ReadLine();
                if (codfiscale.Length > 16)
                {
                    codfiscale= codfiscale.Remove(16);
                }
                Console.WriteLine("Data di nascita: ");
                string n;
                DateTime nascita;
                n = Console.ReadLine();
                nascita = DateTime.Parse(n);
                Console.WriteLine("Anni di servizio: ");
                string a;              
                a = Console.ReadLine();
                int servizio;
                servizio = Convert.ToInt32(a);


                //Aprire la connessione
                connection.Open();

                //Creare un command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "insert into Agente values(@Nome, @Cognome, @Codicefiscale, @Nascita, @Servizio)";

                //altro command
                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandType = System.Data.CommandType.Text;
                command1.CommandText = "select * from Agente";

                //creazione parametri
                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Cognome", cognome);
                command.Parameters.AddWithValue("@Codicefiscale", codfiscale);
                command.Parameters.AddWithValue("@Nascita", nascita);
                command.Parameters.AddWithValue("@Servizio", servizio);

                //Eseguire il command
                command.ExecuteNonQuery();
                SqlDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    Console.WriteLine("{0} - {1} {2} {3} {4} {5}", reader1["ID"], reader1["Nome"], reader1["Cognome"], reader1["CodFiscale"], reader1["Nascita"], reader1["Servizio"]);
                }

                //chiusura connessione
                reader1.Close();
                connection.Close();


            }
        }
    }
}
