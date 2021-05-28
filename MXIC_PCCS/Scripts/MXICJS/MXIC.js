//頁面變動依據資料庫生成 無須更改前端架構 樣式如需修改僅需修改MXIC.css檔
//該頁面表格需使用之api請先定義

var dataList = [];
var ajaxUrl = [];
var seachobj = '';
generateUrl = '/Webpage/PageGenerate';
var tablename = ""
var multiselect;
//var navUrl = '/Webpage/Nav';
var navUrl = '.../api/api-1';
var title = document.title;
if (title == 'MXIC') {

} else if (title == '人員管理') {
    //搜尋url這個要先寫
    ajaxUrl = "/UserManagement/UserList"
    //編輯資料取的url
    editDetailUrl = "/UserManagement/EditUserDetail",
    //編輯Url
    editUrl = "/UserManagement/EditUser"
    //刪除url
    deleteurl = "/UserManagement/DeleteUser"
    //新增url
    inserturl = "/UserManagement/AddUser"
    //table名稱
    tablename = "MXIC_UserManagement"
    //是否出現刪除checkbox
    multiselect = true;
} else if (title == '部門管理') {
    //搜尋url這個要先寫
    ajaxUrl = '/DepartmentManagement/SearchDepToVen'
    //刪除url
    deleteurl = "/DepartmentManagement/DeleteDepToVen"
    //table名稱
    tablename = "MXIC_DepartmentManagement"
    //是否出現刪除checkbox
    multiselect = true;
} else if (title == '廠商管理') {
    //搜尋url這個要先寫
    ajaxUrl = "/VendorManagement/VendorList"
    //編輯資料取的url
    editDetailUrl = "/VendorManagement/EditVendorDetail",
    //編輯Url
    editUrl = "/VendorManagement/EditVendor"
    //刪除url
    deleteurl = "/VendorManagement/DeleteVendor"
    //新增url
    inserturl = "/VendorManagement/AddVendor"
    //table名稱
    tablename = "MXIC_VendorManagement"
    //是否出現刪除checkbox
    multiselect = true;
} else if (title == '報價單') {
    //搜尋url這個要先寫
    ajaxUrl = "/Quotation/SearchQuotation"
    //刪除url
    deleteurl = "/Quotation/DelQuotation"
    //table名稱
    tablename = "MXIC_Quotation"
    //是否出現刪除checkbox
    multiselect = false;
} else if (title == '班表設定') {
    //搜尋url這個要先寫
    ajaxUrl = "/ScheduleSetting/ScheduleList"
    //刪除url
    deleteurl = "/ScheduleSetting/DelSchedule"
    //table名稱
    tablename = "MXIC_ScheduleSetting"
    //是否出現刪除checkbox
    multiselect = false;
} else if (title == '證照管理') {
    //搜尋url這個要先寫
    ajaxUrl = "/LisenceManagement/SearchLisence"
    //編輯資料取的url
    editDetailUrl = "/LisenceManagement/EditLisenceDetail",
    //編輯Url
    editUrl = "/LisenceManagement/EditLisence"
    //刪除url
    deleteurl = "/LisenceManagement/DeleteLisence"
    //新增url
    inserturl = "/LisenceManagement/AddLisence"
    //table名稱
    tablename = "MXIC_LisenceManagement"
    //是否出現刪除checkbox
    multiselect = true;
} else if (title == '刷卡紀錄') {
    tablename = "MXIC_View_Swipe"
    ajaxUrl = "/SwipeInfo/CheckinList"
    inserturl = "/SwipeInfo/InsertSwipe"
    editDetailUrl = "/SwipeInfo/SwipeInfoDetail"
    editUrl = "/SwipeInfo/EditSwipe"
    //是否出現刪除checkbox
    multiselect = true;
} else if (title == '刷卡警報') {
    tablename = "MXIC_View_Swipe_Double"
    ajaxUrl = "/SwipeInfo/AlarmList"
    //是否出現刪除checkbox
    multiselect = false;
} else if (title == '匯出計價單') {
    tablename = "MXIC_ExportPO"
} else if (title == '班別時間設定') {
    tablename = "MXIC_SwipeSet"
    ajaxUrl = "/SwipeSet/UserList"
    editDetailUrl = "/SwipeSet/EditTimeDetail",
    editUrl = "/SwipeSet/EditTime"
    multiselect = false;
}
//dataList
// var dataList = [
//     { 'name': '正常', 'value': '正常' },
//     { 'name': '異常', 'value': '異常' },
//     { 'name': '加班', 'value': '加班' },
//     { 'name': '遲到', 'value': '遲到' },
//     { 'name': '早退', 'value': '早退' },
//     { 'name': '代早', 'value': '代早' },
//     { 'name': '代晚', 'value': '代晚' },

