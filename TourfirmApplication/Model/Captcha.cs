using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourfirmApplication.Model
{
    internal static class Captcha
    {
        private static string _allowChar="A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        private static string _allowDigitalChar = "1,2,3,4,5,6,7,8,9,0";

        public static string Generate(int N)
        {
             Random _random = new Random();
            string _strCaptcha = string.Empty;
            string temp = _allowChar.ToLower() + "," + _allowDigitalChar;
            string[] separChar = temp.Split(',');
            for (int i = 0; i < N; ++i)
            {
                temp = separChar[(_random.Next(0, separChar.Length))];
                _strCaptcha += temp;
            }
            return _strCaptcha;
        }
    }
}
