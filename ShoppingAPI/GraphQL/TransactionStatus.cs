namespace ShoppingAPI.GraphQL
{ 
    public record TransactionStatus
     (
        bool IsSucceed,
        string? Message
    );

}
