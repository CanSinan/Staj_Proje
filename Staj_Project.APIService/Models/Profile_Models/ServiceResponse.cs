using System.ComponentModel.DataAnnotations;

namespace Staj_Project.APIService.Models.Profile_Models
{
    public class ServiceResponse
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
}
