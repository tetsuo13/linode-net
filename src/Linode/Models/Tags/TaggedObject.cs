using Linode.Models.Domains;
using Linode.Models.Linode;
using Linode.Models.NodeBalancers;
using Linode.Models.Volumes;

namespace Linode.Models.Tags;

// /// <summary>
// /// Object that has been tagged.
// /// </summary>
// public record TaggedObject<T>
// {
//     /// <summary>
//     /// Details on the specific object type the tag is assigned to.
//     /// </summary>
//     public required T Data { get; set; }
//
//     /// <summary>
//     /// The type of object the tag is applied to.
//     /// </summary>
//     public required TaggedObjectType Type { get; set; }
// }
//
// public readonly struct TaggedObject
// {
//     private readonly Domain? _domain;
//     private readonly LinodeInstance? _linodeInstance;
//     private readonly NodeBalancer? _nodeBalancer;
//     private readonly Volume? _volume;
//     private readonly TaggedObjectType? _tag;
//
//     public TaggedObject(Domain? value)
//     {
//         if (value is not null)
//         {
//             _domain = value;
//             _tag = TaggedObjectType.Domain;
//         }
//     }
//
//     public TaggedObject(LinodeInstance? value)
//     {
//         if (value is not null)
//         {
//             _linodeInstance = value;
//             _tag = TaggedObjectType.Linode;
//         }
//     }
//
//     public TaggedObject(NodeBalancer? value)
//     {
//         if (value is not null)
//         {
//             _nodeBalancer = value;
//             _tag = TaggedObjectType.NodeBalancer;
//         }
//     }
//
//     public TaggedObject(Volume? value)
//     {
//         if (value is not null)
//         {
//             _volume = value;
//             _tag = TaggedObjectType.Volume;
//         }
//     }
//
//     public object? Value => _tag switch
//     {
//         TaggedObjectType.Domain => _domain,
//         TaggedObjectType.Linode => _linodeInstance,
//         TaggedObjectType.NodeBalancer => _nodeBalancer,
//         TaggedObjectType.Volume => _volume,
//         _ => null
//     };
//
//     public bool HasValue => _tag is not null;
//
//     public bool TryGetValue(out Domain value)
//     {
//         value = _domain;
//         return _tag == TaggedObjectType.Domain;
//     }
//
//     public bool TryGetValue(out LinodeInstance value)
//     {
//         value = _linodeInstance;
//         return _tag == TaggedObjectType.Linode;
//     }
//
//     public bool TryGetValue(out NodeBalancer value)
//     {
//         value = _nodeBalancer;
//         return _tag == TaggedObjectType.NodeBalancer;
//     }
//
//     public bool TryGetValue(out Volume value)
//     {
//         value = _volume;
//         return _tag == TaggedObjectType.Volume;
//     }
// }
//
// public record TaggedObjectt
// {
//     public TaggedObject Data { get; init; }
//     public required TaggedObjectType Type { get; init; }
// }

/// <summary>
/// Object that has been tagged.
/// </summary>
public record TaggedObject
{
    /// <summary>
    /// Details on the specific object type the tag is assigned to.
    /// </summary>
    public required ITaggedObject Data { get; init; }

    /// <summary>
    /// The type of object the tag is applied to.
    /// </summary>
    public required TaggedObjectType Type { get; init; }
}
