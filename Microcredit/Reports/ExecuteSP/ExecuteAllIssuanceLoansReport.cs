using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP.ModelReportCustomers;
using Microcredit.Reports.ExecuteSP.ModelReportGetAllIssuanceLoans;
using Microcredit.Reports.ExecuteSP.ModelReportpaymentOfistallments;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;

namespace Microcredit.Reports.ExecuteSP
{

    public class ExecuteAllIStatusLoansReport : IAllIStatusLoansReport
    {
        private readonly ApplicationDbContext _db;
        private StringBuilder sb;

        public ExecuteAllIStatusLoansReport(ApplicationDbContext db)
        {
            _db = db;

        }
        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.getAllIssuanceLoansReportModels.FromSqlRaw(SPName, ParamValue).AsNoTracking();
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }

        public string GetHTMLString([Optional] SqlParameter ParamValue)
        {
            throw new NotImplementedException();
        }

        public string GetHTMLStringWithoutParam()
        {

            var GetAllIssuanceLoans =
ExecuteReportSP<GetAllIssuanceLoansReportModel>("select * from dbo.View_GetAllIssuanceLoans");

            var sb = new StringBuilder();
            sb.Append(@"
           <html><head></ head>

  <link rel=""stylesheet"" href=""H:\MicrocreditDB\MicrocreditBackEnd\Microcredit\StaticFiles\bootstrap\css\bootstrap.css"">
<body>

                            <body>
<img src='' alt='Girl in a jacket' width='' height=''>
 <table align='center' style='direction: rtl;' class='table table-bordered'>
                                    <tr>
 <th scope='col'>كود العميل</th>
  <th scope='col'>اسم العميل</th>
 <th scope='col'>مبلغ القرض</th>
  <th scope='col'>المنتج</th>
 </tr>");
            foreach (var _GetAllIssuanceLoans in GetAllIssuanceLoans.ToList())
            {
                sb.AppendFormat(@"<tr>
                                    <td scope='col'>{0}</td>
                                 <td scope='col'>{1}</td>
                                    <td scope='col'>{2}</td>
                                    <td scope='col'>{3}</td>
             </tr>
",
   _GetAllIssuanceLoans.CustomerId,
      _GetAllIssuanceLoans.CustomerName,
   _GetAllIssuanceLoans.LonaAmount,
    _GetAllIssuanceLoans.ProdouctName

 );
            }
            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }


        public string GetCountLoansUnderEdit()
        {



            var GetAllLoansUnderEdit = _db.allLoansUnderEditReportModels.FromSqlRaw
 ("select * from dbo.View_AllLoansUnderEdit").AsNoTracking();


            var sb = new StringBuilder();
            sb.Append(@"
           <html><head></ head>

  <link rel=""stylesheet"" href=""H:\MicrocreditDB\MicrocreditBackEnd\Microcredit\StaticFiles\bootstrap\css\bootstrap.css"">
<body>

                      

 <table align='center' style='direction: rtl;' class='table table-bordered'>
                                    <tr>
 <th scope='col'>كود القرض</th>
 <th scope='col'>كود العميل
</th>
 
  <th scope='col'>اسم العميل</th>
 

 
                                    </tr>");
            foreach (var _GetAllLoansUnderEdit in GetAllLoansUnderEdit.ToList())
            {
                sb.AppendFormat(@"<tr>
                                    <td scope='col'>{0}</td>
                                 <td scope='col'>{1}</td>
                                    <td scope='col'>{2}</td>
 
                                    </tr>
",
   _GetAllLoansUnderEdit.LonaId,
      _GetAllLoansUnderEdit.CustomerId,
   _GetAllLoansUnderEdit.CustomerName

 );
            }


            sb.Append(@"
                        <html>
                            <head>
 </ head>

<img src='' alt='Girl in a jacket' width='' height=''>
 

                            <body>

                                <table align='center'>
                                    <tr>
 <th scope='col'>عدد القروض تحت التحرير</th>
  

 
                                    </tr>");

            sb.AppendFormat(@"<tr>
                                    <td scope='col'>{0}</td>
  
                                    </tr>
",
GetAllLoansUnderEdit.Count()


);

            sb.Append(@"</table></body></html>");


            return sb.ToString();
        }
       
        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue, SqlParameter SecondParamValue)
        {
            var result = _db.duelmentsbetweenDateModelReports.FromSqlRaw(SPName, ParamValue, SecondParamValue).AsNoTracking(); ;
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }
    
        
     
       
    }

    } 


