window.onload = function () {

    let lb = []
    let dp = []
    if ($('#lables').val().length > 0) {
        lb=$.parseJSON($('#lables').val());;
        dp=$.parseJSON($('#datapoints').val());
    }

    if (dp.length > 0) {
        $('#chartContainer').show();
        const data = {
            labels: lb,
            datasets: [{
                label: 'Speed Of Load Pages, ms',
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                data: dp
            }]
        };
        const config = {
            type: 'line',
            data: data,
            options: {}
        };
    
        var myChart = new Chart(
            document.getElementById('chartContainer'),
            config
        );
    }
    else {
        $('#chartContainer').hide();
    }
}