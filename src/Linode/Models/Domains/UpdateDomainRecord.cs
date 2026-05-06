using Linode.Models.Domains.Internal;

namespace Linode.Models.Domains;

public record UpdateDomainRecord
{
    public string? Name { get; init; }
    public int? Port { get; init; }
    public int? Priority { get; init; }
    public string? Protocol { get; init; }
    public string? Service { get; init; }
    public DomainRecordTag? Tag { get; init; }
    public string? Target { get; init; }
    public int? TtlSec { get; init; }
    public int? Weight { get; init; }

    internal UpdateDomainRecordRequest ToRequest() => new()
    {
        Name = Name,
        Port = Port,
        Priority = Priority,
        Protocol = Protocol,
        Service = Service,
        Tag = Tag,
        Target = Target,
        TtlSec = TtlSec,
        Weight = Weight
    };
}
