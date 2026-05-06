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

    public bool IsValid
    {
        get
        {
            switch (Type)
            {
                case DomainRecordType.A when string.IsNullOrEmpty(Target):
                case DomainRecordType.AAAA when string.IsNullOrEmpty(Target):
                case DomainRecordType.SRV when !Port.HasValue || !Priority.HasValue || string.IsNullOrEmpty(Service):
                case DomainRecordType.CAA when !Tag.HasValue:
                case DomainRecordType.NS when string.IsNullOrEmpty(Target):
                case DomainRecordType.CNAME when string.IsNullOrEmpty(Target) || string.IsNullOrEmpty(Name):
                case DomainRecordType.TXT when string.IsNullOrEmpty(Target):
                case DomainRecordType.PTR when string.IsNullOrEmpty(Target):
                    return false;

                default:
                    return true;
            }
        }
    }

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
