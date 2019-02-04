using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Globalization;

namespace Utilities
{

    public sealed class GetDateComponents : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> CulturalInfo { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Date { get; set; }

        [Category("Output")]
        public OutArgument<string> Day { get; set; }

        [Category("Output")]
        public OutArgument<string> DayOfWeek { get; set; }

        [Category("Output")]
        public OutArgument<string> Month { get; set; }

        [Category("Output")]
        public OutArgument<string> Year { get; set; }

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

                CultureInfo ci = new CultureInfo(culture);
                DateTime d = Convert.ToDateTime(date, ci);

                Day.Set(context, Convert.ToString(d.Day));
                DayOfWeek.Set(context, Convert.ToString(d.DayOfWeek));
                Month.Set(context, Convert.ToString(d.Month));
                Year.Set(context, Convert.ToString(d.Year));
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
