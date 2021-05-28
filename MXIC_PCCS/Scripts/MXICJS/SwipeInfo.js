// var dataList = [
//     { 'name': '正常', 'value': '正常' },
//     { 'name': '異常', 'value': '異常' },
//     { 'name': '加班', 'value': '加班' },
//     { 'name': '遲到', 'value': '遲到' },
//     { 'name': '早退', 'value': '早退' },
//     { 'name': '代早', 'value': '代早' },
//     { 'name': '代晚', 'value': '代晚' },

// ]

$(document).ready(function () {
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: "/Webpage/SwipeInfoSelect",
        data:{TableName:tablename},
    
        success: function (data) {
            dataList = [];
            dataList = JSON.parse(data);
            console.log(dataList);
           
          
        }
    })

    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: "/Webpage/SwipeDoubleCheckTypeSelect",

        success: function (data) {
            CheckTypedata = [];
            CheckTypedata = JSON.parse(data);
            console.log(CheckTypedata);


        }
    })

})
