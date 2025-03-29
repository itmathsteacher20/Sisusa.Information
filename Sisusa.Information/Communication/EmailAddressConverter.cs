using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sisusa.Information.Communication;

public class EmailAddressConverter : JsonConverter<EmailAddress>
{
    public override EmailAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string emailString =
            reader.GetString() ?? throw new JsonException("Email address cannot be null or empty.");
        return new EmailAddress(emailString);
    }

    public override void Write(Utf8JsonWriter writer, EmailAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}