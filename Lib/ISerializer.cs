namespace Lib
{
    public interface ISerializer
    {
        string Serialize(object item);

        object Deserialize(string str);

        T Deserialize<T>(string str);
    }
}
