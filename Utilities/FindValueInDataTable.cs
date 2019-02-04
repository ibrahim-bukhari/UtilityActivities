using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Data;

namespace Utilities
{

    public sealed class FindValueInDatatable : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> SearchValue { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<DataTable> SourceDatatable { get; set; }

        [Category("Input")]
        public InArgument<string> SourceColumn { get; set; }

        [Category("Output")]
        public OutArgument<int> RowNumber { get; set; }

        [Category("Output")]
        public OutArgument<bool> Found { get; set; }

        [Category("Output")]
        public OutArgument<DataRow> Row { get; set; }

        [Category("Output")]
        public OutArgument<Exception> Exception { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                string inVal = context.GetValue(this.SearchValue);
                DataTable dt = context.GetValue(this.SourceDatatable);
                string colName = context.GetValue(this.SourceColumn);
                int rowCounter = 0;
                bool found = false;

                foreach (DataRow row in dt.Rows)
                {
                    string colVal = row[colName].ToString();
                    Console.WriteLine("Row value for column: " + colName + " is: " + colVal);
                    if(string.Equals(colVal, inVal))
                    {
                        found = true;
                        Row.Set(context, row);
                        break;
                    }
                    rowCounter++;
                }
                
                rowCounter = (found) ? rowCounter : -1;
                Found.Set(context, found);
                RowNumber.Set(context, rowCounter);

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
