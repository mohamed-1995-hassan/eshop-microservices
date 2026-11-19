namespace Shopping.Web.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?PageIndex={pageIndex}&PageSize={pageSize}")]
    Task<GetOrdersResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10);

    [Get("/ordering-service/orders/{orderName}")]
    Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);

    [Get("/ordering-service/order/customer/{customerId}")]
    Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
}
