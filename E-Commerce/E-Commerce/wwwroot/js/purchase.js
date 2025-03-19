
// Tìm phần tử nút dựa trên class hoặc các thuộc tính khác
let button = document.querySelector('.btn.dropdown-toggle');

// Lấy giá trị email từ nội dung của nút
//let email = button.textContent.trim();
let email = "bergs@abc.com";
console.log(email); // Output: 123@123

let table1 = new DataTable('#myTable1', {
    "ajax": {
        "url": `http://localhost:5159/api/Cart/all-cart?email=${encodeURIComponent(email)}`,
        "type": "GET",
        "dataType": "json",
        "dataSrc": ""
    },
    "columns": [
        { "data": "ngayDat" },
        { "data": null, "visible": false },
        { "data": "tenHH" },
        { "data": null, "visible": false },
        { "data": "donGia" },
        { "data": "soLuong" }
    ]
});