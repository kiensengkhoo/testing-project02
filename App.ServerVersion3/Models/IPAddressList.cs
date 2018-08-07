using System;
using System.Collections.Generic;
using System.Text;

namespace App.ServerVersion3.Models
{
    public class IPAddressList
    {
        public string IPAddress { get; set; }

        public List<IPAddressList> GetIP()
        {
            List<IPAddressList> list = new List<IPAddressList>()
            {
                new IPAddressList{ IPAddress = "34.227.178.142" },
                new IPAddressList{ IPAddress = "34.227.149.106" }
            };

            return list;
        }
    }
}
