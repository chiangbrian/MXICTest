$(document).ready(function () {
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: "/Webpage/VendorManagementSelect",
    
        success: function (data) {
            dataList = [];
            dataList = JSON.parse(data);
            console.log(dataList);
           
          
        }})

})


$(document).ready(function () {
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: "/VendorManagement/Shifts",
    
        success: function (data) {
            dataList2 = [];
            dataList2 = JSON.parse(data);
            console.log(dataList2);
           
          
        }})

})
