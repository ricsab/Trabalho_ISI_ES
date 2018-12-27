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
        #region Cp

        #endregion

        #region Utilizador

        /// <summary>
        /// Servico que retorna as informacoes de um utilizador
        /// </summary>
        /// <param name="numero_cc"> numero do cartao de cidadao </param>
        /// <returns> informacoes desse utilizador sob o formato de uma struct </returns>
        public Utilizador GetUser(int numero_cc)
        {
            Utilizador user = new Utilizador();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Servicos_Web.Properties.Settings.Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarPessoa = @"SELECT *FROM Pessoa WHERE Numero_cartao_cidadao=@CC";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            data = selpess.ExecuteReader();
            while (data.Read())
            {
                user.name = (string)data["Nome"];
                user.password= (string)data["Password"];
                user.numero_cc= (int)data["Numero_cartao_cidadao"];
                user.contribuinte= (int)data["Contribuinte"];
                user.data_Nascimento= (string)data["Data_Nascimento"]; ;
                user.Morada_id = (int)data["MoradaID_Morada"];
            }
            data.Close();
            con.Close();
            return user;
        }

        /// <summary>
        /// Servico que inserir um utilizador
        /// </summary>
        /// <param name="name">Nome do utilizador</param>
        /// <param name="password">password do utilizador</param>
        /// <param name="contribuinte"> numero de contribuinte do utilizador </param>
        /// <param name="data_Nascimento">data de nascimento do utilizador</param>
        /// <param name="numero_cc"> numero do cartao de cidadao do utilizador</param>
        /// <param name="rua"> rua onde mora o utilizador </param>
        /// <param name="num_porta">numero da porta do utilizador </param>
        /// <param name="cp"> codigo postal </param>
        /// <returns> se foi ou nao possivel inserir o utilizador </returns>
        public bool RegistUser(string name, string password, int contribuinte,string data_Nascimento ,int numero_cc, string rua, int num_porta, string cp)
        {
            Morada h;
            SqlDataReader data;
            h = new Morada();
            string cs = ConfigurationManager.ConnectionStrings["Servicos_Web.Properties.Settings.Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            #region CP
            Regex rx = new Regex(@"/^\d{ 4 } -\d{ 3}?$/");
            if (rx.IsMatch(cp)==false)
            {
                con.Close();
                return false;
            }
            string CompareCP = @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.Int).Value = cp;
            data = compCp.ExecuteReader();

            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            #endregion

            #region Morada

            string comparemorada = @"Select ID_Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CPCP_ID=@CP ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
            compM.Parameters.Add("@CP", SqlDbType.Int).Value = cp;
            data = compM.ExecuteReader();
            if (data.HasRows==false)
            {
                data.Close();
                string insertmorada = @"insert into Morada(Rua,N_Porta,CPCP_ID) values(@Rua,@N_Porta,@CP_ID)";
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

            else
            {
                //preenche o objeto h com as informações da morada solicitada
                while (data.Read())
                {
                    h.Morada_id = (string)data["ID_Morada"];
                    h.num_porta = (int)data["N_Porta"];
                    h.rua = (string)data["Rua"];
                    h.cp = (string)data["CPCP_ID"];
                }
                data.Close();
            }
            
            #endregion

            #region Pessoa
            string insertPessoa = @"insert into Pessoa(Nome,Password,Contribuinte,Numero_cartao_cidadao,Data_Nascimento,MoradaID_Morada) values(@Nome,@Password,@CC,@DataNascimento,@Morada)";
            SqlCommand inpess = new SqlCommand(insertPessoa, con);
            inpess.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = name;
            inpess.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            inpess.Parameters.Add("@Contribuinte", SqlDbType.VarChar).Value = contribuinte;
            inpess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            inpess.Parameters.Add("@DataNascimento", SqlDbType.VarChar).Value = data_Nascimento;
            inpess.Parameters.Add("@Morada", SqlDbType.Int).Value =h.Morada_id;

            if (inpess.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            #endregion

            con.Close();
            return true;
        }

        /// <summary>
        /// serviço que permite remover um utilizador
        /// </summary>
        /// <param name="numero_cc"> numero de cartao de cidadao</param>
        /// <returns> se foi ou nao possivel remover o utilizador</returns>
        public bool DeleteUser(int numero_cc)
        {
            string cs = ConfigurationManager.ConnectionStrings["Servicos_Web.Properties.Settings.Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string DeletePessoa = @"DELETE FROM Pessoa WHERE Numero_cartao_cidadao=@CC";
            SqlCommand delpess = new SqlCommand(DeletePessoa, con);
            delpess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            if ()
            {
                con.Close();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Servico que atualiza a informacao de um utilizador
        /// </summary>
        /// <param name="name">Nome do utilizador</param>
        /// <param name="password">password do utilizador</param>
        /// <param name="contribuinte"> numero de contribuinte do utilizador </param>
        /// <param name="data_Nascimento">data de nascimento do utilizador</param>
        /// <param name="numero_cc"> numero do cartao de cidadao do utilizador</param>
        /// <param name="rua"> rua onde mora o utilizador </param>
        /// <param name="num_porta">numero da porta do utilizador </param>
        /// <param name="cp"> codigo postal </param>
        /// <returns> se foi ou nao possivel atualizar o utilizador </returns>
        public bool UpdateUser(string name, string password, int contribuinte,string data_Nascimento,int numero_cc, string rua, int num_porta, string cp)
        {
            Morada h = new Morada();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Servicos_Web.Properties.Settings.Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            #region CP
            Regex rx = new Regex(@"/^\d{ 4 } -\d{ 3}?$/");
            if (rx.IsMatch(cp) == false)
            {
                con.Close();
                return false;
            }
            string CompareCP = @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.Int).Value = cp;
            data = compCp.ExecuteReader();

            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            #endregion

            #region Morada

            string comparemorada = @"Select ID_Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CPCP_ID=@CP ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
            compM.Parameters.Add("@CP", SqlDbType.Int).Value = cp;
            data = compM.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                string insertmorada = @"insert into Morada(Rua,N_Porta,CPCP_ID) values(@Rua,@N_Porta,@CP_ID)";
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

            else
            {
                //preenche o objeto h com as informações da morada solicitada
                while (data.Read())
                {
                    h.Morada_id = (int)data["ID_Morada"];
                    h.num_porta = (int)data["N_Porta"];
                    h.rua = (string)data["Rua"];
                    h.cp = (string)data["CPCP_ID"];
                }
                data.Close();
            }

            #endregion

            #region Pessoa

            string UpdatePessoa = @"Update Pessoa SET Nome=@Nome,Password=@Password,Data_Nascimento=@DataNascimento,MoradaID_Morada=@Morada where Numero_cartao_cidadao=@CC";
            SqlCommand uppess = new SqlCommand(UpdatePessoa, con);
            uppess.Parameters.AddWithValue("@Nome", SqlDbType.VarChar).Value = name;
            uppess.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            uppess.Parameters.Add("@Contribuinte", SqlDbType.VarChar).Value = contribuinte;
            uppess.Parameters.Add("@ID", SqlDbType.Int).Value = contribuinte;
            uppess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            uppess.Parameters.Add("@DataNascimento", SqlDbType.VarChar).Value = data_Nascimento;
            uppess.Parameters.Add("@Morada", SqlDbType.Int).Value = h.Morada_id;

            if (uppess.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            #endregion

            con.Close();
            return true;
        }

        #endregion

        #region Nao Residente
        #endregion

        #region Residente
        #endregion

        #region Funcionario
        #endregion

        #region Administrador
        #endregion

        #region Servicos
        #endregion

        #region Servicos Gerais
        #endregion

        #region Noticias
        #endregion

        #region Noticias Residente
        #endregion

        #region Noticias Nao Residente
        #endregion
    }

    /// <summary>
    /// Struct para conter a informação de uma morada
    /// </summary>
    public struct Morada
    {
        public int Morada_id;
        public string rua;
        public int num_porta;
        public string cp;
    }

    /// <summary>
    /// Struct para conter a informação de uma Utilizador
    /// </summary>
    public struct Utilizador
    {
        public string name;
        public string password;
        public int contribuinte;
        public string data_Nascimento;
        public int numero_cc;
        public int Morada_id;
    }
}