// ]
//navBar & indexBTN 資料格式範例 
var navData = [
    { 'name': '人員管理', 'url': '../UserManagement/', 'commonly_used': '1' },
    { 'name': '部門管理', 'url': '../DepartmentManagement/', 'commonly_used': '1' },
    { 'name': '廠商管理', 'url': '../VendorManagement/', 'commonly_used': '1' },
    { 'name': '報價單', 'url': '../Quotation/', 'commonly_used': '1' },
    { 'name': '班表設定', 'url': '../ScheduleSetting/', 'commonly_used': '1' },
    { 'name': '證照管理', 'url': '../LisenceManagement/', 'commonly_used': '1' },
    { 'name': '刷卡紀錄', 'url': '../SwipeInfo/', 'commonly_used': '1' },
    { 'name': '刷卡警報', 'url': '../SwipeInfo/Alarm', 'commonly_used': '1' },
    { 'name': '班別時間設定', 'url': '../SwipeSet/', 'commonly_used': '1' },
    { 'name': '匯出報表', 'url': '../ExportPO/', 'commonly_used': '1' }
]

//跑馬燈資料格式範例
var data = [
    { 'type': 'test', 'contant': '這是一個測試' },
    { 'type': 'alert', 'contant': '可能會爆炸' },
    { 'type': 'test', 'contant': '不要再點下去了' },
    { 'type': 'test', 'contant': '我覺得已經很完美' },
    { 'type': 'test', 'contant': '只能給你放五個公告' }
]

//撈取描述 SQL語法
// SELECT a.Table_schema +'.'+a.Table_name       as 表格名稱   
//        ,b.COLUMN_NAME                          as 欄位名稱   
//        ,b.DATA_TYPE                            as 資料型別   
//        ,isnull(b.CHARACTER_MAXIMUM_LENGTH,'')  as 長度   
//        ,isnull(b.COLUMN_DEFAULT,'')            as 預設值   
//        ,b.IS_NULLABLE                          as 允許空值   
//        ,( SELECT value   
//                 FROM fn_listextendedproperty (NULL, 'schema', a.Table_schema, 'table', a.TABLE_NAME, 'column', default)   
//                WHERE name='MS_Description'    
//                  and objtype='COLUMN'    
//                  and objname Collate Chinese_Taiwan_Stroke_CI_AS = b.COLUMN_NAME   
//          ) as 欄位備註   
//  FROM    
//      INFORMATION_SCHEMA.TABLES  a
//      LEFT JOIN INFORMATION_SCHEMA.COLUMNS b
//                ON a.TABLE_NAME = b.TABLE_NAME
// WHERE TABLE_TYPE='BASE TABLE'  and a.Table_name = '填入表名'
// ORDER BY a.TABLE_NAME

//navBar內容 更改ajax url屬性與篩選條件
$.ajax({
    async: false,
    cache: false,
    type: "post",
    datatype: "json",
    url: navUrl,
    // error: function () {
    //     alert('Navber異常，目前為範例資料')
    // },
    success: function (data) {
        navData = [];
        navData = JSON.parse(data);
    }
})
console.log(navData)

//搜尋欄位生成 更改ajax url屬性與篩選條件
$.ajax({
    async: false,
    cache: false,
    type: "post",
    datatype: "json",
    url: generateUrl,
    data: { tablename: tablename },
    // error: function () {
    //     alert('欄位生成異常，目前為範例資料')
    // },
    success: function (data) {
        inputGenerate = [];
        inputGenerate = JSON.parse(data);
        console.log(inputGenerate)
        //inputGenerate = data;
    }
})

//表格內容 更改ajax url屬性與篩選條件
$.ajax({
    async: false,
    cache: false,
    type: "post",
    datatype: "json",
    url: ajaxUrl,
    data: { AjaxSelect },
    // error: function () {
    //     alert('表單查詢失敗，目前為範例資料')
    // },
            success: function (data) {
        mydata =[];
        mydata = JSON.parse(data);

    }
    })

