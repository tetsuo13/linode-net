namespace Linode.Models;

public enum DomainType
{
    /// <summary>
    /// Authoritative source of information for the domain it describes.
    /// </summary>
    Master,

    /// <summary>
    /// A read-only copy of a master.
    /// </summary>
    Slave
}
