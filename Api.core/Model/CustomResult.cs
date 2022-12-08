using Domain.Shared.Enum;

namespace Repo.WebApi.Model
{
    public class CustomResult
    {
        public object Data { get; set; }   
        public StatusCodeEnum Status { get; set; }   
    }
}
