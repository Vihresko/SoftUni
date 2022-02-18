using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class Constant
    {
        //User
        public const int USER_NAME_MIN_LENGTH = 5;
        public const int USER_NAME_MAX_LENGTH = 20;
        public const string EMAIL_REGEX_VALIDATION = "[A-z]+[@][A-z]+[.][A-z]+";
        public const int PASS_MIN_LENGTH = 6;
        public const int PASS_MAX_LENGTH = 20;
        public const int PASS_HASH_LENGTH = 64;

        //Product
        public const int PRODUCT_NAME_MIN_LENGTH = 4;
        public const int PRODUCT_NAME_MAX_LENGTH = 20;
        public const decimal PRICE_MIN_VALUE = 0.05m;
        public const decimal PRICE_MAX_VALUE = 1000m;
    }
}
