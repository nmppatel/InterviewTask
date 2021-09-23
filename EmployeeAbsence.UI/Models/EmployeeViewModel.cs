using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAbsence.UI.Models
{
	public class EmployeeViewModel
	{ /// <summary>
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
	}
}