// 取得查詢AJAX查詢條件
var AjaxSelect = $.map(inputGenerate, function (item, index) {
    if (item.Generate == '1') {
        return item.COLUMN_NAME
    }
    })


var AjaxInsert = $.map(inputGenerate, function (item, index) {
    if (item.PopGenerate == '1') {
        return item.COLUMN_NAME
    }
    })

var AjaxEdit = $.map(inputGenerate, function (item, index) {
    if (item.PopGenerate == '1' && item.EditPopGenerate !== '0') {
        return item.COLUMN_NAME
    }
    })


//------------------------動態生成---------------------


//取得頁面應動態生成INPUT資料
var GenerateResult = $.map(inputGenerate, function (item, index) {
    if (item.Generate == '1') {
        return item.Remarks
    }
})
console.log(GenerateResult)
//取得頁面應動態生成INPUT格式
var GenerateTypeResult = $.map(inputGenerate, function (item, index) {
    if (item.GenerateType !== '0' && item.Generate == '1') {
        return item.GenerateType
    }
})
console.log(GenerateTypeResult)
//取得Pop應動態生成INPUT資料
var PopGenerateResult = $.map(inputGenerate, function (item, index) {
    if (item.PopGenerate == '1') {
        return item.Remarks
    }
})
//取得Pop應動態生成INPUT 格式
var PopGenerateTypeResult = $.map(inputGenerate, function (item, index) {
    if (item.AddPopGenerate !== '0' && item.PopGenerate == '1') {
        return item.AddPopGenerate
    }
})

//取得描述應動態生成表頭資料
var RemarksResult = $.map(inputGenerate, function (item, index) {
    if (item.Remarks !== '' && item.GridTitleGenerate == 1) {
        return item.Remarks
    }
})

//取得修改Pop應動態生成INPUT資料
var editPopGenerateResult = $.map(inputGenerate, function (item, index) {
    if (item.PopGenerate == '1' && item.EditPopGenerate !== '0') {
        return item.Remarks
    }
})

//取得Pop應動態生成INPUT 格式
var editPopGenerateTypeResult = $.map(inputGenerate, function (item, index) {
    if (item.PopGenerate == '1' && item.EditPopGenerate !== '0') {
        return item.EditPopGenerate
    }
})




console.log(RemarksResult)
//------------------------動態生成---------------------------------
//取得資料KEY colModal自動生成需使用
var testA = Object.keys(mydata[0]);
console.log(testA)

