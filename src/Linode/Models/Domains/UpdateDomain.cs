using Linode.Models.Domains.Internal;
using Linode.Models.Internal;

namespace Linode.Models.Domains;

public sealed record UpdateDomain
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
    public DomainType Type { get; set; }

    internal UpdateDomainRequest ToRequest() => new()
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
