using Microcredit.Models;
using Microcredit.Reports.ExecuteSP.ModelRepor;
using Microcredit.Reports.ExecuteSP.ModelReportCustomers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecutCountCustomersReport : IExecutCountCustomersReport
    {

        private readonly ApplicationDbContext _db;
         private StringBuilder sb;

        public ExecutCountCustomersReport(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.getCountCustomers.FromSqlRaw(SPName, ParamValue).AsNoTracking(); 
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }


        public string GetHTMLString([Optional] SqlParameter ParamValue)
        {
            throw new NotImplementedException();
        }

        public string GetHTMLStringWithoutParam()
        {

            var  GetCountCustomers  =
ExecuteReportSP<GetCountCustomersReportModel>("select * from dbo.View_GetCountCustomers");

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
   

                            </ head>
                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
                                <table align='center'>
                                    <tr>
 <th style='text-align: center;'>عدد العملاء المسجلين بالنظام</th>
 

 
                                    </tr>");
            foreach (var _GetCountCustomers in GetCountCustomers.ToList())
            {
                sb.AppendFormat(@"<tr>
                                    <td style='text-align: center;'>{0}</td>
                                    
                                    </tr>
",
   _GetCountCustomers.CustomerId
 );
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }

    }
}