//step.1 根據陣列物件數量，自動配置欄寬
var jsonItemList = mydata.map(item => Object.keys(item));
var dataColume = []
dataColume.push(jsonItemList[0])
var ColumeWid = dataColume[0].length;
//---------------------------------------------------------------------------------------------------------------------
$(document).ready(function () {

    $('.DelBtn').addClass("disabled");
    $('.DelBtn').attr("disabled", true);

    UserID = $('#UserID').val();
    Admin = "";

    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: '/Webpage/Admin',
        data: { UserID: UserID },
        success: function (data) {
            Admin = "";
            Admin = data;
            if (Admin == "false") {
                multiselect = false;
                $('.Manager').attr("style", "display:none;");
            } else if (Admin == "true") {
                $('.SuperAdmin').attr("style", "display:none;");

            }
        }
    })




    $('.title').html(title);


    //-----------------------------------------------------以下新版動態生成
    for (i = 0; i < GenerateResult.length; i++) {
        if (GenerateTypeResult[i] == "date") {
            $('.inputBox').append(GenerateResult[i] + '<input type="' + GenerateTypeResult[i] + '" autocomplete="off" name="seachTextInput" placeholder="' + GenerateResult[i] + '" required="required" />')
        } else {
            $('.inputBox').append('<input type="' + GenerateTypeResult[i] + '" autocomplete="off" name="seachTextInput" placeholder="' + GenerateResult[i] + '" required="required" />')
            $('input[type=select]').replaceWith(GenerateResult[i] + '<input  onfocus=this.value="" type="text" autocomplete="off" name="seachTextInput" id="" class="editInputOption" list="seachInputOption' + [i] + '"><datalist id="seachInputOption' + [i] + '"></datalist>')

        }
    };

    //畫面select選單資料
    if (title == '廠商管理') {
        inputOption('seachInputOption1', dataList, '', '');
        inputOption('seachInputOption4', dataList2, '', '');
    }
    else {
        for (i = 0; i < GenerateResult.length; i++) {
            if (GenerateTypeResult[i] == "select") {
                inputOption('seachInputOption' + [i], dataList, '', '');
            }
        }
    }
    //動態生成新增Pop INPUT
    inputGenerate('insertPopUpContant', PopGenerateResult, PopGenerateTypeResult, 'insertTextInput');

    //動態生成修改Pop INPUT
    inputGenerate('editPopUpContant', editPopGenerateResult, editPopGenerateTypeResult, 'editTextInput');

    //彈跳視窗 input生成
    function inputGenerate(location, result, resultType, inputname) {
        if (location == 'insertPopUpContant') {
            var ClassName = 'insertInputOption';
        } else { var ClassName = 'editInputOption'; }
        $('.' + location).html('');
        for (i = 0; i < result.length; i++) {

            $('.' + location).append('<div class="PopSeachBox" stlye="display:flex;"><label>' + result[i] + '</label><input type="' + resultType[i] + '" autocomplete="off" name="' + inputname + '" placeholder="' + result[i] + '" required="required" /></div>')
            $('input[type=select]').replaceWith('</label><input  type="text" autocomplete="off" onfocus=this.value=""  name="' + inputname + '" id="editPopInput' + i + '" class="' + ClassName + '" list="' + ClassName + [i] + '"><datalist id="' + ClassName + [i] + '"></datalist>')

        };
    }




    //指定 下拉選單選項
    // function inputOption(inputID, DT, PrefixValue, PrefixText) {
    //     for (i = 0; i < DT.length; i++) {
    //         $('#' + inputID).append('<option value="' + PrefixValue + '' + DT[i].name + '" />' + PrefixText + '' + DT[i].value + '</option>')
    //     };
    // };

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
    testArea = [];
    //JQ-Grid的ColModel自動生成
    for (i = 0; i < testA.length; i++) {
        inputGenerate2 = [];

        testb = []

        $.ajax({
            async: false,
            cache: false,
            type: "post",
            datatype: "json",
            url: generateUrl,
            data: { tablename: tablename, COLUMN_NAME: testA[i] },
            // error: function () {
            //     alert('欄位生成異常，目前為範例資料')
            // },
            success: function (data) {
                inputGenerate2 = [];
                inputGenerate2 = JSON.parse(data);
            }
        })
        testb = Object.values(inputGenerate2).map(item => item.GridFormatter);
        console.log(inputGenerate2);
        if (testb == 'CheckBox') {

            testdata = { name: testA[i], index: testA[i], width: gridColume, align: "center", formatter: CheckBox }

        } else if (testb == 'DeleteBtn' && (Admin == "true" || Admin == "SuperAdmin")) {
            //hidden: true 記得加入
            testdata = { name: testA[i], index: testA[i], width: gridColume * 0.5, align: "center", key: true, formatter: DelCheckBox, hidden: true }

        } else if (testb == 'EditBtn' && (Admin == "true" || Admin == "SuperAdmin")) {

            testdata = { name: testA[i], index: testA[i], width: gridColume * 0.7, align: "center", formatter: EditBtn }

        } else if (testb == 'Hidden' || (testb == 'EditBtn' && Admin == "false") || (testb == 'DeleteBtn' && Admin == "false")) {

            testdata = { name: testA[i], index: testA[i], width: gridColume * 0.5, align: "center", hidden: true }

        } else if (testb == 'SwipeEditBtn') {

            testdata = { name: testA[i], index: testA[i], width: gridColume * 0.7, align: "center", formatter: EditBtn }

        } else if (testb == 'jqWidthQPCN') {

            testdata = { name: testA[i], index: testA[i], width: gridColume * 2.2, align: "center" }
        } else {

            testdata = { name: testA[i], index: testA[i], width: gridColume, align: "center" }
        }
        // 追加する新しいレコードを作成
        testArea.push(testdata)
    }


    //Loading
    $('.loading').addClass('hidden');
    $('.tableContant').removeClass('hidden').addClass('show')

    //表格
    $("#grid").jqGrid({
        datatype: "local",
        data: mydata,
        colNames: RemarksResult,
        colModel: testArea,
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
        multiselect: multiselect,
        //     onSelectRow: function (rowId, status, e) {

        //     alert("選一個")
        //     },
        // //点击题头的checkbox按钮，一键多选
        //     onSelectAll:function(rowids,statue){
        //         alert("選全部")
        //     },
        //     //multiboxonly=true,
        loadComplete: function () {
            //获取列表数据
            var ids = $("#grid").jqGrid("getDataIDs");
            var rowDatas = $("#grid").jqGrid("getRowData");
            for (var i = 0; i < rowDatas.length; i++) {
                var rowData = rowDatas[i];
                var electricityCondition = rowData.AttendType;
                if (electricityCondition == '異常' || electricityCondition == '曠職')
                    $("#" + ids[i]).find("td").css({ "background-color": "#ff4040", "color": "white" });

                var electricityCondition_LisenceDiff = new Date(rowData.EndDate);
                var Today = new Date();
                var DateDiff = parseInt(Math.abs(Today - electricityCondition_LisenceDiff)) / 86400000;

                if (DateDiff < 90) {
                    $("#" + ids[i]).find("td").css({ "background-color": "#FF0000", "font-weight": "bold" });
                } else if (DateDiff < 180) {
                    $("#" + ids[i]).find("td").css({ "background-color": "#FFFF37", "font-weight": "bold" });
                }

            }
        }
    });

    navbarItem(navData);
    iconMenu(navData);
    marquee(marqueeContant);
});

