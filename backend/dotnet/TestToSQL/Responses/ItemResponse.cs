using TestToSQL.Interfaces;

namespace TestToSQL.Responses
{
    public class ItemResponse<T> : SuccessResponse, IItemResponse
    {
        public T Item { get; set; }
        public string TransactionId { get; set; }
        object IItemResponse.Item { get { return this.Item;  } }
    }
}
