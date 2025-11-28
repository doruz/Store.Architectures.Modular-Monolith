namespace Store.Presentation.Api.IntegrationTests;

public sealed class NewOrderTestModel
{
    public List<NewOrderLineTestModel> Lines { get; } = [];

    public NewOrderTestModel Apples(int quantity)
        => With(TestProducts.Apples.Id, quantity);

    public NewOrderTestModel Bananas(int quantity)
        => With(TestProducts.Bananas.Id, quantity);

    public NewOrderTestModel With(string productId, int quantity)
    {
        Lines.Add(new NewOrderLineTestModel(productId, quantity));
        return this;
    }
}

public record NewOrderLineTestModel(string ProductId, int Quantity);