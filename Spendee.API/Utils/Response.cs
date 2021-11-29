namespace Spendee.API.Utils;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T Result { get; set; }
}
