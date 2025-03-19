const ctx = document.getElementById('myChart').getContext('2d');
let myChart; // Biến để lưu đối tượng Chart

$(document).ready(function () {
    $('#btn').click(e => {
        const x = $('#x').val();
        const y = $('#y').val();
        const z = $('#z').val();

        console.log(x, y + " " + z);
        $.ajax({
            url: `http://localhost:5159/api/Revenue/revenues?x=${x}&y=${y}&z=${z}`,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                const thangArray = data.map(item => item.thang);
                const totalArray = data.map(item => item.total);
                console.log('Mảng thang:', thangArray);
                console.log('Mảng total:', totalArray);

                // Xóa biểu đồ cũ nếu có
                if (myChart) {
                    myChart.destroy();
                }

                // Tạo biểu đồ mới
                myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: thangArray,
                        datasets: [{
                            label: '# of Votes',
                            data: totalArray,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error('Error:', error); // Xử lý lỗi nếu có
            }
        });
    });
});

const selectElement = document.getElementById('x');

for (let i = 1; i <= 12; i++) {
    const option = document.createElement('option');
    option.value = i;
    option.textContent = i;
    selectElement.appendChild(option);
}

const selectElementY = document.getElementById('y');

for (let i = 1; i <= 12; i++) {
    const option = document.createElement('option');
    option.value = i;
    option.textContent = i;
    selectElementY.appendChild(option);
}
ss