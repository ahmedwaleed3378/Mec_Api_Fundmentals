using Mec_Api_Fundmentals.Models;
using Mec_Api_Fundmentals.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mec_Api_Fundmentals.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MecController : BaseController<Product>
	{
		public MecController(IGenericRepository<Product> repository) : base(repository) { }
	}
}
