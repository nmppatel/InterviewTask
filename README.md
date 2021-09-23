// Web API - Employee Absence API
Summary 

1. add those two csv files into project to read from project//files/csv file name
2. add classes for employee and absence
3. add repository and interface for employee details to put logics to read files and filter and return list of employee details with total absence.
4. add this employee details repository to startup class to use DJ
5. add api controller and httpget method to call repository >> get employee details with total absences and list of absences to view employee absences details on UI

// xUnit Test  - EMployeeAbsence.Test

// don't get chance to write unit test but I have put some comments on there that what was my plan

//MVC Application  - EmployeeAbsence.UI

Summary

1. add view model class to hold employee details.
2. add controller >> Index >> view >> show list of employee details 
3. Index action >> call web api to get employee details and deserialized json object to model class 
4. used jquery and datatable js to use paging/sorting/search/filter

below is the result of API return list of employees
![image](https://user-images.githubusercontent.com/11857448/134523958-1ef98f55-3e3e-4bb2-af26-5654f7244bd7.png)


below is the screenshot of result to display on UI 

![image](https://user-images.githubusercontent.com/11857448/134523630-8ea3b9e4-bfd7-41b0-8887-29734569b5e1.png)


