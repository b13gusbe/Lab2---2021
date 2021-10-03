using System;

namespace Lab2___Butik
{
    [Serializable]
    class GoldCustomer : Customer
    {
        public GoldCustomer(string username, string password) : base(username, password) { }
        
    }
}
