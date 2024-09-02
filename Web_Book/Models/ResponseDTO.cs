namespace Web_Book.Models
{
	public class ResponseDTO
	{
		public bool IsSuccess { get; set; } = true;
		public Object Result { get; set; }
		public string DisplayMessage { get; set; } = "Empty";
		public List<string> ErrorMessages { get; set; }
	}
}
