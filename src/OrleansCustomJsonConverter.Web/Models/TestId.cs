using System.Text.Json.Serialization;

namespace OrleansMultiClusterExample.Web.Models;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<TestId, Guid>))]
public record TestId(Guid Id) : StronglyTypedId<Guid>(Id);