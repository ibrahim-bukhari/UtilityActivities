using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Utilities
{

    public sealed class AddNumInString : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> StringValue { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Add { get; set; }
        
        [Category("Output")]
        [RequiredArgument]
        public OutArgument<String> UpdatedValue { get; set; }

        
        [Category("Output")]
        public OutArgument<Exception> Exception { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                string str = context.GetValue(this.StringValue);
                string add = context.GetValue(this.Add);


                string resultString = Regex.Match(str, @"\d+").Value;
                long num = Convert.ToInt64(resultString);
                Console.WriteLine(resultString);
                num += Convert.ToInt64(add);

                int length = resultString.Length;
                string asString = num.ToString("D" + length);
                Console.WriteLine("Updated num string: " + asString);

                string sub = str.Substring(0, 3);
                string final = string.Concat(sub, asString);
                Console.WriteLine("Final string: " + final);

                UpdatedValue.Set(context, final);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Exception.Set(context, ex);
                throw ex;
            }


        }
    }
}
