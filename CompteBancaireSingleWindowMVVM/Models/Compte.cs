using CompteBancaireSingleWindowMVVM.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompteBancaireSingleWindowMVVM.Models
{
    public class Compte
    {
        private int id;
        private int clientId;
        private string numeroCompte;
        private decimal solde;
        private object _lock = new object();
        private Mutex m = new Mutex();

        public int Id { get => id; set => id = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public string NumeroCompte { get => numeroCompte; set => numeroCompte = value; }
        public decimal Solde { get => solde; set => solde = value; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Compte()
        {
            NumeroCompte = Guid.NewGuid().ToString();
            Solde = 0;
        }

        public Compte(string numero)
        {
            Task t = Task.Run(() =>
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Compte  WHERE Numero = @n", Connection.Instance);
                command.Parameters.Add(new SqlParameter("@n", numero));
                lock (_lock)
                {
                    Connection.Instance.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        NumeroCompte = reader.GetString(1);
                        Solde = reader.GetDecimal(2);
                        ClientId = reader.GetInt32(3);
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
            //Task t = Task.Run(() =>
            //{
            //    lock (_lock)
            //    {
            //        //m.WaitOne();
            //        SqlCommand command = new SqlCommand("INSERT INTO Compte (Numero, Solde, ClientId) OUTPUT INSERTED.ID VALUES(@n,@s,@c)", Connection.Instance);
            //        command.Parameters.Add(new SqlParameter("@n", NumeroCompte));
            //        command.Parameters.Add(new SqlParameter("@s", Solde));
            //        command.Parameters.Add(new SqlParameter("@c", ClientId));
            //        Connection.Instance.Open();
            //        Id = (int)command.ExecuteScalar();
            //        command.Dispose();
            //        Connection.Instance.Close();
            //        //m.ReleaseMutex();
            //    }
            //});
            //t.Wait();
            SqlCommand command = new SqlCommand("INSERT INTO Compte (Numero, Solde, ClientId) OUTPUT INSERTED.ID VALUES(@n,@s,@c)", Connection.Instance);
            command.Parameters.Add(new SqlParameter("@n", NumeroCompte));
            command.Parameters.Add(new SqlParameter("@s", Solde));
            command.Parameters.Add(new SqlParameter("@c", ClientId));
            Connection.Instance.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            Connection.Instance.Close();
        }

        public void Update()
        {
            //Task t = Task.Run(() =>
            //{
            //    if (Id > 0)
            //    {
            //        SqlCommand command = new SqlCommand("UPDATE Compte set Solde = @s WHERE Id = @i", Connection.Instance);
            //        command.Parameters.Add(new SqlParameter("@i", Id));
            //        command.Parameters.Add(new SqlParameter("@s", Solde));
            //        lock (_lock)
            //        {
            //            Connection.Instance.Open();
            //            command.ExecuteNonQuery();
            //            command.Dispose();
            //            Connection.Instance.Close();
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception("Pas d'id");
            //    }

            //});
            //t.Wait();
            if (Id > 0)
            {
                SqlCommand command = new SqlCommand("UPDATE Compte set Solde = @s WHERE Id = @i", Connection.Instance);
                command.Parameters.Add(new SqlParameter("@i", Id));
                command.Parameters.Add(new SqlParameter("@s", Solde));
                lock (_lock)
                {
                    Connection.Instance.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    Connection.Instance.Close();
                }
            }
            else
            {
                throw new Exception("Pas d'id");
            }
        }

        public static ObservableCollection<Compte> GetComptes()
        {
            ObservableCollection<Compte> list = new ObservableCollection<Compte>();
            SqlCommand command = new SqlCommand("SELECT cl.Nom, cl.Prenom, c.Id, c.Numero, c.Solde from Compte as c inner join Client as cl on cl.Id = c.ClientId", Connection.Instance);
            Connection.Instance.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Compte c = new Compte
                {
                    Id = reader.GetInt32(2),
                    Nom = reader.GetString(0),
                    Prenom = reader.GetString(1),
                    Solde = reader.GetDecimal(4),
                    NumeroCompte = reader.GetString(3)
                };
                list.Add(c);
            }
            reader.Close();
            command.Dispose();
            Connection.Instance.Close();
            return list;
        }
    }
}
