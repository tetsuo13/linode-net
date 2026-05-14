using Linode.Models.Domains.Internal;
using Linode.Models.Internal;

namespace Linode.Models.Domains;

public record CreateDomainRecord
{
    public string? Name { get; set; }
    public int? Port { get; set; }
    public int? Priority { get; set; }
    public string? Protocol { get; set; }
    public string? Service { get; set; }
    public DomainRecordTag? Tag { get; set; }
    public string? Target { get; set; }
    public int? TtlSec { get; set; }
    public required DomainRecordType Type { get; set; }
    public int? Weight { get; set; }

    internal CreateDomainRecordRequest ToRequest() => new()
    {
        Name = Name,
        Port = Port,
        Priority = Priority,
        Protocol = Protocol,
        Service = Service,
        Tag = Tag,
        Target = Target,
        TtlSec = TtlSec,
        Type = Type,
        Weight = Weight
    };
}
