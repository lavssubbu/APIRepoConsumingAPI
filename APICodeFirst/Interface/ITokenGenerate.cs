using APICodeFirst.Models;

namespace APICodeFirst.Interface
{
    public interface ITokenGenerate
    {
        public string GenerateToken(User user);
    }

}
