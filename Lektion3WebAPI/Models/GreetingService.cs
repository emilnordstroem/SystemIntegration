namespace Lektion3WebAPI.Models
{
    public class Greetingservice : IGreetingService
    {
        public string CreateCreeting(string name)
        {
            return $"Hello from server, {name}!";
        }
    }
}