////指定 下拉選單選項
function inputOption(inputID, DT, PrefixValue, PrefixText) {


    if (DT.length == 1) {
        $('#' + inputID).append('<option value="' + PrefixValue + '' + DT[0].name + '" />' + PrefixText + '' + DT[0].value + '</option>')

    }
    else {
        for (i = 0; i < DT.length; i++) {
            $('#' + inputID).append('<option value="' + PrefixValue + '' + DT[i].name + '" />' + PrefixText + '' + DT[i].value + '</option>')
        };
    }

};



//navbar 導覽列自動生成排列
function navbarItem(e) {
    var arrLength = e.length;
    var menuWidth = $('.navber').width();
    var menuBTN = (100 / arrLength)
    for (i = 0; i < arrLength; i++) {
        $('.navber > ul').append('<li><a href="./' + e[i].url + '">' + e[i].name + '</a></li>')
    }
    if (arrLength <= 10) {
        $('ul > li').css('width', '' + menuBTN + '%');
    } else {
        $('.navber > ul').css('flex-wrap', 'wrap')
    }
}

//index導覽按鈕生成
function iconMenu(e) {
    var arrLength = e.length;
    var menuWidth = $('.indexBTN').width();
    for (i = 0; i < arrLength; i++) {
        if (i == 3) {
            $('.indexBTN').append('<div class="btn"><div class="btnBackground link-container"><a class="link" href="./' + e[i].url + '">' + e[i].name + '</a></div></div><br>')
        } else {
            $('.indexBTN').append('<div class="btn"><div class="btnBackground link-container"><a class="link" href="./' + e[i].url + '">' + e[i].name + '</a></div></div>')
        }
    }
}


//跑馬燈 內容
function marquee(e) {
    i = 0;
    marqueeMassege(e, 0)
    setInterval(function () {
        i += 1;
        if (i > e.length - 1) {
            i = 0;
        }
        marqueeMassege(e, i);
    }, 20000);
    $('.fa-chevron-left').click(function () {
        i--;
        if (i < 0) {
            i = 0;
        }
        marqueeMassege(e, i);
    });
    $('.fa-chevron-right').click(function () {
        i++;
        if (i > e.length - 1) {
            i = e.length - 1;
        }
        marqueeMassege(e, i);
    });
}

