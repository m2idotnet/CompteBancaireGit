using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaireWpf.Classes
{
    public class Operation
    {
        private int id;
        private int compteId;
        private DateTime dateOperation;
        private decimal montant;

        public int Id { get => id; set => id = value; }
        public int CompteId { get => compteId; set => compteId = value; }
        public DateTime DateOperation { get => dateOperation; set => dateOperation = value; }
        public decimal Montant { get => montant; set => montant = value; }

        public Operation()
        {

        }
        public Operation(decimal m, int c)
        {
            Montant = m;
            CompteId = c;
            DateOperation = DateTime.Now;
        }

        public void Add()
        {
            SqlCommand command = new SqlCommand("INSERT INTO Operation (CompteId, Montant, DateOperation) OUTPUT INSERTED.ID VALUES(@c,@m,@d)", Connection.Instance);
            command.Parameters.Add(new SqlParameter("@c", CompteId));
            command.Parameters.Add(new SqlParameter("@m", Montant));
            command.Parameters.Add(new SqlParameter("@d", DateOperation));
            Connection.Instance.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            Connection.Instance.Close();
        }

        public static List<Operation> GetOperations(int compte)
        {

            Task<List<Operation>> t = Task.Run(new Func<List<Operation>>(() =>
            {
                List<Operation> liste = new List<Operation>();
                SqlCommand command = new SqlCommand("SELECT * FROM Operation  WHERE CompteId = @n", Connection.Instance);
                command.Parameters.Add(new SqlParameter("@n", compte));
                lock (new object())
                {
                    Connection.Instance.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Operation o = new Operation()
                        {
                            Id = reader.GetInt32(0),
                            CompteId = reader.GetInt32(1),
                            Montant = reader.GetDecimal(2),
                            DateOperation = reader.GetDateTime(3)
                        };
                        liste.Add(o);
                    }
                    reader.Close();
                    command.Dispose();
                    Connection.Instance.Close();
                    return liste;
                }

            }));

            return t.Result;
        }
        public override string ToString()
        {
            return "Date Opération : " + DateOperation + " Montant : " + Montant;
        }
    }
}
