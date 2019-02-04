using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Globalization;

namespace Utilities
{

    public sealed class GetFormattedDate : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> CulturalInfo { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Date { get; set; }

        [Category("Input")]
        public InArgument<string> OutputFormat { get; set; }

        [Category("Output")]
        public OutArgument<string> FormattedDate { get; set; }

        [Category("Output")]
        public OutArgument<Exception> Exception { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                string date = context.GetValue(this.Date);
                Console.WriteLine("Date is: " + date);
                string culture = context.GetValue(this.CulturalInfo);
                Console.WriteLine("Culture is: " + culture);
                string format = context.GetValue(this.OutputFormat);
                Console.WriteLine("Output format is: " + format);

                CultureInfo ci = new CultureInfo(culture);
                DateTime d = Convert.ToDateTime(date, ci);

                FormattedDate.Set(context, d.ToString(format));
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
