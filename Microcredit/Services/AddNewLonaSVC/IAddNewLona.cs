using Microcredit.Models;
using Microcredit.ModelService;
using Microsoft.Data.SqlClient;
using ModelService;
using System.Runtime.InteropServices;
 namespace Microcredit.Services.AddNewLonaSVC
{
    public interface IAddNewLona
    {

        IEnumerable<TrackLoanObjectModel> GetAllLonaAsync(string SPName);

        IEnumerable<UpdateLoanObjectModel> GetLonaByidAsync(string SPName, [Optional] SqlParameter ParamValue);
 
        IEnumerable<TrackLonaWithGuarantorId> trackLonaWithGuarantorIds(string SPName, [Optional] SqlParameter ParamValue);
        
        Task<ResponseObject> CreateNewLonaMaster(AddNewLoanObjectModel addNewLoanObjectModel);
        IEnumerable<SearchLonaGuarantorStatusT> SearchLonaGuarantorStatuses(string SPName, [Optional] SqlParameter ParamValue);
        IEnumerable<SearchCanCustomerBeGuanantorT> SearchcanCustomerBeGuanantorStatuses(string SPName, [Optional] SqlParameter ParamValue);
         
         Task<ResponseObject> CreateNewLona( AddNewLoanObjectModel addNewLoanObjectModel);
         
        IEnumerable<AddNewLoanObjectModel> addNewLoanObjectModels(string SPName, [Optional] SqlParameter ParamValue);
        Task<ResponseObject> UpdateMasterLonaAsync(int LonaId, AddNewLonaMasterModel addNewLonaMasterModel);

        Task<ResponseObject> UpdateLona(AddnewLonaDetailsModel addnewLonaDetailsModel);

        //Task<ResponseObject> UpdateLona(int LonaId, int LonaGuarantorId);

        Task<ResponseObject> IssuanceLonaAsync(IssuanceLonaModel issuanceLonaModel);

        Task<bool> DeleteLonaAsync(int lonaId);
        Task<bool> DeleteLonaDetailsAsync(int lonaId);

        Task<bool> DeleteLonaGuarantorAsync(int lonaDetailsId);

        Task<ResponseObject> ChangeStatusMasterLona(int LonaId);

    }
}
