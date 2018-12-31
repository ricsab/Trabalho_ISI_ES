using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using System.ServiceModel.Activation;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Servicos_Trabalho
{
    [ServiceContract]
    public interface IService1Soap
    {
        #region Cp

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Cp/{cp}")]
        CP GetCp(string cp);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AllCp")]
        List<CP> GetAllCp();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Cp/{cp}/{localidade}")]
        bool RegistCp(string cp, string localidade);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Cp/{cp}")]
        bool DeleteCp(string cp);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Cp/{cp}/{localidade}")]
        bool UpdateCp(string cp, string localidade);

        #endregion

        #region Morada

        [OperationContract]
        Morada GetMorada(string rua, int num_porta, string cp);

        [OperationContract]
        List<Morada> GetAllMorada();

        [OperationContract]
        bool RegistMorada(string rua, int num_porta, string cp);

        [OperationContract]
        bool DeleteMorada(string rua, int num_porta, string cp);

        [OperationContract]
        bool UpdateMorada(string rua, int num_porta, string cp, Morada m);

        #endregion

        #region Utilizador

        [OperationContract]
        Pessoas GetUser(int pessoa_id);

        [OperationContract]
        List<Pessoas> GetAllUsers();

        [OperationContract]
        bool RegistUser(string name, string password, int contribuinte, string data_Nascimento, int numero_cc, Morada m);

        [OperationContract]
        bool DeleteUser(int pessoa_id);

        [OperationContract]
        bool UpdateUser(string name, string password, int contribuinte, string data_Nascimento, int numero_cc, Morada m);

        #endregion

        #region Nao Residente

        [OperationContract]
        User GetNaoResidente(Pessoas p);

        [OperationContract]
        List<User> GetAllNaoResidentes();

        [OperationContract]
        bool Regist_Nao_Residente(char validacao, Pessoas p);

        [OperationContract]
        bool Delete_Nao_residente(Pessoas p);

        [OperationContract]
        bool Update_Nao_residente(char validacao, Pessoas p);

        #endregion

        #region Residente

        [OperationContract]
        User GetResidente(Pessoas p);

        [OperationContract]
        List<User> GetAllResidentes();

        [OperationContract]
        bool Regist_Residente(char validacao, Pessoas p);

        [OperationContract]
        bool Delete_Residente(Pessoas p);

        [OperationContract]
        bool Update_Residente(char validacao, Pessoas p);

        #endregion

        #region Funcionario

        [OperationContract]
        Funcionario GetFuncinario(Pessoas p);

        [OperationContract]
        List<Funcionario> GetAllFuncionarios();

        [OperationContract]
        bool Regist_Funcionario(string funcao, Pessoas p);

        [OperationContract]
        bool Delete_Funcionario(Funcionario p);

        [OperationContract]
        bool Update_Funcionario(string funcao, Funcionario p);

        #endregion

        #region Administrador

        [OperationContract]
        Administrador GetAdmin(Funcionario a);

        [OperationContract]
        List<Administrador> GetAllAdmin();

        [OperationContract]
        bool Regist_Admin(Funcionario a);

        [OperationContract]
        bool Delete_Admin(Administrador p);

        [OperationContract]
        bool Update_Admin(Administrador p, Funcionario a);

        #endregion

        #region Doc

        [OperationContract]
        Doc GetDoc(int id_doc);

        [OperationContract]
        List<Doc> GetAllDocs();

        [OperationContract]
        bool RegistDoc(int id, string descricao);

        [OperationContract]
        bool DeleteDoc(int id);

        [OperationContract]
        bool UpdateDoc(int id, string descricao);

        #endregion

        #region Servicos

        //[OperationContract]
        //List<Servico> GetServico(User a,Funcionario n,Doc d);

        //[OperationContract]
        //List<Servico> GetAllServicos();

        //[OperationContract]
        //bool Regist_Servico(string descricao,DateTimeFormat b ,User a, Funcionario n, Doc d);

        //[OperationContract]
        //bool Delete_Servico(Servico p);

        //[OperationContract]
        //bool Update_Servico(Servico s, string descricao, DateTimeFormat b, User a, Funcionario n, Doc d);


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


}
