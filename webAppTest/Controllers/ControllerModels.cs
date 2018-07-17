using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAppTest.Services;
using Microsoft.AspNetCore.Mvc;
using webAppTest.Models;
using System.Diagnostics;
using MongoDB.Bson;

namespace webAppTest.Controllers {
	[Route("api/fromAtoB")]
	public class ModelsAPIController : Controller {
		ModelAService serviceAInstance;
		ModelBService serviceBInstance;

		public ModelsAPIController () {
			serviceAInstance = new ModelAService();
			serviceBInstance = new ModelBService();
		}

		[HttpGet("type/{type}")]
		public IActionResult Get (string type) {
			var modelsA = serviceAInstance.GetListFromType(type);
			if ( modelsA == null ) {
				return NotFound();
			}
			serviceBInstance.AddFromList(modelsA);
			return new OkResult();
		}
	}
}
