// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//function getAllSalaries() {
//    var dict = {
//        city = this["city"].value,
//        jobTitle = this["jobTitle"].value
//    }
//    $.get("http://localhost:55746/api/averagesalaries", function (data) {
//        for (let i = 0; i < data.length; i++) {
//            $("#salaryDisplay").append(
//                `<tr>
//                <td><strong>City</strong><td>
//                <td><strong>Job Title</strong><td>
//                <td><strong>Average Salary</strong><td>`
//            );
//            for (let i = 0; i < data.length; i++) {
//                $("#salaryDisplay").append(`<tr>
//                <td>${JSON.stringify(data[i].city).replace(/\"/g, '')}</td>
//                <td>${JSON.stringify(data[i].jobTitle).replace(/\"/g, '')}</td>
//                <td>${JSON.stringify(data[i].avgSalary).replace(/\"/g, '')}</td></tr>`
//                )
//            }

//        }
//    })
//} (jQuery);
    function getSalariesByTitle(e) {
        var jobTitle = $("#JobTitle").val();
        $.get("http://localhost:55746/api/AverageSalaries/" + jobTitle, function (data) {
            console.log(data);
            $("#salaryDisplay").html("");
                $("#salaryDisplay").append(
                `<thead>
                    <tr>
                        <th>City<th>
                        <th>Job Title<th>
                        <th>Average Salary<th>
                    </tr>
                </thead>`
            );
            for (let i = 0; i < data.length; i++) {
                $("#salaryDisplay").append(
                `<tbody>
                    <tr>
                        <td>${JSON.stringify(data[i].city).replace(/\"/g, '')}</td>
                        <td>${JSON.stringify(data[i].jobTitle).replace(/\"/g, '')}</td>
                        <td>${JSON.stringify(data[i].avgSalary).replace(/\"/g, '')}</td>
                    </tr>
                </tbody>`
                )
            }
        });
}
$("#submitSalarySearch").click(function (event) {
    event.preventDefault();
});
(jQuery);
