namespace Project.Core.DTO.Auth
{
    public class LoggedUserDTO : BaseAuthDTO
    {
        public string Id { get; set; }
        public string Token { get; set; }
    }
}