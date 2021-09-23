using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAbsences.Models
{
	public class Employees
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string EmployeeNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string EmployeeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Department { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int TotalAbsence { get; set; }

		public IEnumerable<Absence> Absences { get; set; }
	}
}
