namespace MoveX.Domain.Entities.Drivers;

public class DriverDocument
{
    public long Id { get; set; }
    public int DriverId { get; set; }
    public DriverDocumentType DocumentType { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string? DocumentNumber { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DocumentVerificationStatus Status { get; set; } = DocumentVerificationStatus.Pending;
    public string? VerifiedByAdminUserId { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? RejectionReason { get; set; }

    public DriverProfile? Driver { get; set; }
}

public enum DriverDocumentType
{
    IdentityDocument,
    DriversLicence,
    ProfessionalDrivingPermit,
    VehicleLicence,
    RoadworthyCertificate,
    Insurance,
    VehicleOwnership,
    OperatingPermit,
    CompanyRegistration,
    Other
}

public enum DocumentVerificationStatus
{
    Pending,
    Approved,
    Rejected,
    Expired
}