//跑馬燈內容 調用顯示內容 傳入資料形式與所在位置
function marqueeMassege(data, num) {
    $('.marqueeContant').empty();
    $('.marqueeContant').append('<div style="display:flex; height:18px; margin:0 20px 0 0"><p style="color:red; margin-right:10px">' + data[num].type + '</p><p style="line-height: 18px;">' + data[num].contant + '</p></div>')
}
//取得所有"文字"輸入框參數
function seachBtn() {

    seachInputValue = []
    inputLength = document.querySelectorAll('input[name="seachTextInput"]').length;
    for (i = 0; i < inputLength; i++) {
        //key = document.querySelectorAll('input[name="seachTextInput"]')[i].placeholder;
        value = document.querySelectorAll('input[name="seachTextInput"]')[i].value;
        obj = value
        seachInputValue.push(obj);
    }
    console.log(seachInputValue)
    seachobj = '';
    for (x = 0; x < AjaxSelect.length; x++) {

        seachobj += AjaxSelect[x] + '=' + seachInputValue[x] + '&'

    }
    console.log(seachobj)
    GridData()

}

function GridData() {
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: ajaxUrl,
        data: seachobj,
        traditional: true,
        success: function (data) {
            $("#grid").jqGrid('clearGridData');
            mydata = [];
            mydata = JSON.parse(data);
            $("#grid").jqGrid('setGridParam', { data: mydata });
            // refresh the grid
            $("#grid").trigger('reloadGrid');
        }
    })
}
//開啟"新增"彈窗
function insert() {
    if (title == '廠商管理') {
        inputOption('insertInputOption4', dataList2, '', '');
    } 
    else if (title == '刷卡紀錄') {
        inputOption('insertInputOption0', dataList, '', '');
        inputOption('insertInputOption1', CheckTypedata, '', '');
    }
    else {
        inputOption('insertInputOption2', dataList, '', '');
        inputOption('editInputOption0', dataList, '', '');
        inputOption('editInputOption2', dataList, '', '');
    }

    $("#insert input").val("");
    $('.insertBox').fadeIn(700);
    $('.cover').removeClass('blur-out').addClass('blur-in')


}
//開啟"刪除"彈窗
function delect(e) {
    $('.delectBox').fadeIn(700);
    $('.cover').removeClass('blur-out').addClass('blur-in')

    DeleteID = e.id;
}

//開啟"修改"彈窗
function edit(e) {
    if (title == '廠商管理') {
        inputOption('editInputOption4', dataList2, '', '');
    }
    else {
        inputOption('editInputOption0', dataList, '', '');
        inputOption('editInputOption2', dataList, '', '');
    }
    $('.popEditBox').fadeIn(700);
    $('.cover').removeClass('blur-out').addClass('blur-in')

    EditID = e.id;

    EditData(EditID)


}

//生成EDIT中的資料
function EditData(EditID) {
    editinput = []
    EditIinfo = []

    EditColumnName = []

    inputLength = document.querySelectorAll('input[name="editTextInput"]').length;
    for (i = 0; i < inputLength; i++) {
        key = document.querySelectorAll('input[name="editTextInput"]')[i].placeholder;

        obj = key
        EditColumnName.push(obj);
    }
    //console.log(EditColumnName)
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: editDetailUrl,
        data: { EditID: EditID },
        traditional: true,
        success: function (data) {

            EditIinfodata = JSON.parse(data);

            console.log(EditIinfodata)

        }
    })


    var EditdataColumnName = Object.keys(EditIinfodata[0]);

    for (x = 0; x < EditdataColumnName.length; x++) {

        for (var idx_Key in EditIinfodata[x]) {
            editobj = EditIinfodata[x][idx_Key]
            EditIinfo.push(editobj)
        }
    }

    for (i = 0; i <= EditIinfo.length; i++) {
        editinput = []
        if (EditIinfo[i] == "true") {

            editinputobj = $('.editPopUpContant input[placeholder=' + EditColumnName[i] + ']').attr('checked', true)

        } else if (EditIinfo[i] == "false") {

            editinputobj = $('.editPopUpContant input[placeholder=' + EditColumnName[i] + ']').attr('checked', false)
        }
        else if (editPopGenerateTypeResult[i] == "Select") {

            editinputobj = $('.editPopUpContant input[id=editPopInput' + i + ']').attr('value', EditIinfo[i])
        }
        else {

            editinputobj = $('.editPopUpContant input[placeholder=' + EditColumnName[i] + ']').attr('value', EditIinfo[i])
        }

        editinput.push(editinputobj)
    }

}


