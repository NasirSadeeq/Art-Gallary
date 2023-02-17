

using Art_Gallery.Models;

namespace Art_Gallery.Interface;
public interface Iadmin
{
    Task<Registration> AddUser(Registration registration);
    Task<Admin> UpdateProfile(Admin Details);
    Task<Registration> GetUser(string Email);
     Task<IEnumerable<Registration>> GetAllUsers();
     Task DeleteUser(string Email);
     Task<IEnumerable<Order>> GetAllOders();
     Task<Order> GetOrder(string OrderId);
     Task<Order> UpdateOrderStatus(Order Details);

}