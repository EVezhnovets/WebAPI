using WebAPI.Data.Models;

namespace WebAPI.Data.ViewModels
{
    public class CustomActionResultVM
    {
        public Exception? Exception { get; set; }
        public Publisher? Publisher{ get; set; }  
    }
}