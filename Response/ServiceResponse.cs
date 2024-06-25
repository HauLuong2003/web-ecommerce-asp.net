using Web_Ecommerce_Server.Model.Entity;

namespace Web_Ecommerce_Server.Response
{
    public record class ServiceResponse (bool Flag,string Message);
    public record class ProductServiceResponse(bool Flag,Product product);
}
