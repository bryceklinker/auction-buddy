using System;

namespace Auction.Buddy.Core.Authentication.Dtos
{
    public class AuthenticationResultDto
    {
        public bool IsSuccess { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public string TokenType { get; set; }
    }
}