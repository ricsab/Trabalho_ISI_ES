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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool GetDatalogin(string name, string password);

        [OperationContract]
        bool RegistUser(string name, string password, int contribuinte, DateTime data_Nascimento, int numero_cc, string rua, int num_porta, int cp);

        [OperationContract]
        bool DeleteUser(int numero_cc);

        [OperationContract]
        bool UpdateUser(string name, string password, int contribuinte, int numero_cc, string rua, int num_porta, int cp);
    }

   
}
