using Microcredit.Models;
using Microcredit.Reports.ExecuteSP.ModelRepor;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Data.SqlClient;
using Stimulsoft.Blockly.Model;

namespace Microcredit.Reports.ExecuteSP
{

    public class ExecutAllInfoAboutcustomerReport : IExecuteReport
    {
        private readonly ApplicationDbContext _db;
        private List<GETGuarantorNameModelReport> lonaGuarantModelReports;
        private StringBuilder sb;
        private string GetStatusIstalments;

        public ExecutAllInfoAboutcustomerReport(ApplicationDbContext db)
        {
            _db = db;
        }


        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.getLonaGuarantModelReport.FromSqlRaw(SPName, ParamValue).AsNoTracking(); ;
            //_db.Dispose();
            return (IEnumerable<T>)result;
        }



        /// <summary>
        /// can not used AddNewLoanObjectModelReport because diffrent proc and query not retrive all col from class object
        /// say.....
        /// AddNewLoanObjectModelReport =>
        /// public class AddNewLoanObjectModelReport

        /// public int CustomerId { get; set; }
        ///  public string CustomerName { get; set; }
        ///  public int LonaGuarantorId { get; set; }
        ///  in proc not return all col from  AddNewLoanObjectModelReport  and not mapped not useful here
        /// 
        /// </summary>
        /// <param name="ParamValue"></param>
        /// <returns></returns>
        /// 

