using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Properties.DAL.Models
{
	public class ToDoItem
	{
		public int Id { get; set; }
		public string Activity { get; set; }
	}
}
