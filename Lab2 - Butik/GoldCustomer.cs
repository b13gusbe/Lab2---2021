using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___Butik
{
    [Serializable]
    class GoldCustomer : Customer
    {

        public GoldCustomer(string username, string password) : base(username, password) { }
        

    }
}
