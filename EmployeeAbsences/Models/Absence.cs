using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAbsences.Models
{
	public class Absence
	{
		public int ID { get; set; }
		public int EmployeeID { get; set; }
		public eDepartmentType Type { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}

	public enum eDepartmentType
	{
		Development = 1,
		Sales = 2,
		Marketing = 3,
		QA = 4
	}
}
