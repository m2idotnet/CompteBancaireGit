using CompteBancaireAdo.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CompteBancaireAdoNetDeconnecte.Classes
{
    public class DataBase
    {
        private static DataSet _instance = null;
        private static object _lock = new object();
        private static SqlDataAdapter adapterClient;
        private static SqlDataAdapter adapterCompte ;
        private static SqlDataAdapter adapterOperation;
        public static DataSet Instance
        {
            get
            {
                lock(_lock)
                {
                    if (_instance == null)
                        _instance = new DataSet();
                    return _instance;
                }
            }
        }

        private DataBase()
        {

        }
        public static void Init()
        {
            adapterOperation = new SqlDataAdapter("SELECT * FROM Operation", Connection.Instance);
            adapterCompte = new SqlDataAdapter("SELECT * FROM Compte", Connection.Instance);
            adapterClient = new SqlDataAdapter("SELECT * FROM Client", Connection.Instance);
           
            Connection.Instance.Open();
            adapterClient.Fill(Instance, "Client");
            //adapterClient.Dispose();
            adapterCompte.Fill(Instance, "Compte");
            //adapterCompte.Dispose();
            adapterOperation.Fill(Instance, "Operation");
            //adapterOperation.Dispose();
            Connection.Instance.Close();
        }

        public static void Update()
        {
            SqlCommand commandInsert = new SqlCommand("INSERT INTO Client (Nom, Prenom, Tel) VALUES (@n,@p,@t)", Connection.Instance);
            commandInsert.Parameters.Add("@n", SqlDbType.VarChar, 100, "Nom");
            commandInsert.Parameters.Add("@p", SqlDbType.VarChar, 100, "Prenom");
            commandInsert.Parameters.Add("@t", SqlDbType.VarChar, 10, "Tel");
            adapterClient.InsertCommand = commandInsert;
            SqlCommand commandInsertCompte = new SqlCommand("INSERT INTO Compte(Numero, Solde, ClientId) VALUES(@n,@s,@c)", Connection.Instance);
            commandInsertCompte.Parameters.Add("@n", SqlDbType.VarChar, 100, "Numero");
            commandInsertCompte.Parameters.Add("@s", SqlDbType.Decimal, 100, "Solde");
            commandInsertCompte.Parameters.Add("@c", SqlDbType.Int, 11, "ClientId");
            adapterCompte.InsertCommand = commandInsertCompte;
            SqlCommand commandUpdateCompte = new SqlCommand("UPDATE Compte set Solde = @s WHERE Id = @i", Connection.Instance);
            commandUpdateCompte.Parameters.Add("@s", SqlDbType.Decimal, 100, "Solde");
            commandUpdateCompte.Parameters.Add("@i", SqlDbType.Int, 11, "Id");
            adapterCompte.UpdateCommand = commandUpdateCompte;
            SqlCommand commandInsertOperation = new SqlCommand("INSERT INTO Operation (CompteId, Montant, DateOperation) Values(@c,@m,@d)", Connection.Instance);
            commandInsertOperation.Parameters.Add("@c", SqlDbType.Int, 11, "CompteId");
            commandInsertOperation.Parameters.Add("@m", SqlDbType.Decimal, 11, "Montant");
            commandInsertOperation.Parameters.Add("@d", SqlDbType.DateTime, 11, "DateOperation");
            adapterOperation.InsertCommand = commandInsertOperation;
            Connection.Instance.Open();
            adapterClient.Update(Instance.Tables["Client"]);
            //adapterClient.Dispose();
            adapterCompte.Update(Instance.Tables["Compte"]);
            //adapterCompte.Dispose();
            adapterOperation.Update(Instance.Tables["Operation"]);
            //adapterOperation.Dispose();
            Connection.Instance.Close();
        }
    }
}
