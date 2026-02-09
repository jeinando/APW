namespace APW.API;

public class ComplexObject
{
    public string Identifier { get { return _uniqueIdentifier; } }

    private string _uniqueIdentifier { get; set; } = Guid.NewGuid().ToString();

    public List<string> Errors { get; set; } = [];
}
