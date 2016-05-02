var ViewModel = function () {
    var self = this;
    self.employees = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.newEmployee = {
        lName: ko.observable(),
        fName: ko.observable(),
        Title: ko.observable(),
        Address: ko.observable(),
        City: ko.observable(),
        Region: ko.observable(),
        PostalCode: ko.observable(),
        Country: ko.observable(),
        Ext: ko.observable(),
        Salary: ko.observable(),
        Dept: ko.observable(),
        Super: ko.observable(),
        Tenure: ko.observable()
    }

    var emURI = '/api/Employees/';

    self.addEmployee = function (formElement) {
        var employee = {
            lName: self.newEmployee.lName(),
            fName: self.newEmployee.fName(),
            Title: self.newEmployee.Title(),
            Address: self.newEmployee.Address(),
            City: self.newEmployee.City(),
            Region: self.newEmployee.Region(),
            PostalCode: self.newEmployee.PostalCode(),
            Country: self.newEmployee.Country(),
            Ext: self.newEmployee.Ext(),
            Salary: self.newEmployee.Salary(),
            Dept: self.newEmployee.Dept(),
            Super: self.newEmployee.Super(),
            Tenure: self.newEmployee.Tenure()
        };

        ajaxHelper(emURI, 'POST', employee).done(function (item) {
            self.employees.push(item);
        });
    }

    self.updateEmployee = function (formElement) {
        var updatedEmployee = {
            Id: formElement.Id,
            lName: formElement.lName,
            fName: formElement.fName,
            Title: formElement.Title,
            Address: formElement.Address,
            City: formElement.City,
            Region: formElement.Region,
            PostalCode: formElement.PostalCode,
            Country: formElement.Country,
            Ext: formElement.Ext,
            Salary: formElement.Salary,
            Dept: formElement.Dept,
            Super: formElement.Super,
            Tenure: formElement.Tenure
        };

        ajaxHelper(emURI + updatedEmployee.Id, 'PUT', updatedEmployee).done(function (item) {
            self.employees.push(item);
        });
    }

    self.getEmployeeDetail = function (item) {
        ajaxHelper(emURI + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.deleteEmployee = function (item) {
        ajaxHelper(emURI + item.Id, 'DELETE').done(function (data) {
            self.detail(data);
        });
    }

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllEmployees() {
        ajaxHelper(emURI, 'GET').done(function (data) {
            self.employees(data);
        });
    }

    // Fetch the initial data.
    getAllEmployees();
};

ko.applyBindings(new ViewModel());