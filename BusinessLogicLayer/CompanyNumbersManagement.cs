using DataAccessLayer.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer
{
    public static class CompanyNumbersManagement
    {
        public static bool RegonIsValid(string regon)
        {
            if (string.IsNullOrEmpty(regon))
                return false;

            string regonValue = regon.ToOnlyDigitString();

            int regonLength = regonValue.Length;
            
            if (regonLength == 9)
            {
                char[] regonCharacters = regonValue.ToCharArray();
                int[] regontDigits = regonCharacters.Select(s => (int)char.GetNumericValue(s)).ToArray();
                return NineDigitRegonIsValid(regontDigits);
            }

            if (regonLength == 14)
            {
                char[] regonCharacters = regonValue.ToCharArray();
                int[] regontDigits = regonCharacters.Select(s => (int)char.GetNumericValue(s)).ToArray();
                int[] nineDigitRegontDigits = regontDigits.Take(9).ToArray();

                bool isValid = NineDigitRegonIsValid(nineDigitRegontDigits);

                if (!isValid)
                {
                    return false;
                }
                else
                {
                    int[] weights = { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };//array length = 13

                    int regonSum = 0;
                    for (int i = 0; i < 13; i++)
                    {
                        regonSum += regontDigits[i] * weights[i];
                    }

                    int controlValue = regonSum % 11;
                    if (controlValue == 10)
                        controlValue = 0;

                    if (regontDigits[13] == controlValue)
                        return true;

                    return false;
                }
            }
            return false;
        }

        private static bool NineDigitRegonIsValid(int[] regonDigits)
        {
            int[] weights = { 8, 9, 2, 3, 4, 5, 6, 7 };//array length = 8

            int regonSum = 0;
            for (int i = 0; i < 8; i++)
            {
                regonSum += regonDigits[i] * weights[i];
            }

            int controlValue = regonSum % 11;

            if (controlValue == 10)
                controlValue = 0;

            if (regonDigits[8] == controlValue)
                return true;

            return false;
        }

        public static bool NipIsValid(string nip)
        {
            if (string.IsNullOrEmpty(nip))
                return false;

            string nipValue = nip.ToOnlyDigitString();

            if (nipValue.Length != 10)
                return false;

            char[] nipCharacters = nipValue.ToCharArray();
            int[] nipDigits = nipCharacters.Select(s => (int)char.GetNumericValue(s)).ToArray();

            int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };//array length = 9

            int nipSum = 0;
            for (int i = 0; i < 9; i++)
            {
                nipSum += nipDigits[i] * weights[i];
            }

            int controlValue = nipSum % 11;

            if (nipDigits[9] == controlValue)
                return true;

            return false;
        }

        public static bool KrsIsValid(string krs)
        {
            if (string.IsNullOrEmpty(krs))
                return false;

            string krsValue = krs.ToOnlyDigitString();

            if (!krsValue.StartsWith("0000"))
                return false;

            if (krsValue.Length != 10)
                return false;

            return true;
        }

        public static string ToOnlyDigitString(this string value)
        {
            string regexPattern = @"[^0-9]";
            string digits = Regex.Replace(value, regexPattern, "").Trim();
            return digits;
        }

        public static string ToCorrectlyFormatedNip(this string value)
        {
            string regexPattern = @"[^0-9]";
            string formatedNip = "PL" + Regex.Replace(value, regexPattern, "").Trim();
            return formatedNip;
        }

        public static NumberType RecognizeNumberType(string number)
        {
            if (number.StartsWith("PL"))
            {
                if (NipIsValid(number))
                    return NumberType.NIP;
                else
                    return NumberType.Unrecognized;
            }

            string onlyDigitNumber = number.ToOnlyDigitString();

            switch (onlyDigitNumber.Length)
            {
                case 9:
                    if (RegonIsValid(onlyDigitNumber))
                        return NumberType.REGON;
                    break;

                case 14:
                    if (RegonIsValid(onlyDigitNumber))
                        return NumberType.REGON;
                    break;

                case 10:
                    if (onlyDigitNumber.StartsWith("00"))
                    {
                        if (KrsIsValid(onlyDigitNumber))
                            return NumberType.KRS;
                    }
                    else
                    {
                        if (NipIsValid(onlyDigitNumber))
                            return NumberType.NIP;
                    }    
                    break;

                default:
                    return NumberType.Unrecognized;
            }
            return NumberType.Unrecognized;
        }
    }
}