namespace CoreApi.ApiResponse
{
    public class ApiResponse<T>
    {
         
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string Status { get; set; }
        public string ExceptionMessage { get; internal set; }
    }

}
