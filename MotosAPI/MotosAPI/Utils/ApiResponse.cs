namespace MotosAPI.Utils
{
    public class ApiResponse
    {
        public int Status { get; set; } 

        public string Title { get; set; } = "";

        public object Data { get; set; } = new { };
    }
}
