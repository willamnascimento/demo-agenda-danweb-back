namespace Solucao.Application.Contracts.Requests
{
    public class ChangeUserPasswordRequest
	{
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

