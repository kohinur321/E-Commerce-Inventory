using E_Shop.Models.Admin;
using E_Shop.Models.User;

namespace E_Shop.Utilities
{
    public class ResponseStatus
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success
        {
            get
            {
                 return StatusCode == 1;
            }
        }
        public virtual CustomerModel Customer { get; set; }
        public virtual OrderModel Order { get; set; }
    }
}
