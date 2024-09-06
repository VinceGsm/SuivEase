namespace Business.User;

// Add properties to this class and update the server and client AuthenticationStateProviders
// to expose more information about the authenticated user to the client.
public class UserInfo
{
    public required Guid UserId { get; set; }    
    public required string Email { get; set; }
    public required string Name { get; set; }
}
