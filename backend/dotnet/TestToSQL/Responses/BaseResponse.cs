namespace TestToSQL.Responses
{
    public abstract class BaseResponse
    {
        public bool IsSuccessful { get; set; }
        public string TransacationId { get; set; }

        public BaseResponse()
        {
            this.TransacationId = Guid.NewGuid().ToString();
        }
    }
}
