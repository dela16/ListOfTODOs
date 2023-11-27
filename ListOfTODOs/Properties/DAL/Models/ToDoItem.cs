using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Properties.DAL.Models
{
	public class ToDoItem
	{
		//[BsonElement("id")]
		//[BsonId]
		public int Id { get; set; }

		//[BsonElement("activity")]
		public string activity { get; set; }
	}
}
