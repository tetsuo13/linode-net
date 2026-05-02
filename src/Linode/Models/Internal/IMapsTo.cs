namespace Linode.Models.Internal;

internal interface IMapsTo<out T>
{
    T ToDomain();
}
