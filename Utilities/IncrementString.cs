using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Utilities
{

    public sealed class IncrementString : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> StringNum { get; set; }

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
                string str = context.GetValue(this.StringNum);
                string add = context.GetValue(this.Add);

                long n = Convert.ToInt64(str);
                long i = Convert.ToInt64(add);

                long sum = n + i;
                string final = Convert.ToString(sum);
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
