namespace GameStore.APi;

public class ApiResponse<T>
{
    public bool Result { get; set; }
    public string Reason { get; set; }
    public T Data { get; set; }

    public ApiResponse(bool result, string reason, T data)
    {
        Result = result;
        Reason = reason;
        Data = data;
    }
}

