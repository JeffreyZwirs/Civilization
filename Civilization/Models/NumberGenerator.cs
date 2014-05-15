using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Civilization.Models
{
    public class NumberGenerator
    {
        public int Gen(int length)
        {
            var builder = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                builder.Append(random.Next(10));
            }
                       
            return Convert.ToInt32(builder.ToString());
        }
    }
}