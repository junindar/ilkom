namespace Blazor_BiometricPWA.Models.Responses
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ResponseModel<T> : ResponseModel where T : class
    {
        public T Result { get; set; }
    }
    public class ListResponseModel<T> : ResponseModel where T : class
    {
        public List<T> Result { get; set; }
    }
}
