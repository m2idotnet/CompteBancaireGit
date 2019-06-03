using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaireWpf.Classes
{
    public class Client
    {
        private int id;
        private string nom;
        private string prenom;
        private string tel;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Tel { get => tel; set => tel = value; }
        private object _lock = new object();

        public Client()
        {

        }
        public Client(string n, string p, string t)
        {
            Nom = n;
            Prenom = p;
            Tel = t;
        }

        public Client(int id)
        {
            Task t = Task.Run(() =>
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Client  WHERE Id = @n", Connection.Instance);
                command.Parameters.Add(new SqlParameter("@n", id));
                lock (_lock)
                {
                    Connection.Instance.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        Nom = reader.GetString(1);
                        Prenom = reader.GetString(2);
                        Tel = reader.GetString(3);
                    }
                    reader.Close();
                    command.Dispose();
                    Connection.Instance.Close();
                }
            });
            t.Wait();
        }

        public void Add()
        {
            lock (_lock)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Client (Nom, Prenom, Tel) OUTPUT INSERTED.ID VALUES(@n,@p,@t)", Connection.Instance);
                command.Parameters.Add(new SqlParameter("@n", Nom));
                command.Parameters.Add(new SqlParameter("@p", Prenom));
                command.Parameters.Add(new SqlParameter("@t", Tel));
                Connection.Instance.Open();
                Id = (int)command.ExecuteScalar();
                command.Dispose();
                Connection.Instance.Close();
            }

        }


        public override string ToString()
        {
            return "Nom : " + Nom + " Prénom : " + Prenom + " Tel : " + Tel;
        }
    }
}