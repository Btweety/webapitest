using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webAppTest.Services;
using webAppTest.Models;
using MongoDB.Bson;
using System.Diagnostics;

/*
 * Controlador para a primeira collection
 */
namespace webAppTest.Controllers {

	[Route("api/modelA")]
	public class ControllerA : Controller {
		ModelAService serviceAInstance;

		public ControllerA () {
			serviceAInstance = new ModelAService();
		}

		/** obter todos os documentos */
		[HttpGet]
		public IEnumerable<Model> Get () {
			return serviceAInstance.GetModels();
		}

		/** obter um documento segundo o ID */
		[HttpGet]
		public IActionResult Get (string id) {
			var model = serviceAInstance.GetModel(new ObjectId(id));
			if ( model == null ) {
				return NotFound();
			}
			return new ObjectResult(model);
		}

		/** obter uma lista segundo o tipo */
		[HttpGet("type/{type}")]
		public IActionResult GetListOnType (string type) {
			var model = serviceAInstance.GetListFromType(type);
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
			serviceAInstance.Create(p);
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
			var model = serviceAInstance.GetModel(recId);
			if ( model == null ) {
				return NotFound();
			}
			serviceAInstance.Update(recId, p);
			return new OkResult();
		}

		/** remover documento */
		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete (string id) {
			var model = serviceAInstance.GetModel(new ObjectId(id));
			if ( model == null ) {
				return NotFound();
			}

			serviceAInstance.Remove(model.Id);
			return new OkResult();
		}
	}
}
