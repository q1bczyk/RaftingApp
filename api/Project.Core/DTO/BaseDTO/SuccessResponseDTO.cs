namespace Project.Core.DTO.BaseDTO
{
    public class SuccessResponseDTO
    {
        public string Message { get; set; }

        public SuccessResponseDTO(string message)
        {
            Message = message;
        }
    }
}