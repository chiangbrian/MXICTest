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
           
          
        }})

})