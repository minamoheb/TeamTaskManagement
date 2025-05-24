


using TeamTaskManagement.Services.Helper;

namespace TeamTaskManagement.Services.Dtos
{
    public class ResponseBase<T> : ResponseBaseModel
    {
        public ResponseBase() : base()
        {

        }
        public ResponseBase(StatusResult status) : base(status)
        {
        }
        public T Result { get; set; }
    }
}
