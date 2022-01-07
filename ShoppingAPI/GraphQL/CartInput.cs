namespace ShoppingAPI.GraphQL
{
    public record CartInput
   (
        int? CartId,
        int ProductId,
        string Username,
        int Quantity);
}
