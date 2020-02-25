using System;
using System.Collections.Generic;
using System.Text;

namespace AllCountries.Classes
{
    public class Country
    {
        private string code;
        private string omschrijving;
        private byte[] byteArray;

        public string Code 
        { 
            get { return code; }
            set { code = value; }
        }

        public string Omschrijving
        {
            get { return omschrijving; }
            set { omschrijving = value; }
        }

        public byte[] ByteArray
        {
            get { return byteArray; }
            set { byteArray = value; }
        }
    }
}
