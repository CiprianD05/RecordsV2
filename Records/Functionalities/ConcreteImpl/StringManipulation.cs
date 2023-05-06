using Records.Functionalities.Interfaces;
using System.Text;

namespace Records.Functionalities.ConcreteImpl
{
    public class StringManipulation : IStringManipulation
    {
        public string AbbreviationExtractor(string name)
        {
            var strBuilder=new StringBuilder();

            foreach(var c in name)
            {
                if(Char.IsUpper(c))
                    strBuilder.Append(c);
            }
            return strBuilder.ToString();
        }
    }
}
