using System.Collections.Generic;

namespace FishBuy.Application.Interfaces
{
    public class ApplicationServiceResponse
    {
        public virtual object Result { get; set; }
        public virtual int StatusCode { get; set; }
        public virtual IEnumerable<string> Messages { get; set; }
    }


    public class ApplicationServiceResponse<T> : ApplicationServiceResponse
    {
        public new T Result { get; set; }
    }

}
