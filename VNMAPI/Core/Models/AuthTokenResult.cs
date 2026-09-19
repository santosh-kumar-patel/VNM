
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Models.Base;

namespace Core.Models
{
    public class AuthTokenResult : Response
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime Expiration { get; set; }

        [JsonIgnore]
        public List<Claim>? Claims { get; set; }

    }
}
