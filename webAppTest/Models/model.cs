using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webAppTest.Models {

	public class Model {
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement("ModelID")]
		public int ModelID { get; set; }

		[BsonElement("Type")]
		public string Type { get; set; }

		[BsonElement("Name")]
		public string Name { get; set; }
	}
}

/**
modeltest:
	modelID
	type
	name
*/
