﻿using Microcredit.Reports.ExecuteSP.ModelReportGetAllIssuanceLoans;
using Microcredit.Reports.ExecuteSP.ModelReportpaymentOfistallments;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecuteReportIssuanceLoansbetweenDate : IExecuteReportIssuanceLoansbetweenDate
    {


        private readonly ApplicationDbContext _db;
        private StringBuilder sb;


        public ExecuteReportIssuanceLoansbetweenDate(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.issuanceLoansbetweenDateReportModels.FromSqlRaw(SPName, ParamValue).AsNoTracking();
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }

        public string GetHTMLString([Optional] SqlParameter ParamValue)
        {
            throw new NotImplementedException();
        }

        public string GetHTMLStringWithoutParam()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue, SqlParameter SecondParamValue)
        {
            var result = _db.issuanceLoansbetweenDateReportModels.FromSqlRaw(SPName, ParamValue, SecondParamValue).AsNoTracking(); ;
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }


        public string GetHTMLStringWitmultipleParam([Optional] SqlParameter DateFrom, SqlParameter DateTo)
        {

            var GETDuelmentsbetweenDate = ExecuteReportSP<IssuanceLoansbetweenDateReportModel>("dbo.SP_GetSIssuanceLoansbetweenDate {0}, {1}", DateFrom, DateTo);


            var TotalAmount = 0;

            sb = new StringBuilder();
            sb.Append(@"
           <html><head></ head>

  <link rel=""stylesheet"" href=""H:\MicrocreditDB\MicrocreditBackEnd\Microcredit\StaticFiles\bootstrap\css\bootstrap.css"">
<body>
      
 <table align='center' style='direction: rtl;' class='table table-bordered'>

      <tr style='  direction: rtl;' >
 
      <th scope='col'>كود العميل</th>

      <th scope='col'>اسم العميل</th>
      <th scope='col'>مبلغ القرض</th>

      <th scope='col'>كود القرض</th>

       <th scope='col'>تاريخ اصدار القرض</th>

      
      </tr>");

            foreach (var _GETDuelmentsbetweenDate in GETDuelmentsbetweenDate.ToList())
            {

                sb.AppendFormat(@"<tr style=' direction: rtl; align='center'>
                        <td scope='col'>{0}</td>
                        <td scope='col'>{1}</td>
                        <td scope='col'>{2}</td>
                        <td scope='col'>{3}</td>
                        <td scope='col'>{4}</td>
                       
                        </tr> ",
 
         _GETDuelmentsbetweenDate.CustomerId,
         _GETDuelmentsbetweenDate.CustomerName,
         _GETDuelmentsbetweenDate.LonaAmount,
          _GETDuelmentsbetweenDate.LonaId,
          _GETDuelmentsbetweenDate.DateAdd
 

); ;
            }
            sb.Append(@"
           <html><head></ head>

  <link rel=""stylesheet"" href=""H:\MicrocreditDB\MicrocreditBackEnd\Microcredit\StaticFiles\bootstrap\css\bootstrap.css"">
<body>
      
 <table align='center' style='direction: rtl;' class='table table-bordered'>

      <tr style='  direction: rtl;' >
      
      <th scope='col'>الاجمالى</th>
     
      </tr>");
            sb.AppendFormat(@"<tr style=' direction: rtl; align='center'>
                        <td scope='col' align='center' style='direction: rtl;'>{0}</td>
                      
 
                        </tr> ",
                        GETDuelmentsbetweenDate.Count());


            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();
            sb.Append(@"</table></body></html>");
            GC.Collect();
            return sb.ToString();




        }

    }
}
