using EmployeeAbsences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAbsences.Repo
{

	/// <summary>
	/// Defined employee related operation
	/// </summary>
	public interface IEmployeeRepository
	{

		/// <summary>
		/// return list of employeees
		/// </summary>
		/// <returns></returns>
		IEnumerable<Employees> GetEmployeelist();

	}
}
