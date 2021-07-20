namespace CommonUtilities.Serializers
{
    public interface ISerializer
    {
        string Serialize<T>(T item);
        T Deserialize<T>(string item);
    }
}