//關閉彈窗
function cancel() {
    $('.popUp').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    ResetInput();

}
//確認表單按鈕 請自行定義傳遞陣列方式
function check() {


    $('.insertBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    //取得所有新增"文字"輸入框參數
    insertInputValue = [];

    checkvalue = '';
    inputLength = document.querySelectorAll('.insertPopUpContant input[name="insertTextInput"]').length;

    console.log(inputLength)
    if ($(".insertPopUpContant input[name=insertTextInput]").is(':checked')) {
        checkvalue = "true"
    } else {
        checkvalue = "false"
    }
    for (i = 0; i < inputLength; i++) {
        key = document.querySelectorAll('.insertPopUpContant input[name="insertTextInput"]')[i].type;
        if (key == "checkbox") {

            value = checkvalue;
        } else {
            value = document.querySelectorAll('.insertPopUpContant input[name="insertTextInput"]')[i].value;
        }
        if (document.querySelectorAll('.insertPopUpContant input[name="insertTextInput"]')[i].type == 'date') {
            if (document.querySelectorAll('.insertPopUpContant input[name="insertTextInput"]')[i].value == "") {
                alert('新增失敗，請輸入所有資料!');
                return;
            }
        }

        obj = value
        insertInputValue.push(obj);
    }
    console.log(insertInputValue)

    console.log(AjaxInsert)


    //下方定義傳遞方式
    Insertobj = '';
    for (x = 0; x < AjaxInsert.length; x++) {

        Insertobj += AjaxInsert[x] + '=' + insertInputValue[x] + '&'

    }
    console.log(Insertobj)
    InsertDB()

    GridData()
    //清空POP視窗 輸入框數值
    $('.popUpContant > input').val('');
}


function InsertDB() {

    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "text",
        url: inserturl,
        data: Insertobj,
        traditional: true,
        success: function (data) {
            console.log(data)
            alert(data);

        }
    })
}
function Editcheck() {
    $('.popEditBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    //取得所有新增"文字"輸入框參數
    editInputValue = [];
    checkvalue = '';
    inputLength = document.querySelectorAll('.editPopUpContant input[name="editTextInput"]').length;

    console.log(inputLength)
    if ($(".editPopUpContant input[name=editTextInput]").is(':checked')) {
        checkvalue = "true"
    } else {
        checkvalue = "false"
    }
    for (i = 0; i < inputLength; i++) {
        key = document.querySelectorAll('.editPopUpContant input[name="editTextInput"]')[i].type;
        if (key == "checkbox") {

            value = checkvalue;
        } else {
            value = document.querySelectorAll('.editPopUpContant input[name="editTextInput"]')[i].value;
        }


        obj = value;
        editInputValue.push(obj);
    }
    console.log(editInputValue)

    console.log(AjaxEdit)


    //下方定義傳遞方式
    Editobj = 'EditID=' + EditID + '&';
    for (x = 0; x < AjaxEdit.length; x++) {

        Editobj += AjaxEdit[x] + '=' + editInputValue[x] + '&'

    }
    console.log(Editobj)
    ResetInput();

    EditDB();
    //清空POP視窗 輸入框數值
    GridData()


}

function EditDB() {
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "text",
        url: editUrl,
        data: Editobj,
        traditional: true,
        success: function (data) {
            alert(data)

        }
    })
}
//刪除資料按鈕 請自行定義傳遞陣列方式
function delectCheck() {
    $('.delectBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');

    DeleteID = "";



    $('td[class=td_cbox] *').each(function () {
        if ($(this).is(':checked')) {
            DeleteID += $(this).attr('id') + ",";

        }
    });
    console.log(DeleteID);
    //下方定義傳遞方式    
    $.ajax({
        url: deleteurl,
        type: "post",
        dataType: "text",
        async: false,
        data: { DeleteID: DeleteID },
        success: function (result) {
            alert(result)
            GridData()
        }
    })
}
function deleteCheck() {
    $('.delectBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');

    seachInputValue = []
    inputLength = document.querySelectorAll('input[name="seachTextInput"]').length;
    for (i = 0; i < inputLength; i++) {

        value = document.querySelectorAll('input[name="seachTextInput"]')[i].value;
        obj = value
        seachInputValue.push(obj);
    }
    console.log(seachInputValue)
    seachobj = '';
    for (x = 0; x < AjaxSelect.length; x++) {

        seachobj += AjaxSelect[x] + '=' + seachInputValue[x] + '&'

    }
    $(".inputBox input").val("");
    console.log(seachobj)
    //下方定義傳遞方式    
    $.ajax({
        url: deleteurl,
        type: "post",
        dataType: "text",
        async: false,
        data: seachobj,
        success: function (result) {
            alert(result)
            seachobj = []
            GridData()

        }
    })
}
function deleteScheduleCheck() {

    $('.delectBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');

    var ScheduleDate = $("#Date").val();
    var SchedulePoNo = $("#PoNo").val();

    //下方定義傳遞方式    
    $.ajax({
        url: deleteurl,
        type: "post",
        dataType: "text",
        async: false,
        data: { ScheduleDate: ScheduleDate, SchedulePoNo: SchedulePoNo },
        success: function (result) {
            alert(result)

            GridData()



        }
    })
    var ScheduleDate = $("#Date").val('');
    var SchedulePoNo = $("#PoNo").val('');
}


