using TeamTaskManagement.Services.Helper;

namespace TeamTaskManagement.Services.Dtos
{
    public class ResponseBaseModel
    {
        public ResponseBaseModel()
        {
            Status = StatusResult.Ok;
        }
        public ResponseBaseModel(StatusResult status, string error = "", string success = "")
        {
            Status = status;
            Error = error;
            Success = success;
        }
        public StatusResult? Status { get; set; }
        public string? Error { get; set; }
        public string? Success { get; set; }
        public StatusType? StatusType { get; set; }
        public object? AdditionalData { get; set; }
    }
}
