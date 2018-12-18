using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Servicos_Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public bool GetDatalogin(string name, string password)
        {
            return false;
        }


        public bool RegistUser(string name, string password, int contribuinte,DateTimeFormat data_Nascimento ,int numero_cc, string rua, int num_porta, int cp)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = cs;
            con.Open();

            string CompareCP= @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.Int).Value = cp;
            if (compCp.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }

            string comparemorada= @"Select ID Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CP_ID=@CP_ID ";

            SqlCommand compM = new SqlCommand(comparemorada, con);

            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
            compM.Parameters.Add("@CP_ID", SqlDbType.Int).Value = cp;

            SqlDataReader data = compM.ExecuteReader();

            if (data==null)
            {
                string insertmorada = @"insert into Morada(Rua,N_Porta,CP_ID) values(@Rua,@N_Porta,@CP_ID)";

                SqlCommand inmor = new SqlCommand(insertmorada, con);

                inmor.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
                inmor.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
                inmor.Parameters.Add("@CP_ID", SqlDbType.Int).Value = cp;
                
                if (inmor.ExecuteNonQuery() == 0)
                {
                    con.Close();
                    return false;
                }
            }
            

            string insertPessoa = @"insert into Pessoa(Nome,Password,ID,CC,DataNascimento,Morada) values(@Nome,@Password,@ID,@CC,@DataNascimento,@Morada)";

            SqlCommand inpess = new SqlCommand(insertPessoa, con);

            inpess.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = name;
            inpess.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            inpess.Parameters.Add("@ID", SqlDbType.Int).Value = contribuinte;
            inpess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            inpess.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = data_Nascimento;
            inpess.Parameters.Add("@Morada", SqlDbType.Int).Value =(int)data["ID Morada"] ;


            if (inpess.ExecuteNonQuery() != 0) { con.Close(); return true; }
            else return false;
        }


        public bool DeleteUser(int numero_cc)
        {
            return false;
        }


        public bool UpdateUser(string name, string password, int contribuinte, int numero_cc, string rua, int num_porta, int cp)
        {
            return false;
        }
    }
}
