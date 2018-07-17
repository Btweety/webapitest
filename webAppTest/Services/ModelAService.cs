using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using webAppTest.Models;

/*
 * Serviço para a primeira collection
 */

namespace webAppTest.Services {
	public class ModelAService {
		MongoClient _client;
		IMongoDatabase _db;
		IMongoCollection<Model> modelColl;

		/* Dados da ligação */
		string ip = "mongodb://192.168.4.78:27017";
		string database = "test";

		/* Iniciar ligação */
		public ModelAService () {
			_client = new MongoClient(ip);

			Debug.WriteLine("----------------------------------------");
			_db = _client.GetDatabase(database);
			modelColl = _db.GetCollection<Model>("ModelA");

			/** List DBs */
			/*
			var dbNameList = _client.ListDatabaseNames().ToList();
			foreach ( var item in dbNameList ) {
				Debug.WriteLine(item);
			}
			*/
			/** List collections */
			/*
			Debug.WriteLine("--------------------");
			Debug.WriteLine("Collections in " + database + ":");
			var colNameList =_db.ListCollectionNames().ToList();
			foreach ( var item in colNameList ) {
				Debug.WriteLine(item);
			}
			*/
		}

		/**
		 * Document based operations
		 */
		/* Obter uma lista com todos os documentos da collection */
		public IEnumerable<Model> GetModels () {
			return modelColl.Find(_ => true).ToList();
		}

		/* Obter um documento especifico a partir do objectID */
		public Model GetModel (ObjectId id) {
			var filter = new BsonDocument("_id", id);
			var res = modelColl.Find(filter);
			long num = res.CountDocuments();
			if ( num > 0 )
				return res.First();
			else
				return null;
		}

		/* Introduzir um novo documento na collection */
		public Model Create (Model p) {
			modelColl.InsertOne(p);
			return p;
		}

		/* Modificar um documento (identificado pelo objectID) da collection */
		public void Update (ObjectId id, Model p) {
			p.Id = id;
			var filter = Builders<Model>.Filter.Eq("_id", id);
			var result = modelColl.FindOneAndReplace(filter, p);
		}

		/* Remover um documento da collection */
		public void Remove (ObjectId id) {
			var filter = Builders<Model>.Filter.Eq("_id", id);
			var operation = modelColl.DeleteOne(filter);
		}

		/* obter uma lista com todos os elementos com o mesmo tipo */
		public List<Model> GetListFromType (String type) {
			var filter = Builders<Model>.Filter.Eq("Type", type);
			var operation = modelColl.Find(filter);
			return operation.ToList<Model>();
		}

		/* adicionar a partir de uma lista */
		public void AddFromList (List<Model> models) {
			modelColl.InsertMany(models);
		}

	}
}