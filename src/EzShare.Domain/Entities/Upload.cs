using EzShare.Domain.Common;

namespace EzShare.Domain.Entities;

public class Upload : BaseEntity
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public Guid? UserId { get; set; }
    public User? User { get; set; } = null!;
}