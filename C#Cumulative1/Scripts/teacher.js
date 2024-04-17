// AJAX for teacher Add 
// This file is connected to the project via Shared/_Layout.cshtml

 
function AddTeacher() {
	var outputmsg = document.getElementById("msg");
	//goal: send a request which looks like this:
	//POST : http://localhost:44396/api/TeacherData/AddTeacher
	//with POST data of TeacherFname, TeacherLname, EmployeeNumber, HireDate,Salary 

	var URL = "https://localhost:44396/api/TeacherData/AddTeacher/";

	var xhr = new XMLHttpRequest();
	
	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"HireDate":HireDate,
		"Salary": Salary
	};


	xhr.open("POST", URL, true);
	xhr.setRequestHeader("Content-Type", "application/json");
	xhr.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (xhr.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	xhr.send(JSON.stringify(TeacherData));
	outputmsg.innerHTML = "Teacher successfully Added";
	outputmsg.style.color = "Green";
}



function DeleteTeacher(id) {
	var message = document.getElementById("outputmsg");
	//goal: send a request which looks like this:
	//POST : http://localhost:44396/api/TeacherData/DeleteTeacher
	//with POST data of TeacherFname, TeacherLname, EmployeeNumber, HireDate,Salary

	var URL = "https://localhost:44396/api/TeacherData/DeleteTeacher/"+id;

	var xhr = new XMLHttpRequest();
	


	xhr.open("POST", URL, true);
	
	xhr.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (xhr.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	xhr.send(null);
	message.innerHTML = "Teacher successfully Deleted";
	message.style.color = "red";
}


function UpdateTeacher(id){
	console.log("hello");
	var message = document.getElementById("msg");
	//goal: send a request which looks like this:
	//POST : http://localhost:44396/api/TeacherData/UpdateTeacher/{id}
	//with POST data of TeacherFname, TeacherLname, EmployeeNumber, HireDate,Salary

	var URL = "https://localhost:44396/api/TeacherData/UpdateTeacher/" + id;

	var xhr = new XMLHttpRequest();

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"HireDate": HireDate,
		"Salary": Salary
	};


	xhr.open("POST", URL, true);
	xhr.setRequestHeader("Content-Type", "application/json");
	xhr.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (xhr.readyState == 4 && xhr.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	xhr.send(JSON.stringify(TeacherData));
	message.innerHTML = "Teacher successfully  Updated";
	message.style.color = "Green";

}