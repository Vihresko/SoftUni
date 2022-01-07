using System;
using System.Collections.Generic;
using System.Text;

namespace Artillery.Data.Models.Constants
{
    public class Constant
    {
        //Country
        public const int COUNTRY_NAME_MIN_LEN = 4;
        public const int COUNTRY_NAME_MAX_LEN = 60;
        public const int COUNTRY_ARMY_MIN_SIZE = 50000;
        public const int COUNTRY_ARMY_MAX_SIZE = 10000000;

        //Manufacturer
        public const int MANUFACTURER_NAME_MIN_LEN = 4;
        public const int MANUFACTURER_NAME_MAX_LEN = 40;
        public const int FOUNDED_MIN_LEN = 10;
        public const int FOUNDED_MAX_LEN = 100;

        //Shel
        public const double SHELL_WEIGHT_MIN_VALUE = 2;
        public const double SHELL_WEIGHT_MAX_VALUE = 1680;
        public const int CALIBER_TEXT_MIN_LEN = 4;
        public const int CALIBER_TEXT_MAX_LEN = 30;

        //Gun
        public const int GUN_WEIGHT_MIN_VALUE = 100;
        public const int GUN_WEIGHT_MAX_VALUE = 1350000;
        public const double BARREL_LEN_MIN_VALUE = 2.00;
        public const double BARREL_LEN_MAX_VALUE = 35.00;
        public const int GUN_MIN_RANGE = 1;
        public const int GUN_MAX_RANGE = 100000;
    }
}
