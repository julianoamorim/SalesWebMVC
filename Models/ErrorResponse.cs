
namespace SalesWebMVC.Models
{
    public class ErrorResponse
    {
        public bool Sucess { get; set; } = false;
        public int Status { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}