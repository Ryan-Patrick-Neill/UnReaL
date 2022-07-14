using System.Text;

namespace UnReaL.Repository
{
    public class BijectionService : IBijectionService
    {
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private readonly int Base = Alphabet.Length;

        public string Encode(int num)
        {
            var sb = new StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(num % Base));
                num /= Base;
            }
            return sb.ToString();
        }

        public int Decode(string str)
        {
            var num = 0;
            str.ToList().ForEach(c => num = num * Base + Alphabet.IndexOf(c));
            return num;
        }
    }
}
