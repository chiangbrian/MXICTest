var mydata = [];
seachInputValue = [];
DelUserListID="";
//動態生成input 資料形式範例  Remarks為描述欄位  Generate為是否啟用動態生成 有一才生成

function GridData() {
    DepNo=seachInputValue[0];
    DepName=seachInputValue[1];
    UserID=seachInputValue[3];
    UserName=seachInputValue[2];
$.ajax({
    async: false,
    cache: false,
    type: "post",
    datatype: "json",
    data:{DepNo:DepNo,DepName:DepName,UserID:UserID,UserName:UserName},
    url: "/UserManagement/UserList",
    error: function () {
        alert('表單查詢失敗，目前為範例資料')
    },
    success: function (result) {
       
        mydata = [];
        mydata =JSON.parse(result);

        $("#grid").jqGrid('setGridParam', { data: mydata });
        // refresh the grid
        $("#grid").trigger('reloadGrid');
     
     
    }
})}


$(document).ready(function () {
    $('.title').html(title);
    //動態生成INPUT
    $('.inputBox').html('');
    for (i = 0; i < GenerateResult.length; i++) {
        $('.inputBox').append('<input type="text" name="seachTextInput" placeholder="' + GenerateResult[i] + '" required="required" />')
    }

    //動態生成Pop INPUT
    $('.insertPopUpContant').html('');
    for (i = 0; i < PopGenerateResult.length; i++) {
        $('.insertPopUpContant').append('<input type="text" name="insertTextInput" placeholder="' + PopGenerateResult[i] + '" required="required" />')
    }
    GridData();
    console.log(mydata);
    //step.1 根據陣列物件數量，自動配置欄寬
    var jsonItemList = mydata.map(item => Object.keys(item));
    var dataColume = []
    dataColume.push(jsonItemList[0])
    var ColumeWid = dataColume[0].length;


    //step.2 根據陣列物件數量，自動配置欄寬
    var gridWid = $('.tableContant').width();
    //step.3 傳遞參數至表格 
    var gridColume = gridWid / ColumeWid;

    //根據螢幕高度縮放表格列數
    var winHei = document.body.clientHeight;
    var pageRow = []
    if (winHei > 900) {
        pageRow = 15;
    } else {
        pageRow = 10;
    }
    setTimeout(function () {
        //Loading
        $('.loading').addClass('hidden');
        $('.tableContant').removeClass('hidden').addClass('show')

        //表格
        $("#grid").jqGrid({
            datatype: "local",
            //data: mydata,
            colNames: ["部門代號", "部門名稱", "人員編號", "人員姓名", "Admin","Edit","Delete"],
            colModel: [
                { name: "DepNo", index: "DepNo", width: gridColume, align: "center" },
                { name: "DepName", index: "DepName", width: gridColume, align: "center" },    
                { name: "UserID", index: "UserID", width: gridColume, align: "center" },
                { name: "UserName", index: "UserName", width: gridColume, align: "center" },
                { name: "Admin", index: "Admin", width: gridColume*0.6, align: "center" , formatter: CheckBox},
                { name: "UserListID", index: "Edit", width: gridColume*0.6, align: "center", formatter: EditBtn },
                { name: "UserListID", index: "Delete", width: gridColume*0.6, align: "center", formatter: DeleteBtn }
            ],
            height: "100%",
            guiStyle: "bootstrap4",
            iconSet: "fontAwesome",
            pageSize: "10",
            idPrefix: "gb1_",
            rownumbers: true,
            sortname: "invdate",
            sortorder: "desc",
            pager: true,
            rowNum: pageRow,
        }

        );
        GridData();
    }, 2000);
    //navbar 導覽列自動生成排列
    // navbarItem(navData);


    //跑馬燈生成
    marquee(marqueeContant);

});
//表格返回自訂物件
function EditBtn(cellvalue, options, rowObject) {
    return ' <a href="#" id='+cellvalue+' class="seachBTN btn-1" style="width:50px" onclick="EditUser()">修改</a>';
}
function DeleteBtn(cellvalue, options, rowObject) {
    return ' <a href="#"  id='+cellvalue+' class="seachBTN btn-1" onclick="delect(this)">刪除</a>';
}

function CheckBox(cellvalue, options, rowObject) {
    var reg = RegExp("false");
    //判斷ChildGrid是否開啟
    if (reg.exec(cellvalue)) {
        return '<input type="checkbox" onclick="return false">';
    }
    else
    {
        return '<input type="checkbox" checked onclick="return false" >';
    }
}







//開啟"刪除"彈窗
function delect(e) {
    $('.delectBox').fadeIn(700);
    $('.cover').removeClass('blur-out').addClass('blur-in')
    DelUserListID=e.id;
}


//刪除資料按鈕 請自行定義傳遞陣列方式
function delectCheck() {
    $('.delectBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    alert(DelUserListID);
    //下方定義傳遞方式

    
    $.ajax({
        url: "/UserManagement/DeleteUser",
        type: "post",
        dataType: "text",
        async: false,
        data: {UserListID:DelUserListID},
        success: function (result) {
            alert(result);
            $("#grid").jqGrid('clearGridData');//清空表格
            GridData()
              
            }
            
        })
   
}

//取得所有"文字"輸入框參數
function seachBtn() {
    seachInputValue = []
    inputLength = document.querySelectorAll('input[name="seachTextInput"]').length;
    for (i = 0; i < inputLength; i++) {
        key = document.querySelectorAll('input[name="seachTextInput"]')[i].placeholder;
        value = document.querySelectorAll('input[name="seachTextInput"]')[i].value;
        obj = value 
        seachInputValue.push(obj);
    }
    console.log(seachInputValue)
    $("#grid").jqGrid('clearGridData');//清空表格
    GridData()
}

function cancel() {
    $('.popUp').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    //清空POP視窗 輸入框數值
    $('.popUpContant > input').val('');
}
//確認表單按鈕 請自行定義傳遞陣列方式
function check() {
    $('.insertBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    //取得所有新增"文字"輸入框參數
    insertInputValue = [];
    inputLength = document.querySelectorAll('input[name="insertTextInput"]').length;
    for (i = 0; i < inputLength; i++) {
        key = document.querySelectorAll('input[name="insertTextInput"]')[i].placeholder;
        value = document.querySelectorAll('input[name="insertTextInput"]')[i].value;
        obj =  value 
        insertInputValue.push(obj);
    }
    console.log(insertInputValue)
    alert(insertInputValue[0]);
    //下方定義傳遞方式
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "text",
        data:{DepNo: insertInputValue[0],DepName:insertInputValue[1],UserID:insertInputValue[3],UserName:insertInputValue[2],Admin:insertInputValue[4],PassWord:insertInputValue[5]},
        url: "/UserManagement/AddUser",
        error: function () {
            alert('表單查詢失敗，目前為範例資料')
        },
        success: function (result) {
           
            alert(result)
            $("#grid").jqGrid('clearGridData');//清空表格
            GridData()
         
        }
    })

    //清空POP視窗 輸入框數值
    $('.popUpContant > input').val('');
}