        public string GetHTMLString([Optional] SqlParameter ParamValue)
        {

            var GETLonaGuarantorNames = ExecuteReportSP<GETGuarantorNameModelReport>("dbo.SP_GETLonaGuarantorNames @CustomerId", ParamValue);

            var GETAllInfoAboutcustomerLoan = _db.gETAllInfoAboutcustomerLoanReports.FromSqlRaw("dbo.SP_AllInfoAboutcustomerLoan @CustomerId", ParamValue).AsNoTracking();

            var GETExpeditedPayment = _db.expeditedPayments.FromSqlRaw("dbo.SP_GetExpeditedPaymen @CustomerId", ParamValue).AsNoTracking();

            //TODO change var to public

            sb = new StringBuilder();
            sb.Append(@"
           <html><head></ head>

  <link rel=""stylesheet"" href=""H:\MicrocreditDB\MicrocreditBackEnd\Microcredit\StaticFiles\bootstrap\css\bootstrap.css"">
<body>
       <label   style='direction: rtl; margin: 0 0 40px 0;  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2); '>ملحوظه* اذا كانت الحالة  صفر فهو قرض تحت التحرير
        1 مصدر
        2 مدفوع
        3 محذوف
        </label>
 <table align='center' style='direction: rtl;' class='table table-bordered'>

      <tr style='  direction: rtl;' >
      <th scope='col'>كود القرض</th>
      <th scope='col'>اسم الضامن</th>
      <th scope='col'>بداية القرض</th>
      <th scope='col'>كود الضامن</th>
      <th scope='col'>الرقم القومى</th>
      <th scope='col'>العنوان</th>
      <th scope='col'>الهاتف</th>
      <th>الفائده</th>
      <th>المبلغ</th>
      <th>عدد الاقساط</th>
      <th>المنتج</th>

      <th>الحالة</th>
      </tr>");
            foreach (var _GETLonaGuarantorNames in GETLonaGuarantorNames.ToList())
                sb.AppendFormat(@"<tr style=' direction: rtl; align='center'>
                        <td scope='col'>{0}</td>
                        <td scope='col'>{1}</td>
                        <td scope='col'>{2}</td>
                        <td scope='col'>{3}</td>
                        <td scope='col'>{4}</td>
                        <td scope='col'>{5}</td>
                        <td scope='col'>{6}</td>
                        <td scope='col'>{7}</td>
                        <td scope='col'>{8}</td>
                        <td scope='col'>{9}</td>
                        <td scope='col'>{10}</td>
                 


                        </tr> ",
         _GETLonaGuarantorNames.LonaId,
         _GETLonaGuarantorNames.CustomerName,
         _GETLonaGuarantorNames.StartDateLona,
         _GETLonaGuarantorNames.CustomerId,
         _GETLonaGuarantorNames.CustomerNationalid,
         _GETLonaGuarantorNames.CustomerAddress,
         _GETLonaGuarantorNames.FirstPhone,
         _GETLonaGuarantorNames.InterestRateName,
         _GETLonaGuarantorNames.AmountAfterAddInterest,
         _GETLonaGuarantorNames.MonthNumber,
         _GETLonaGuarantorNames.ProdouctName,
          _GETLonaGuarantorNames.StatusLona

);


            sb.Append(@"</table></body></html>");




            sb.Append(@"
    <html><head></ head><body>
    <label   style='direction: rtl; margin: 0 0 40px 0;  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2); '>ملحوظه* اذا كانت حالة القسط صفر فهو قسط غير مدفوع
     1 مدفوع
     2 مسدد جزئيا
      </label>



                  <table align='center' style='direction: rtl;' class='table table-bordered' >
                  <tr style=' direction: rtl;' >
                 <th>كود القرض</th>
                 <th>اسم العميل</th>
                 <th>مبلغ القسط </th>
                 <th>المبلغ المتبقى</th>
                 <th>المبلغ المدفوع</th>
                 <th> حالة القسط</th>

             
                   </tr>");
            foreach (var _GETAllInfoAboutcustomerLoan in GETAllInfoAboutcustomerLoan)

                sb.AppendFormat(@"<tr style=' direction: rtl; align='center'  >
             
                <td style='direction: rtl; align='center'>{0}</td>
                     <td >{1}     </td>
                     <td >{2}    </td>
                     <td >{3} </td>
                     <td >{4}    </td>
                     <td >{5} </td>
                     </tr> ",
                     
                     _GETAllInfoAboutcustomerLoan.LonaId,
                    _GETAllInfoAboutcustomerLoan.CustomerName,
                    _GETAllInfoAboutcustomerLoan.IstalmentsAmount,
                    _GETAllInfoAboutcustomerLoan.AmountRemaining,
                    _GETAllInfoAboutcustomerLoan.AmountPaid,
               _GETAllInfoAboutcustomerLoan.StatusIstalments
);



            sb.Append(@"
    <html><head></ head><body>
    <label   style='direction: rtl; margin: 0 0 40px 0;  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2); '>ملحوظه* اذا كانت حالة القسط صفر فهو قسط غير مدفوع
     1 مدفوع
     2 مسدد جزئيا
      </label>



 <table align='center' style='direction: rtl;' class='table table-bordered' >
 <tr style=' direction: rtl;' > 
<label   style='direction: rtl; margin: 0 0 40px 0;  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2); '>ملحوظه* اذا كانت حالة حالة القرض صفر فهو قرض مصدر
     1 مدفوع
       </label>
      <th>النسبئة %</th>
      <th>المبلغ قبل الخصم</th>
      <th>المبلغ بعد الخصم</th>
      <th>تاريخ سداد القرض</th>
      <th>حالة القرض</th>

             
                   </tr>");
            foreach (var _GETExpeditedPayment in GETExpeditedPayment)



                sb.AppendFormat(@"<tr style=' direction: rtl; align='center'  >
             
                <td style='direction: rtl; align='center'>{0}</td>
                     <td >{1}     </td>
                     <td >{2}     </td>
                     <td >{3}     </td>
                     <td >{4}     </td>

                    
                     </tr> ",
                     _GETExpeditedPayment.DiscountPercentage,
                _GETExpeditedPayment.AmountBeforeDiscount,
                _GETExpeditedPayment.AmountAfterDiscount,
            _GETExpeditedPayment.ExpeditedPaymentDate,
            _GETExpeditedPayment.StatusLona



);
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();
            sb.Append(@"</table></body></html>");
            GC.Collect();
            return sb.ToString();

        }


        public string GetHTMLStringWithoutParam()
        {
            throw new NotImplementedException();
        }

        public string GetHTMLStringWitmultipleParam([Optional] SqlParameter DateFrom, SqlParameter DateTo)
        {
            throw new NotImplementedException();
        }
    }
}

