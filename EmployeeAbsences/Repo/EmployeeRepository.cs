using EmployeeAbsences.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeAbsences.Repo
{

	/// <summary>
	/// Contains employee related operations
	/// </summary>
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;

		/// <summary>
		/// this is Employee absence variable which will hold list of absence details
		/// </summary>
		private List<Absence> m_Absence;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="webHostEnvironment"></param>
		public EmployeeRepository(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;

		}

		/// <summary>
		/// return list of employee details
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Employees> GetEmployeelist()
		{
			try
			{
				List<Employees> oListEmpoyee = GetEmployeeDetailsFromCSV();
				if (oListEmpoyee.Any())
				{
					return oListEmpoyee;
				}
				else
				{
					//write log oListEmpoyee should not be empty
				}
				return oListEmpoyee;
			}
			catch (Exception)
			{
				//write log to read exception
				throw;
			}

		}

		/// <summary>
		/// return list of absences based on employee id and type
		/// </summary>
		/// <param name="aEmployeeId"></param>
		/// <param name="aType"></param>
		/// <returns></returns>
		public IEnumerable<Absence> GetEmployeeAbsences(int aEmployeeId, int aType)
		{
			try
			{
				IEnumerable<Absence> oAbsenceList = GetEmployeeAbsencesListFromCSV();
				if (oAbsenceList.Any())
				{
					return oAbsenceList.Where(m => m.EmployeeID == aEmployeeId && m.Type == (eDepartmentType)aType).ToList();
				}
				else
				{
					//write log that oAbsenceList is empty,expecting data
					return null;
				}
			}
			catch (Exception)
			{
				//write log to read exception
				throw;
			}
		}


		/// <summary>
		/// Read CSV file to get employee details
		/// get total employee absence 
		/// return list of employee object
		/// </summary>
		/// <returns></returns>
		private List<Employees> GetEmployeeDetailsFromCSV()
		{
			string wPath = Path.Combine(webHostEnvironment.ContentRootPath, "files\\employees.csv");
			ReadCSV(out List<string[]> csv, out string[] lines, out string[] properties, wPath);

			//initialize employee list object
			List<Employees> oListEmployee = new List<Employees>();

			//read absences file first to hold absence in the list object and when calculate employee absence
			//can easily read it from this list object instead reading file each time 
			m_Absence = GetEmployeeAbsencesListFromCSV();

			//make fore loop to read data of each column and row
			for (int i = 1; i < lines.Length; i++)
			{
				//create employee object to keep each row information
				Employees oEmployee = new Employees();
				for (int j = 0; j < properties.Length; j++)
				{
					//check header and push data to the equivalent employee prop
					switch (properties[j])
					{
						case "Id":
							oEmployee.ID = Convert.ToInt32(csv[i][j]);
							break;
						case "EmployeeNumber":
							oEmployee.EmployeeNumber = csv[i][j];
							break;
						case "Name":
							oEmployee.EmployeeName = csv[i][j];
							break;
						case "Department":
							oEmployee.Department = csv[i][j];
							break;
					}
				}
				//get total employee absence, used enum to differentiate department type
				TotalAbsence(out int TotalLeave, out List<Absence> AbsenceList, oEmployee.ID, (eDepartmentType)Enum.Parse(typeof(eDepartmentType), oEmployee.Department, true));
				oEmployee.Absences = AbsenceList;
				oEmployee.TotalAbsence = TotalLeave;
				oListEmployee.Add(oEmployee);
			}
			return oListEmployee;
		}

		/// <summary>
		/// return list of employee absence details
		/// </summary>
		/// <returns></returns>
		private List<Absence> GetEmployeeAbsencesListFromCSV()
		{
			//get file path
			var wPath = Path.Combine(webHostEnvironment.ContentRootPath, "files\\absences.csv");

			//read csv data and return header,cells data
			ReadCSV(out List<string[]> csv, out string[] lines, out string[] properties, wPath);


			List<Absence> oAbseneceList = new List<Absence>();

			for (int i = 1; i < lines.Length; i++)
			{
				Absence oAbsence = new Absence();
				for (int j = 0; j < properties.Length; j++)
				{
					//check header and push data to the equivalent Absence prop
					switch (properties[j])
					{
						case "Id":
							oAbsence.ID = Convert.ToInt32(csv[i][j]);
							break;
						case "Start":
							oAbsence.Start = Convert.ToDateTime(csv[i][j]);
							break;
						case "End":
							oAbsence.End = Convert.ToDateTime(csv[i][j]);
							break;
						case "Type":
							oAbsence.Type = (eDepartmentType)Convert.ToInt32(csv[i][j]);
							break;
						case "EmployeeId":
							oAbsence.EmployeeID = Convert.ToInt32(csv[i][j]);
							break;
					}
				}
				oAbseneceList.Add(oAbsence);
			}
			return oAbseneceList;
		}

		/// <summary>
		/// read csv data and return header and cell data
		/// </summary>
		/// <param name="csv"></param>
		/// <param name="lines"></param>
		/// <param name="properties"></param>
		/// <param name="aPath"></param>
		private void ReadCSV(out List<string[]> csv, out string[] lines, out string[] properties, string aPath)
		{
			//read file path

			//store csv data into string array to easy to read
			csv = new List<string[]>();

			//read csv file line by line
			lines = System.IO.File.ReadAllLines(aPath);

			//split each line by comma separate
			foreach (string line in lines)
				csv.Add(line.Split(','));

			//read header of the each column
			properties = lines[0].Split(',');
		}

		/// <summary>
		/// return total absence based on employee id and employee department type
		/// </summary>
		/// <param name="aIDEmployee"></param>
		/// <param name="aType"></param>
		/// <returns></returns>
		private void TotalAbsence(out int ToleAbsnce, out List<Absence> AbsenceList, int aIDEmployee, eDepartmentType aType)
		{
			//make a filter to m_Absence to return  absence list for aIDEmployee and aType
			AbsenceList = m_Absence.Where(m => m.EmployeeID == aIDEmployee && m.Type == aType).ToList();
		
			//get total absences
			ToleAbsnce = (from m in AbsenceList select Convert.ToInt32((m.End - m.Start).TotalDays)).Sum();
		}
	}
}
