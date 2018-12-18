using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Servicos_Web
{
    public bool GetDatalogin(string name, string password)
    {
        return false;
    }


    public bool RegistUser(string name, string password, int contribuinte, int numero_cc, string rua, int num_porta, int cp)
    {
        return false;
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
