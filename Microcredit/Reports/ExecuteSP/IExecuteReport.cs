using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP.ModelRepor;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Microcredit.Reports.ExecuteSP
{


    // Inheritance has been made because when using the same interface, the system accesses one class and does not go to the other class to be accessed
    public interface IExecuteReport
    {
        
        public IEnumerable<T> ExecuteReportSP<T>(string SPName, [Optional] SqlParameter ParamValue);

        // public IEnumerable<AddNewLoanObjectModelReport> ExecuteReportSP(string SPName, [Optional] SqlParameter ParamValue);

        public string GetHTMLString ([Optional] SqlParameter ParamValue);
        public string GetHTMLStringWithoutParam();



    }

    public interface IExecutCountCustomersReport : IExecuteReport { }

    public interface IExecuteReportWithmultipleParam : IExecuteReport

    {
    public string GetHTMLStringWitmultipleParam([Optional] SqlParameter DateFrom, SqlParameter DateTo);

}

    public interface IExecuteReportDuelmentsbetweenDate : IExecuteReportWithmultipleParam
    {

    }

    public interface IExecuteReportIssuanceLoansbetweenDate : IExecuteReportDuelmentsbetweenDate { }

    public interface IAllIStatusLoansReport : IExecuteReport
    {
        public string GetCountLoansUnderEdit();

    }



}
