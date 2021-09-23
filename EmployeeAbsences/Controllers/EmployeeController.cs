using EmployeeAbsences.Models;
using EmployeeAbsences.Repo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAbsences.Controllers
{

	/// <summary>
	/// Hold Employee related operations
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		#region data member
		private readonly IEmployeeRepository employeeRepository;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="employeeRepository"></param>
		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		
		}

		/// <summary>
		/// return list of employees details
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Employees> List()
		{
			//do to - add try catch block to handle error and write into log
			return employeeRepository.GetEmployeelist();
		}

	}
}
