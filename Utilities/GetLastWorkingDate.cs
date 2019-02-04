using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Utilities
{

    public sealed class GetLastWorkingDate : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<DateTime> Date { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> OutputFormat { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<int> DateOffset { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<int> ExcludeWeekend { get; set; }


        [Category("Output")]
        [RequiredArgument]
        public OutArgument<string> LastWorkDate { get; set; }

        [Category("Output")]
        public OutArgument<Exception> Exception { get; set; }


        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                string format = context.GetValue(this.OutputFormat);
                int offset = context.GetValue(this.DateOffset);
                int ignoreWeekend = context.GetValue(this.ExcludeWeekend);
                DateTime today = context.GetValue(this.Date);

                switch (today.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        today = (ignoreWeekend >= 1) ? today.AddDays(-2 - offset) : today.AddDays(-offset);
                        //today = today.AddDays(-3);
                        break;
                    default:
                        today = today.AddDays(-offset);
                        break;
                }
                Console.WriteLine(today.ToString(format));
                LastWorkDate.Set(context, today.ToString(format));
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
