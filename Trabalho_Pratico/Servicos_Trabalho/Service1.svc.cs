using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;


namespace Servicos_Trabalho
{



    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1Soap
    {
        #region Cp

        /// <summary>
        /// Servico que retorna as informacoes de um codigo postal
        /// </summary>
        /// <param name="cp">codigo postal</param>
        /// <returns> as infos desse codigo postal</returns>
        public CP GetCp(string cp)
        {
            CP codigo = new CP();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarPessoa = @"SELECT *FROM CP WHERE CP_ID=@CP";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@CP", SqlDbType.Int).Value = cp;
            data = selpess.ExecuteReader();
            while (data.Read())
            {
                codigo.codigo_postal = cp;
                codigo.localidade = (string)data["Localidade"];
            }
            data.Close();
            con.Close();
            return codigo;

        }

        /// <summary>
        /// Servico que retorna as informacoes de todos os codigos postais
        /// </summary>
        /// <returns> as infos de todos os codigos postais</returns>
        public List<CP> GetAllCp()
        {
            List<CP> h = new List<CP>();
            CP codigo = new CP();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarCodigo = @"SELECT *FROM CP ";
            SqlCommand selcp = new SqlCommand(SelecionarCodigo, con);
            data = selcp.ExecuteReader();
            while (data.Read())
            {
                codigo.codigo_postal = (string)data["CP_ID"];
                codigo.localidade = (string)data["Localidade"];
                h.Add(codigo);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que permite adicionar um codigo postal a base de dados
        /// </summary>
        /// <param name="cp"> codigo postal</param>
        /// <param name="localidade"> localidade desse codigo postal</param>
        /// <returns> se foi ou nao possivel adicionar o codigo postatl</returns>
        public bool RegistCp(string cp, string localidade)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            //Regex rx = new Regex(@"/^[0-9]{4}-[0-9]{3}?$/");
            //if (rx.IsMatch(cp) == false)
            //{
            //    con.Close();
            //    return false;
            //}
            string CompareCP = @"Select * from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compCp.ExecuteReader();
            if (data.HasRows == true)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string insertCP = @"insert into CP(CP_ID,Localidade) values(@CP,@Localidade)";
            SqlCommand incp = new SqlCommand(insertCP, con);
            incp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            incp.Parameters.Add("@Localidade", SqlDbType.VarChar).Value = localidade;

            if (incp.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite eliminar um codigo postal
        /// </summary>
        /// <param name="cp"> respetivo codigo postal</param>
        /// <returns>se foi ou nao possivel remover o codigo postal</returns>
        public bool DeleteCp(string cp)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string DeletePessoa = @"DELETE FROM CP WHERE CP_ID=@CP";
            SqlCommand delpess = new SqlCommand(DeletePessoa, con);
            delpess.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;

            if (delpess.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite atualizar a localidade do codigo postal
        /// </summary>
        /// <param name="cp"> codigo postal </param>
        /// <param name="localidade">nova localidade do codigo postal</param>
        /// <returns>se foi ou nao possivel </returns>
        public bool UpdateCp(string cp, string localidade)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            //Regex rx = new Regex(@"/^\d{ 4 } -\d{ 3}?$/");
            //if (rx.IsMatch(cp) == false)
            //{
            //    con.Close();
            //    return false;
            //}
            string CompareCP = @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compCp.ExecuteReader();

            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();
            string UpdateCP = @"Update CP SET Localidade=@Localidade where CP_ID=@CP";
            SqlCommand upcp = new SqlCommand(UpdateCP, con);
            upcp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            upcp.Parameters.Add("@Localidade", SqlDbType.VarChar).Value = localidade;

            if (upcp.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        #endregion

        #region Morada

        /// <summary>
        /// servico que envias as infos de uma morada
        /// </summary>
        /// <param name="rua">rua da morada</param>
        /// <param name="num_porta">numero da porta da morada</param>
        /// <param name="cp">codigo postal da morada</param>
        /// <returns>morada tipada</returns>
        public Morada GetMorada(string rua, int num_porta, string cp)
        {
            Morada m = new Morada();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string CompareCP = @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compCp.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return m;
            }
            data.Close();

            string comparemorada = @"Select ID_Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CPCP_ID=@CP ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
            compM.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compM.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return m;
            }
            while (data.Read())
            {
                m.Morada_id = (int)data["ID_Morada"];
                m.num_porta = num_porta;
                m.rua = rua;
                m.cp = cp;
            }
            data.Close();
            con.Close();
            return m;
        }

        /// <summary>
        /// servico que retorna todas as moradas da base de dados
        /// </summary>
        /// <returns>lista com todas as moradas</returns>
        public List<Morada> GetAllMorada()
        {
            List<Morada> h = new List<Morada>();
            Morada m = new Morada();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string comparemorada = @"Select * from Morada ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            data = compM.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return h;
            }
            while (data.Read())
            {
                m.Morada_id = (int)data["ID_Morada"];
                m.num_porta = (int)data["N_Porta"];
                m.rua = (string)data["Rua"];
                m.cp = (string)data["CPCP_ID"];
                h.Add(m);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que permite adicionar uma morada a base de dados
        /// </summary>
        /// <param name="rua">rua dessa morada</param>
        /// <param name="num_porta">numero da porta dessa morada</param>
        /// <param name="cp">codigo postal</param>
        /// <returns>se foi ou nao possivel adicionar</returns>
        public bool RegistMorada(string rua, int num_porta, string cp)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            //Regex rx = new Regex(@"/^\d{ 4 } -\d{ 3}?$/");
            //if (rx.IsMatch(cp)==false)
            //{
            //    con.Close();
            //    return false;
            //}
            string CompareCP = @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compCp.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string comparemorada = @"Select ID_Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CPCP_ID=@CP ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
            compM.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compM.ExecuteReader();

            if (data.HasRows == false)
            {
                data.Close();
                string insertmorada = @"insert into Morada(Rua,N_Porta,CPCP_ID) values(@Rua,@N_Porta,@CP_ID)";
                SqlCommand inmor = new SqlCommand(insertmorada, con);
                inmor.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
                inmor.Parameters.Add("@N_Porta", SqlDbType.Int).Value = num_porta;
                inmor.Parameters.Add("@CP_ID", SqlDbType.VarChar).Value = cp;

                if (inmor.ExecuteNonQuery() == 0)
                {
                    con.Close();
                    return true;
                }
            }
            con.Close();
            return false;
        }

        /// <summary>
        /// servico que permite eliminar uma morada da base de dados 
        /// </summary>
        /// <param name="rua">rua da morada</param>
        /// <param name="num_porta">numero da porta da morada</param>
        /// <param name="cp">codigo postal</param>
        /// <returns> se foi ou nao possivel remover a morada</returns>
        public bool DeleteMorada(string rua, int num_porta, string cp)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string DeleteMorada = @"DELETE FROM Morada WHERE CPCP_ID=@CP AND Rua=@Rua AND N_Porta=@Num_Porta";
            SqlCommand delmor = new SqlCommand(DeleteMorada, con);
            delmor.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            delmor.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            delmor.Parameters.Add("@Num_Porta", SqlDbType.Int).Value = num_porta;

            if (delmor.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite atualizar as infos de uma morada
        /// </summary>
        /// <param name="rua"> nova rua dessa morada </param>
        /// <param name="num_porta">novo numero da porta dessa morada</param>
        /// <param name="cp">novo codigo postal dessa </param>
        /// <param name="m"> antigas informaçoes dessa morada</param>
        /// <returns>se foi ou nao possivel atualizar a morada</returns>
        public bool UpdateMorada(string rua, int num_porta, string cp, Morada m)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string CompareMorada = @"Select ID_Morada from Morada where CPCP_ID=@CP AND Rua=@Rua AND N_Porta=@Num_Porta";
            SqlCommand compMor = new SqlCommand(CompareMorada, con);
            compMor.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            compMor.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            compMor.Parameters.Add("@Num_Porta", SqlDbType.Int).Value = num_porta;
            data = compMor.ExecuteReader();
            if (data.HasRows == true)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();
            string CompareCP = @"Select CP_ID from cp where CP_ID=@CP";
            SqlCommand compCp = new SqlCommand(CompareCP, con);
            compCp.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            data = compCp.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string UpdateMorada = @"Update Morada SET CPCP_ID=@CP,Rua=@Rua,N_Porta=@Num_Porta where ID_Morada=@ID";
            SqlCommand upmor = new SqlCommand(UpdateMorada, con);
            upmor.Parameters.Add("@CP", SqlDbType.VarChar).Value = cp;
            upmor.Parameters.Add("@Rua", SqlDbType.VarChar).Value = rua;
            upmor.Parameters.Add("@Num_Porta", SqlDbType.Int).Value = num_porta;
            upmor.Parameters.Add("@ID", SqlDbType.Int).Value = m.Morada_id;
            if (upmor.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }



        #endregion

        #region Utilizador

        /// <summary>
        /// Servico que retorna as informacoes de todos pessoas
        /// </summary>
        /// <returns> informacoes de todas as pessoas sob o formato de uma struct </returns>
        public List<Pessoas> GetAllUsers()
        {
            List<Pessoas> h = new List<Pessoas>();
            Pessoas user = new Pessoas();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarPessoa = @"SELECT *FROM Pessoa ";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            data = selpess.ExecuteReader();
            while (data.Read())
            {
                user.ID = (int)data["ID_Pessoa"];
                user.name = (string)data["Nome"];
                user.password = (string)data["Password"];
                user.numero_cc = (int)data["Numero_cartao_cidadao"];
                user.contribuinte = (int)data["Contribuinte"];
                user.data_Nascimento = (string)data["Data_Nascimento"]; ;
                user.Morada_id = (int)data["MoradaID_Morada"];
                h.Add(user);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// Servico que retorna as informacoes de um utilizador
        /// </summary>
        /// <param name="numero_cc"> numero do cartao de cidadao </param>
        /// <returns> informacoes desse utilizador sob o formato de uma struct </returns>
        public Pessoas GetUser(int pessoa_id)
        {
            Pessoas user = new Pessoas();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarPessoa = @"SELECT *FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = pessoa_id;

            data = selpess.ExecuteReader();
            while (data.Read())
            {
                user.ID = pessoa_id;
                user.name = (string)data["Nome"];
                user.password = (string)data["Password"];
                user.numero_cc = (int)data["Numero_cartao_cidadao"];
                user.contribuinte = (int)data["Contribuinte"];
                user.data_Nascimento = (string)data["Data_Nascimento"]; ;
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
        public bool RegistUser(string name, string password, int contribuinte, string data_Nascimento, int numero_cc, Morada m)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            #region Morada
            string comparemorada = @"Select ID_Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CPCP_ID=@CP ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = m.rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = m.num_porta;
            compM.Parameters.Add("@CP", SqlDbType.VarChar).Value = m.cp;
            data = compM.ExecuteReader();
            if (data.HasRows == false)
            {
                con.Close();
                return false;
            }
            #endregion

            #region Pessoa

            string ComparePessoa = @"SELECT * FROM Pessoa WHERE Numero_cartao_cidadao=@CC";
            SqlCommand cmppess = new SqlCommand(ComparePessoa, con);
            cmppess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            data = cmppess.ExecuteReader();
            if (data.HasRows == true)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string insertPessoa = @"insert into Pessoa(Nome,Password,Contribuinte,Numero_cartao_cidadao,Data_Nascimento,MoradaID_Morada) values(@Nome,@Password,@CC,@DataNascimento,@Morada)";
            SqlCommand inpess = new SqlCommand(insertPessoa, con);
            inpess.Parameters.Add("@Nome", SqlDbType.VarChar).Value = name;
            inpess.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            inpess.Parameters.Add("@Contribuinte", SqlDbType.VarChar).Value = contribuinte;
            inpess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            inpess.Parameters.Add("@DataNascimento", SqlDbType.VarChar).Value = data_Nascimento;
            inpess.Parameters.Add("@Morada", SqlDbType.Int).Value = m.Morada_id;

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
        public bool DeleteUser(int pessoa_id)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string DeletePessoa = @"DELETE FROM Pessoa WHERE ID_Pessoa=@ID";
            SqlCommand delpess = new SqlCommand(DeletePessoa, con);
            delpess.Parameters.Add("@ID", SqlDbType.Int).Value = pessoa_id;

            if (delpess.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
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
        public bool UpdateUser(string name, string password, int contribuinte, string data_Nascimento, int numero_cc, Morada m)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            #region Morada
            string comparemorada = @"Select ID_Morada from Morada where Rua=@Rua AND N_Porta=@N_Porta AND CPCP_ID=@CP ";
            SqlCommand compM = new SqlCommand(comparemorada, con);
            compM.Parameters.Add("@Rua", SqlDbType.VarChar).Value = m.rua;
            compM.Parameters.Add("@N_Porta", SqlDbType.Int).Value = m.num_porta;
            compM.Parameters.Add("@CP", SqlDbType.VarChar).Value = m.cp;
            data = compM.ExecuteReader();
            if (data.HasRows == false)
            {
                con.Close();
                return false;
            }
            #endregion

            #region Pessoa

            string UpdatePessoa = @"Update Pessoa SET Nome=@Nome,Password=@Password,Contribuinte=@Contribuinte,Data_Nascimento=@DataNascimento,MoradaID_Morada=@Morada where Numero_cartao_cidadao=@CC";
            SqlCommand uppess = new SqlCommand(UpdatePessoa, con);
            uppess.Parameters.Add("@Nome", SqlDbType.VarChar).Value = name;
            uppess.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            uppess.Parameters.Add("@Contribuinte", SqlDbType.VarChar).Value = contribuinte;
            uppess.Parameters.Add("@CC", SqlDbType.Int).Value = numero_cc;
            uppess.Parameters.Add("@DataNascimento", SqlDbType.VarChar).Value = data_Nascimento;
            uppess.Parameters.Add("@Morada", SqlDbType.Int).Value = m.Morada_id;

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

        /// <summary>
        /// servico que retorna as infos de um user nao residente
        /// </summary>
        /// <param name="p"> o user </param>
        /// <returns> as infos tipadas</returns>
        public User GetNaoResidente(Pessoas p)
        {
            User n = new User();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarNaoRes = @"SELECT * FROM Nao_Residente WHERE PessoaID_Pessoa=@ID";
            SqlCommand selnr = new SqlCommand(SelecionarNaoRes, con);
            selnr.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;

            data = selnr.ExecuteReader();
            while (data.Read())
            {
                n.id_pessoa = p.ID;
                n.id_user = (int)data["ID_NaoResidente"];
                n.validacao = (char)data["Validacao"];
            }
            data.Close();
            con.Close();
            return n;
        }

        /// <summary>
        /// servico que retorna as infos de todos os nao residentes
        /// </summary>
        /// <returns> lista de todos os nao residentes com as informaçoes deles</returns>
        public List<User> GetAllNaoResidentes()
        {
            List<User> h = new List<User>();
            User user = new User();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarNaoRes = @"SELECT * FROM Nao_Residente ";
            SqlCommand selnr = new SqlCommand(SelecionarNaoRes, con);
            data = selnr.ExecuteReader();
            while (data.Read())
            {
                user.id_pessoa = (int)data["PessoaID_Pessoa"];
                user.id_user = (int)data["ID_NaoResidente"];
                user.validacao = (char)data["Validacao"];
                h.Add(user);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que permite adicionar um nao residente 
        /// </summary>
        /// <param name="validacao"> validacao do administrador</param>
        /// <param name="p"> pessoa a ser adicionada</param>
        /// <returns> se foi ou nao possivel adicionar a nao residente</returns>
        public bool Regist_Nao_Residente(char validacao, Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string insertNaoRes = @"insert into Nao_Residente(PessoaID_Pessoa,Validacao) values(@ID_Pessoa,@Validacao)";
            SqlCommand innr = new SqlCommand(insertNaoRes, con);
            innr.Parameters.Add("@ID_Pessoa", SqlDbType.Int).Value = p.ID;
            innr.Parameters.Add("@Validacao", SqlDbType.Bit).Value = validacao;

            if (innr.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }

            con.Close();
            return true;

        }

        /// <summary>
        /// servico que deleta um nao residente
        /// </summary>
        /// <param name="p"> user nao residente </param>
        /// <returns> se foi possivel remover a pessoa</returns>
        public bool Delete_Nao_residente(Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT *FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string DeleteNaoRes = @"Update Nao_Residente SET Validacao=@validacao where PessoaID_Pessoa=@ID";
            SqlCommand delnr = new SqlCommand(DeleteNaoRes, con);
            delnr.Parameters.Add("@validacao", SqlDbType.Bit).Value = 0;
            delnr.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            if (delnr.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite alterar o valor da validacao de um user nao residente
        /// </summary>
        /// <param name="validacao"> validacao deste user </param>
        /// <param name="p"> user </param>
        /// <returns> se foi ou nao possivel alterar a validacao</returns>
        public bool Update_Nao_residente(char validacao, Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string DeleteNaoRes = @"Update Nao_Residente SET Validacao=@validacao where PessoaID_Pessoa=@ID";
            SqlCommand delnr = new SqlCommand(DeleteNaoRes, con);
            delnr.Parameters.Add("@validacao", SqlDbType.Bit).Value = validacao;
            delnr.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            if (delnr.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        #endregion

        #region Residente

        /// <summary>
        /// servico que retorna as infos de um user residente
        /// </summary>
        /// <param name="p"> o user </param>
        /// <returns> as infos tipadas</returns>
        public User GetResidente(Pessoas p)
        {
            User n = new User();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarRes = @"SELECT * FROM Residente WHERE PessoaID_Pessoa=@ID";
            SqlCommand selr = new SqlCommand(SelecionarRes, con);
            selr.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;

            data = selr.ExecuteReader();
            while (data.Read())
            {
                n.id_pessoa = p.ID;
                n.id_user = (int)data["ID_Residente"];
                n.validacao = (char)data["Validacao"];
            }
            data.Close();
            con.Close();
            return n;
        }

        /// <summary>
        /// servico que retorna as infos de todos os residentes
        /// </summary>
        /// <returns> lista de todos os residentes com as informaçoes deles</returns>
        public List<User> GetAllResidentes()
        {
            List<User> h = new List<User>();
            User user = new User();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarRes = @"SELECT * FROM Residente ";
            SqlCommand selr = new SqlCommand(SelecionarRes, con);
            data = selr.ExecuteReader();
            while (data.Read())
            {
                user.id_pessoa = (int)data["PessoaID_Pessoa"];
                user.id_user = (int)data["ID_Residente"];
                user.validacao = (char)data["Validacao"];
                h.Add(user);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que permite adicionar um residente 
        /// </summary>
        /// <param name="validacao"> validacao do administrador</param>
        /// <param name="p"> pessoa a ser adicionada</param>
        /// <returns> se foi ou nao possivel adicionar o residente</returns>
        public bool Regist_Residente(char validacao, Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();


            string insertRes = @"insert into Residente(PessoaID_Pessoa,Validacao) values(@ID_Pessoa,@Validacao)";
            SqlCommand inr = new SqlCommand(insertRes, con);
            inr.Parameters.Add("@ID_Pessoa", SqlDbType.Int).Value = p.ID;
            inr.Parameters.Add("@Validacao", SqlDbType.Bit).Value = validacao;

            if (inr.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }

            con.Close();
            return true;

        }

        /// <summary>
        /// servico que deleta um residente
        /// </summary>
        /// <param name="p"> user residente </param>
        /// <returns> se foi possivel remover a pessoa residente</returns>
        public bool Delete_Residente(Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string DeleteRes = @"Update Residente SET Validacao=@validacao where PessoaID_Pessoa=@ID";
            SqlCommand delr = new SqlCommand(DeleteRes, con);
            delr.Parameters.Add("@validacao", SqlDbType.Bit).Value = 0;
            delr.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            if (delr.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite alterar o valor da validacao de um user residente
        /// </summary>
        /// <param name="validacao"> validacao deste user </param>
        /// <param name="p"> user </param>
        /// <returns> se foi ou nao possivel alterar a validacao</returns>
        public bool Update_Residente(char validacao, Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string UpdateRes = @"Update Residente SET Validacao=@validacao where PessoaID_Pessoa=@ID";
            SqlCommand upr = new SqlCommand(UpdateRes, con);
            upr.Parameters.Add("@validacao", SqlDbType.Bit).Value = validacao;
            upr.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            if (upr.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }
        #endregion

        #region Funcionario

        /// <summary>
        /// servico que retorna as infos de um funcionario
        /// </summary>
        /// <param name="p"> o funcionario </param>
        /// <returns> as infos tipadas</returns>
        public Funcionario GetFuncinario(Pessoas p)
        {
            Funcionario n = new Funcionario();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarFun = @"SELECT * FROM Funcionario WHERE PessoaID_Pessoa=@ID";
            SqlCommand self = new SqlCommand(SelecionarFun, con);
            self.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;

            data = self.ExecuteReader();
            while (data.Read())
            {
                n.id_pessoa = p.ID;
                n.id_funcionario = (int)data["ID_Funcionario"];
                n.funcao = (string)data["Funçao"];
            }
            data.Close();
            con.Close();
            return n;
        }

        /// <summary>
        /// servico que retorna as infos de todos os funcionarios
        /// </summary>
        /// <returns> lista de todos os funcionarios com as informaçoes deles</returns>
        public List<Funcionario> GetAllFuncionarios()
        {
            List<Funcionario> h = new List<Funcionario>();
            Funcionario user = new Funcionario();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarFun = @"SELECT * FROM Funcionario ";
            SqlCommand self = new SqlCommand(SelecionarFun, con);
            data = self.ExecuteReader();
            while (data.Read())
            {
                user.id_pessoa = (int)data["PessoaID_Pessoa"];
                user.id_funcionario = (int)data["ID_Funcionario"];
                user.funcao = (string)data["Funcao"];
                h.Add(user);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que permite adicionar um Funcionario 
        /// </summary>
        /// <param name="funcao"> funcao do funcionario</param>
        /// <param name="p">pessoa a ser adicionada</param>
        /// <returns>se foi ou nao possivel adicionar o funcionario</returns>
        public bool Regist_Funcionario(string funcao, Pessoas p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.ID;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();


            string insertFun = @"insert into Funcionario(PessoaID_Pessoa,Funçao) values(@ID_Pessoa,@Funçao)";
            SqlCommand inf = new SqlCommand(insertFun, con);
            inf.Parameters.Add("@ID_Pessoa", SqlDbType.Int).Value = p.ID;
            inf.Parameters.Add("@Funçao", SqlDbType.Bit).Value = funcao;

            if (inf.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }

            con.Close();
            return true;

        }

        /// <summary>
        /// servico que deleta um funcionario
        /// </summary>
        /// <param name="p"> pessoa </param>
        /// <returns> se foi possivel remover o funcionario</returns>
        public bool Delete_Funcionario(Funcionario p)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string DeleteFuncionario = @"DELETE FROM Funcionairo WHERE ID_Funcionario=@ID";
            SqlCommand delf = new SqlCommand(DeleteFuncionario, con);
            delf.Parameters.Add("@ID", SqlDbType.Int).Value = p.id_funcionario;
            if (delf.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite atualizar as infos de um funcionario
        /// </summary>
        /// <param name="funcao"> funcao do funcionario</param>
        /// <param name="p">infos antigas do funcionario</param>
        /// <returns>se foi ou nao possivel atualizar o funcionario</returns>
        public bool Update_Funcionario(string funcao, Funcionario p)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarPessoa = @"SELECT * FROM Pessoa WHERE ID_PESSOA=@ID";
            SqlCommand selpess = new SqlCommand(SelecionarPessoa, con);
            selpess.Parameters.Add("@ID", SqlDbType.Int).Value = p.id_pessoa;
            data = selpess.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string UpdateFunc = @"Update Funcionario SET Funçao=@funcao where PessoaID_Pessoa=@ID";
            SqlCommand upf = new SqlCommand(UpdateFunc, con);
            upf.Parameters.Add("@funcao", SqlDbType.Bit).Value = funcao;
            upf.Parameters.Add("@ID", SqlDbType.Int).Value = p.id_pessoa;
            if (upf.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        #endregion

        #region Administrador

        /// <summary>
        /// servico que retorna as infos de um Administrador
        /// </summary>
        /// <param name="p"> o funcionario </param>
        /// <returns> as infos tipadas</returns>
        public Administrador GetAdmin(Funcionario p)
        {
            Administrador n = new Administrador();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarAdmin = @"SELECT * FROM Administrador WHERE FuncionarioID_Funcionario=@ID";
            SqlCommand sela = new SqlCommand(SelecionarAdmin, con);
            sela.Parameters.Add("@ID", SqlDbType.Int).Value = p.id_funcionario;
            data = sela.ExecuteReader();
            while (data.Read())
            {
                n.id_funcionario = p.id_funcionario;
            }
            data.Close();
            con.Close();
            return n;
        }

        /// <summary>
        /// servico que retorna as infos de todos os administradores
        /// </summary>
        /// <returns> lista de todos os Administradores com os seus ids de funcionarios</returns>
        public List<Administrador> GetAllAdmin()
        {
            List<Administrador> h = new List<Administrador>();
            Administrador user = new Administrador();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarAdmin = @"SELECT * FROM Administrador ";
            SqlCommand sela = new SqlCommand(SelecionarAdmin, con);
            data = sela.ExecuteReader();
            while (data.Read())
            {
                user.id_funcionario = (int)data["ID_Funcionario"];
                h.Add(user);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que adiciona um administrador a base de dados
        /// </summary>
        /// <param name="a"> funcionario que é administrador</param>
        /// <returns> se foi ou nao possivel adicionar o administrador</returns>
        public bool Regist_Admin(Funcionario a)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string SelecionarFun = @"SELECT * FROM Funcionario WHERE PessoaID_Pessoa=@ID";
            SqlCommand self = new SqlCommand(SelecionarFun, con);
            self.Parameters.Add("@ID", SqlDbType.Int).Value = a.id_pessoa;

            data = self.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }

            string insertAdmin = @"insert into Administrador(FuncionarioID_Funcionario) values(@ID)";
            SqlCommand ina = new SqlCommand(insertAdmin, con);
            ina.Parameters.Add("@ID", SqlDbType.Int).Value = a.id_funcionario;
            if (ina.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }

            con.Close();
            return true;

        }

        /// <summary>
        /// servico que deleta um Administrador
        /// </summary>
        /// <param name="p"> administrador </param>
        /// <returns> se foi possivel remover o administrador</returns>
        public bool Delete_Admin(Administrador p)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string DeleteAdministrador = @"DELETE FROM Administrador WHERE FuncionarioID_Funcionario=@ID";
            SqlCommand dela = new SqlCommand(DeleteAdministrador, con);
            dela.Parameters.Add("@ID", SqlDbType.Int).Value = p.id_funcionario;
            if (dela.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que altera um administrador
        /// </summary>
        /// <param name="p"> administrador antigo </param>
        /// <param name="a"> novo administrador</param>
        /// <returns>se foi ou nao possivel alterar as infos do administrador</returns>
        public bool Update_Admin(Administrador p, Funcionario a)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string UpdateAdmin = @"Update Administrador SET FuncionarioID_Funcionario=@ID where FuncionarioID_Funcionario=@IDantigo";
            SqlCommand upa = new SqlCommand(UpdateAdmin, con);
            upa.Parameters.Add("@IDantigo", SqlDbType.Int).Value = a.id_funcionario;
            upa.Parameters.Add("@ID", SqlDbType.Int).Value = p.id_funcionario;
            if (upa.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        #endregion

        #region Doc

        /// <summary>
        /// servico que permite envia as infos de um documento
        /// </summary>
        /// <param name="doc">identificacao do documento</param>
        /// <returns>infos do documento </returns>
        public Doc GetDoc(int doc)
        {
            Doc codigo = new Doc();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string SelecionarDoc = @"SELECT *FROM Docs WHERE ID_DOC=@ID";
            SqlCommand seldoc = new SqlCommand(SelecionarDoc, con);
            seldoc.Parameters.Add("@ID", SqlDbType.Int).Value = doc;
            data = seldoc.ExecuteReader();
            while (data.Read())
            {
                codigo.id =doc;
                codigo.descricao = (string)data["Descriçao"];
            }
            data.Close();
            con.Close();
            return codigo;
        }

        /// <summary>
        /// Servico que retorna as informacoes de todos os documentos
        /// </summary>
        /// <returns> as infos de todos os documentos</returns>
        public List<Doc> GetAllDocs()
        {
            List<Doc> h = new List<Doc>();
            Doc codigo = new Doc();
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string Selecionardocumento = @"SELECT *FROM Docs ";
            SqlCommand seldoc = new SqlCommand(Selecionardocumento, con);
            data = seldoc.ExecuteReader();
            while (data.Read())
            {
                codigo.id = (int)data["ID_DOC"];
                codigo.descricao = (string)data["Descriçao"];
                h.Add(codigo);
            }
            data.Close();
            con.Close();
            return h;
        }

        /// <summary>
        /// servico que permite adicionar um documento
        /// </summary>
        /// <param name="id">identificacao do documento</param>
        /// <param name="descricao"> descricao do documento</param>
        /// <returns> se foi ou nao possivel adicionar o documento </returns>
        public bool RegistDoc(int id, string descricao)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string CompareDoc = @"Select * from Docs where ID_DOC=@ID";
            SqlCommand compdc = new SqlCommand(CompareDoc, con);
            compdc.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            data = compdc.ExecuteReader();
            if (data.HasRows == true)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string insertDoc = @"insert into Docs(ID_DOC,Descriçao) values(@ID,@descricao)";
            SqlCommand indoc = new SqlCommand(insertDoc, con);
            indoc.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            indoc.Parameters.Add("@descricao", SqlDbType.VarChar).Value = descricao;

            if (indoc.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

       /// <summary>
       /// servico que deleta um documento
       /// </summary>
       /// <param name="id"> id do documento </param>
       /// <returns> se foi ou nao possivel remover o documento </returns>
        public bool DeleteDoc(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string DeleteDoc = @"DELETE FROM Docs WHERE ID_DOC=@ID";
            SqlCommand deldoc = new SqlCommand(DeleteDoc, con);
            deldoc.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            if (deldoc.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        /// <summary>
        /// servico que permite alterar as informaçoes de um documento
        /// </summary>
        /// <param name="id"> identificacao do documento </param>
        /// <param name="descricao"> descricao do documento </param>
        /// <returns> se foi ou nao possivel alterar o documento</returns>
        public bool UpdateDoc(int id, string descricao)
        {
            SqlDataReader data;
            string cs = ConfigurationManager.ConnectionStrings["Trabalho_ISI_ESConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string Comparedoc = @"Select * from Docs where ID_DOC=@ID";
            SqlCommand compdoc = new SqlCommand(Comparedoc, con);
            compdoc.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            data = compdoc.ExecuteReader();
            if (data.HasRows == false)
            {
                data.Close();
                con.Close();
                return false;
            }
            data.Close();

            string Updatedoc = @"Update DOCS SET Descriçao=@Descricao where ID_DOC=@ID";
            SqlCommand updoc = new SqlCommand(Updatedoc, con);
            updoc.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            updoc.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = descricao;
            if (updoc.ExecuteNonQuery() == 0)
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        #endregion

        #region Servicos

        //List<Servico> GetServico(User a, Funcionario n, Doc d)
        //{
        //    List<Servico> ss = new List<Servico>();
        //    Servico s = new Servico();

            

        //    return ss;
        //}

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
    public struct Pessoas
    {
        public int ID;
        public string name;
        public string password;
        public int contribuinte;
        public string data_Nascimento;
        public int numero_cc;
        public int Morada_id;
    }

    /// <summary>
    /// Struct para conter a informação de uma codigo postal
    /// </summary>
    public struct CP
    {
        public string codigo_postal;
        public string localidade;
    }

    /// <summary>
    /// Struct para conter a informação de um documento
    /// </summary>
    public struct Doc
    {
        public int id;
        public string descricao;
    }

    /// <summary>
    /// Struct para conter a informação de um servico
    /// </summary>
    public struct Servico
    {
        public int id;
        public string descricao;
        public string data;
        public int id_residente;
        public char validacao;
        public int id_funcionario;
        public int id_doc;
    }

    /// <summary>
    /// Struct para conter a informação de um User
    /// </summary>
    public struct User
    {
        public int id_user;
        public int id_pessoa;
        public char validacao;
    }

    /// <summary>
    /// Struct para conter a informação de um funcionario
    /// </summary>
    public struct Funcionario
    {
        public int id_funcionario;
        public int id_pessoa;
        public string funcao;
    }

    /// <summary>
    /// Struct para conter a informação de um Administrador
    /// </summary>
    public struct Administrador
    {
        public int id_funcionario;
    }
}
