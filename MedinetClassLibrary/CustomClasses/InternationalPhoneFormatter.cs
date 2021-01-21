using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetClassLibrary.Classes
{
    public static class InternationalPhoneFormatter
    {
        public static string FormatPhone(string maskedPhone)
        {
            char[] delimiterChars = {'+', '(', ')', '-' };
            string unmaskedPhone = null;

            if (maskedPhone.Length > 0)
            {
                string[] phoneTerms = maskedPhone.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in phoneTerms)
                {
                    unmaskedPhone += term;
                }
            }

            if (unmaskedPhone.Length < 7)
                return null;
            else
                return unmaskedPhone;
        }
    }
}
