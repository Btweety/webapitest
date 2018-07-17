using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webAppTest.Services;
using webAppTest.Models;
using MongoDB.Bson;
using System.Diagnostics;

/*
 * Controlador para a segunda collection
 */

namespace webAppTest.Controllers {

	[Route("api/modelB")]
	public class ControllerB : Controller {
		ModelBService serviceBInstance;

		public ControllerB () {
			serviceBInstance =  new ModelBService();
		}

		/** obter todos os documentos */
		[HttpGet]
		public IEnumerable<Model> Get () {
			return serviceBInstance.GetModels();
		}

		/** obter um documento segundo o ID */
		[HttpGet("{id:length(24)}")]
		public IActionResult Get (string id) {
			var model = serviceBInstance.GetModel(new ObjectId(id));
			if ( model == null ) {
				return NotFound();
			}
			return new ObjectResult(model);
		}

		/** obter uma lista segundo o tipo */
		[HttpGet("type/{type}")]
		public IActionResult GetListOnType (string type) {
			var model = serviceBInstance.GetListFromType(type);
			if ( model == null ) {
				return NotFound();
			}
			return new ObjectResult(model);
		}

		/** inserir um documento */
		[HttpPost]
		public IActionResult Post ([FromBody]Model p) {
			if ( p == null ) {
				Debug.WriteLine("ERROR: Null Model");
				return new BadRequestObjectResult(p);
			}
			serviceBInstance.Create(p);
			return new OkObjectResult(p);
		}

		/** update/substituir documento */
		[HttpPut("{id:length(24)}")]
		public IActionResult Put (string id, [FromBody]Model p) {
			if ( p == null ) {
				Debug.WriteLine("ERROR: Null Model");
				return new BadRequestObjectResult(p);
			}
			var recId = new ObjectId(id);
			var model = serviceBInstance.GetModel(recId);
			if ( model == null ) {
				return NotFound();
			}
			serviceBInstance.Update(recId, p);
			return new OkResult();
		}

		/** remover documento */
		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete (string id) {
			var model = serviceBInstance.GetModel(new ObjectId(id));
			if ( model == null ) {
				return NotFound();
			}

			serviceBInstance.Remove(model.Id);
			return new OkResult();
		}
	}
}
