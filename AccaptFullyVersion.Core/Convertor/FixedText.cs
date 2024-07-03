using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Convertor
{
    public class FixedText
    {
        public static string FixedEmailTex(string email)
        {
            return email.ToLower();
        }
    }
}
