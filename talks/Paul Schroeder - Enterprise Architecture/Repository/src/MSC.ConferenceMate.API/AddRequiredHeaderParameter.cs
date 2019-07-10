using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace MSC.ConferenceMate.Repository.API
{
	public class AddRequiredHeaderParameter : IOperationFilter
	{
		public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
		{
			if (operation.parameters == null)
				operation.parameters = new List<Parameter>();

			operation.parameters.Add(new Parameter
			{
				name = "api-version",
				@in = "header",
				type = "string",
				description = "API Version",
				required = true,
				@default = "1"
			});
		}
	}
}