function EditBtn(cellvalue, options, rowObject) {
    if (title == '刷卡紀錄') {
        if ((Admin == "true" || Admin == "SuperAdmin")) {
            return ' <a href="#" id=' + rowObject.EditID + ' class="seachBTN btn-1" style="width:50px" onclick="edit(this)">' + rowObject.AttendType + '</a>';

        } else {

            return rowObject.AttendType;

        }

    } else {
        return ' <a href="#" id=' + rowObject.EditID + ' class="seachBTN btn-1" style="width:50px" onclick="edit(this)">修改</a>';
    }

}
function DeleteBtn(cellvalue, options, rowObject) {
    return ' <a href="#"  id=' + cellvalue + ' class="seachBTN btn-1" onclick="delect(this)">刪除</a>';
}

function CheckBox(cellvalue, options, rowObject) {
    var reg = RegExp("false");
    //判斷ChildGrid是否開啟
    if (reg.exec(cellvalue)) {
        return '<input type="checkbox" onclick="return false">';
    }
    else {
        return '<input type="checkbox" checked onclick="return false" >';
    }
}

function DelCheckBox(cellvalue, options, rowObject) {

    //判斷ChildGrid是否開啟

    return '<input type="checkbox" id=' + cellvalue + '>';

}
function ResetInput() {

    $('.insertPopUpContant').empty();


    $('.editPopUpContant').empty();

    for (i = 0; i < editPopGenerateResult.length; i++) {
        $('.editPopUpContant').append('<div class="PopSeachBox" stlye="display:flex;"><label>' + editPopGenerateResult[i] + '</label><input autocomplete="off" type="' + editPopGenerateTypeResult[i] + '" name="editTextInput" placeholder="' + editPopGenerateResult[i] + '" required="required" /></div>')
        $('input[type=Select]').replaceWith('<input type="text" autocomplete="off" onfocus=this.value="" name="editTextInput" id="editPopInput' + i + '" class="editInputOption" list="editInputOption' + [i] + '"><datalist id="editInputOption' + [i] + '"></datalist>')
    }

    for (i = 0; i < PopGenerateResult.length; i++) {
        $('.insertPopUpContant').append('<div class="PopSeachBox" stlye="display:flex;"><label>' + PopGenerateResult[i] + '</label><input autocomplete="off" type="' + PopGenerateTypeResult[i] + '" name="insertTextInput" placeholder="' + PopGenerateResult[i] + '" required="required" /></div>')
        $('input[type=Select]').replaceWith('<input type="text" autocomplete="off" onfocus=this.value="" name="insertTextInput" id="editPopInput' + i + '" class="insertInputOption" list="insertInputOption' + [i] + '"><datalist id="insertInputOption' + [i] + '"></datalist>')
    }
    // for (i = 0; i < editPopGenerateResult.length; i++) {
    //     if (editPopGenerateTypeResult[i] == "select") {
    //         inputOption('editInputOption' + [i], dataList, '', '');
    //     }
    // }


}

function Setting() {
    $('.popSettingBox').fadeIn(700);
    $('.cover').removeClass('blur-out').addClass('blur-in')
    $('#pw').val('');
}


function Settingcheck() {
    $('.popSettingBox').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    Password = $('#pw').val();

    $.ajax({
        url: '/Webpage/EditPassword',
        type: "post",
        dataType: "text",
        async: false,
        data: { UserID: UserID, Password: Password },
        success: function (result) {
            alert(result)

        }
    })

}
