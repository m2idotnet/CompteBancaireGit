using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CompteBancaireAdo.Classes
{
    public class Compte
    {
        private int id;
        private int clientId;
        private string numeroCompte;
        private decimal solde;

        public int Id { get => id; set => id = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public string NumeroCompte { get => numeroCompte; set => numeroCompte = value; }
        public decimal Solde { get => solde; set => solde = value; }

        public Compte()
        {
            NumeroCompte = Guid.NewGuid().ToString();
            Solde = 0;
        }

        public Compte(string numero)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Compte  WHERE Numero = @n", Connection.Instance);
            command.Parameters.Add(new SqlParameter("@n", numero));
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

        public void Add()
        {
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
            SqlCommand command = new SqlCommand("UPDATE Compte set Solde = @s WHERE Id = @i", Connection.Instance);
            command.Parameters.Add(new SqlParameter("@i", Id));
            command.Parameters.Add(new SqlParameter("@s", Solde));
            Connection.Instance.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            Connection.Instance.Close();
        }
    }
}
