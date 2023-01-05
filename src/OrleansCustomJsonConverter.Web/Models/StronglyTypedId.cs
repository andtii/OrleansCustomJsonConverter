namespace OrleansCustomJsonConverter.Web.Models;
public abstract record StronglyTypedId<TValue>(TValue Value)
    where TValue : notnull
{
    public override string ToString() => Value.ToString();
}
