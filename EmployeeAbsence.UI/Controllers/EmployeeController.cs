using EmployeeAbsence.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAbsence.UI.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	public class EmployeeController : Controller
	{
		private readonly ILogger<EmployeeController> _logger;
		private readonly IConfiguration _Configure;
		private readonly string apiBaseUrl;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="configuration"></param>
		public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_Configure = configuration;
			//hold to get api base url from app config 
			apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
		}

		/// <summary>
		/// call web api to get list of employee details and 
		/// return view to display employee details
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			List<EmployeeViewModel> oModel = new List<EmployeeViewModel>();
			try
			{
				// create http client connection to call api
				using (HttpClient httpClient = new HttpClient())
				{
					//call api to get employee list
					using var response = await httpClient.GetAsync(apiBaseUrl + "/List");
					//read service response in json string
					string apiResponse = await response.Content.ReadAsStringAsync();
					//convert json string to employee model object
					oModel = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(apiResponse);
				}
				return View(oModel);
			}
			catch (System.Exception oExc)
			{
				_logger.LogError(oExc.Message);
				//add model error log to read message and return view instead throwing exception
				throw;
			}
		}
	}
}
