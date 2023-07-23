using Microcredit.Models;
using Microcredit.Reports.ExecuteSP.ModelRepor;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Data.SqlClient;
using Stimulsoft.Blockly.Model;
using Microcredit.Reports.ExecuteSP.ModelReportpaymentOfistallments;

namespace Microcredit.Reports.ExecuteSP
{
    public class ExecutepaymentOfistallmentsbetweenDateReport : IExecuteReportWithmultipleParam
    {


        private readonly ApplicationDbContext _db;
         private StringBuilder sb;

        public ExecutepaymentOfistallmentsbetweenDateReport(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue)
        {

            var result = _db.PaymentOfistallmentsModeLReports.FromSqlRaw(SPName, ParamValue).AsNoTracking(); ;
            //_db.Dispose();
            return (IEnumerable<T>)result;



        }
        public   IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue, SqlParameter SecondParamValue)
        {
            var result = _db.PaymentOfistallmentsModeLReports.FromSqlRaw(SPName, ParamValue, SecondParamValue).AsNoTracking(); ;
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }


        public string GetHTMLStringWitmultipleParam([Optional] SqlParameter  DateFrom, SqlParameter DateTo)
        {


            var  GETpaymentOfistallmentsbetweenDate = ExecuteReportSP<PaymentOfistallmentsModeLReport>("dbo.SP_GETpaymentOfistallmentsbetweenDate {0}, {1}", DateFrom, DateTo);


            var TotalAmount= 0;

            sb = new StringBuilder();
            sb.Append(@"
           <html><head></ head>

  <link rel=""stylesheet"" href=""H:\MicrocreditDB\MicrocreditBackEnd\Microcredit\StaticFiles\bootstrap\css\bootstrap.css"">
<body>
      
 <table align='center' style='direction: rtl;' class='table table-bordered'>

      <tr style='  direction: rtl;' >
      <th scope='col'>كود العميل</th>

      <th scope='col'>اسم العميل</th>

      <th scope='col'>كود القرض</th>
      <th scope='col'>مبلغ القسط</th>
      <th scope='col'>المبلغ المدفوع</th>
      <th scope='col'>التاريخ</th>
      
      </tr>");

            foreach (var _GETpaymentOfistallmentsbetweenDate in GETpaymentOfistallmentsbetweenDate.ToList())
            {
                TotalAmount = +TotalAmount + (int) _GETpaymentOfistallmentsbetweenDate.TotalPaid; 
 
                sb.AppendFormat(@"<tr style=' direction: rtl; align='center'>
                        <td scope='col'>{0}</td>
                        <td scope='col'>{1}</td>
                        <td scope='col'>{2}</td>
                        <td scope='col'>{3}</td>
                        <td scope='col'>{4}</td>
                        <td scope='col'>{5}</td>
                        <td scope='col'>{6}</td>

                 


                        </tr> ",

          _GETpaymentOfistallmentsbetweenDate.CustomerId,
          _GETpaymentOfistallmentsbetweenDate.CustomerName,
          _GETpaymentOfistallmentsbetweenDate.LonaId,
          _GETpaymentOfistallmentsbetweenDate.IstalmentsAmount,
          _GETpaymentOfistallmentsbetweenDate.AmountPaid,
          _GETpaymentOfistallmentsbetweenDate.DateAdd,
          _GETpaymentOfistallmentsbetweenDate.TotalPaid = TotalAmount


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
                        TotalAmount);
 
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();
            sb.Append(@"</table></body></html>");
            GC.Collect();
            return sb.ToString();

        }

        public string GetHTMLString([Optional] SqlParameter ParamValue)
        {
            throw new NotImplementedException();
        }

        public string GetHTMLStringWithoutParam()
        {
            throw new NotImplementedException();
        }
    }
}
