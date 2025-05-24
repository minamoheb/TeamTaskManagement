namespace TeamTaskManagement.Services.Helper
{
    public enum StatusType
    {
        Success = 1,
        Warning = 3,
        Error = 2
    }

    public enum StatusResult
    {
        Ok = 200,
        InternalServerError = 500,
        BadRequest = 400,
        UnAuthorized = 401,
        Failed = 402,
        FailedNoDataFound = 403,
        NotExists = 405,
        UserCreated = 201,
        Authunticated = 202,
        ForceReset = 203,
        LoginNameUsed = 204,
        OTPExpired = 205,
        ForgetPasswordNotAvailableToOwners = 206,
        RegisterationFilesCorrupted = 207,
        EmailUsed = 208,
        InvalidOtp = 209,
        RegisterationFailed = 406,
        EditProfileFailed = 407,
        ModalNotValid = 502,
        RequestUnderProcess = 1001,
        NotConsented = 3001,
        NotFoundInActiveDirectory = 3002,
        NotFoundUserandPasswordAuthuntication = 3003,
        InvalidRequest = 3004,
        AccountExpired = 3005,
        UnAuthorizedClient = 5001,
        InValidClient = 5002,
        Lockedout = 5003,
        NotALLowedDelete = 5004,
        Infected = 5005,
        AccountLocked = 53,
        InvalidCredential = 49,
        PleasecontactQFIUadministrator = 1,
        ReferenceNumberAlreadyExist = 5006

    }
    public enum LookupEnum
    {
        Status = 1,
        Priority = 2
    }




}
