using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Model
{
	public class TodoItem
	{
		public int Id { get; set; }
		
		[Required]
		public string Title { get; set; }=string.Empty;
		
		public bool IsCompeted {  get; set; }

		[Required]
		public int UserId { get; set; }

		[JsonIgnore]
		public User? User { get; set; } = null;
	}
}
