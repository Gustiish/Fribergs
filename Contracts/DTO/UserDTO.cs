namespace Contracts.DTO
{
    public sealed class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public List<OrderDTO> Orders { get; set; }


    }
}
