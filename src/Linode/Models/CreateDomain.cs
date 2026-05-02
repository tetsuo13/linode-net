using Linode.Models.Internal;

namespace Linode.Models;

public class CreateDomain
{
    public List<string> AxfrIps { get; set; }
    public string? Description { get; set; }
    public required string DomainName { get; set; }
    public int ExpireSec { get; set; }
    public List<string> MasterIps { get; set; }
    public int RefreshSec { get; set; }
    public int RetrySec { get; set; }
    public string SoaEmail { get; set; }
    public DomainStatus Status { get; set; }
    public List<string> Tags { get; set; }
    public int TtlExp { get; set; }
    public required DomainType Type { get; set; }

    public bool IsValid => Type == DomainType.Slave ||
                           Type == DomainType.Master && !string.IsNullOrEmpty(SoaEmail);

    internal CreateDomainRequest ToRequest() => new()
    {
        AxfrIps = AxfrIps,
        Description = Description,
        Domain = DomainName,
        ExpireSec = ExpireSec,
        MasterIps = MasterIps,
        RefreshSec = RefreshSec,
        RetrySec = RetrySec,
        SoaEmail = SoaEmail,
        Status = Status,
        Tags = Tags,
        TtlExp = TtlExp,
        Type = Type
    };
}
