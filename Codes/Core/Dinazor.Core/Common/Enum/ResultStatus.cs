
namespace Dinazor.Core.Common.Enum
{
    public enum ResultStatus
    {
        UnknownError = -1,
        Success = 0,
        ForeignKeyConstraint = 1,
        MissingRequiredParamater = 2,
        InValidParamater = 3,
        Unauthorized = 4,
        NoSuchObject = 5,
        AlreadyAdded = 6,
        IndexOutOfBound = 7,
        NoPrimaryKey = 8,
        UnsupportedOperation = 9,
        LoginFailed = 10,
        SessionNotValid = 11,
        PropertyNotFound = 12,
        UnQualifiedPrivilege = 13,
        SessionTimeout = 14,
        InMemoryDatabaseError = 15,
        SerializationError = 16,
        NoLicence = 17,
        AlreadyDeleted = 18,
        DeleteControlError = 19,
        HasMessage = 20,
        BadData = 21,
        AllOfDataNotCommitted = 22,
        TransactionRollback = 23
    }
}
