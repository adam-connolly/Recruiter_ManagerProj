function getAllSalaries() {
    $.get("http://localhost:55746/api/averagesalaries", function (data) {
        for (let i = 0; i < data.length; i++) {
            $("#salaryDisplay").append(
                `<tr>
                <td><strong>City</strong><td>
                <td><strong>Job Title</strong><td>
                <td><strong>Average Salary</strong><td>`
            );
            for (let i = 0; i < data.length; i++) {
                $("#salaryDisplay").append(`<tr>
                <td>${JSON.stringify(data[i].city).replace(/\"/g, '')}</td>
                <td>${JSON.stringify(data[i].jobTitle).replace(/\"/g, '')}</td>
                <td>${JSON.stringify(data[i].avgSalary).replace(/\"/g, '')}</td></tr>`
            )}
        
        }
    })
} (jQuery);
function getSalariesByTitle(jobTitle) {
    $.get("http://localhost:55746/api/averagesalaries" + jobTitle, function (data) {
        for (let i = 0; i < data.length; i++) {
            $("#salaryDisplay").append(`<tr>
            <td>${JSON.stringify(data[i].city).replace(/\"/g, '')}</td>
            <td>${JSON.stringify(data[i].jobTitle).replace(/\"/g, '')}</td>
            <td>${JSON.stringify(data[i].avgSalary).replace(/\"/g, '')}</td></tr>`
            )
        }
        )
}