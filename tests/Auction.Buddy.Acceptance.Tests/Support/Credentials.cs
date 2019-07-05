namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class Credentials
    {
        public static readonly Credentials AdminCredentials = new Credentials("bill@somewhere.com", "abc123!");

        public string Username { get; }
        public string Password { get; }

        private Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}