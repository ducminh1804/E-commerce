let table = new DataTable('#myTable', {
    "ajax": {
        "url": "http://localhost:5159/api/HangHoa/get-all-hanghoa",
        "type": "GET",
        "dataType": "json",
        "dataSrc": ""
    },
    "columns": [
        { "data": "maHH" },
        { "data": "tenHH" },
        { "data": "donGia" },
        {
            "data": "maHH",
            "render": function (data, type, row) {
                return '<div class="btn edit_btn" data-mahh="' + data + '"><i class="fa-solid fa-pen" style="font-size: 20px"></i></div>';
            }
        },
        {
            "data": "maHH",
            "render": function (data, type, row) {
                return '<div class="btn delete_btn" data-mahh="' + data + '"><i class="fa-solid fa-trash" style="font-size: 20px"></i></div>';
            }
        }
    ]
});

$(document).on('click', '.edit_btn', function () {
    let maHH = $(this).data('mahh'); 
    console.log("Edit button clicked for maHH:", maHH);
    
});

$(document).on('click', '.delete_btn', function () {
    let maHH = $(this).data('mahh');
    const url = `http://localhost:5159/api/HangHoa/delete-hanghoa/${maHH}`;
    let row = $(this).closest('tr');
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (result) {
            console.log(`HangHoa with ID ${maHH} has been deleted.`);
            table.row(row).remove().draw(false); 




        },
        error: function (xhr, status, error) {
            console.error(`Failed to delete HangHoa with ID ${maHH}. Error: ${error}`);
        }
    });
});
