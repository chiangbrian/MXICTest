//頁面變動依據資料庫生成 無須更改前端架構 樣式如需修改僅需修改MXIC.css檔
//該頁面表格需使用之api請先定義
var ajaxUrl=[];
//搜尋條件API
var generateUrl=[];
//菜單
var navUrl='.../api/api-1';
var title = document.title;
if(title == 'MXIC'){
    ajaxUrl ='.../api/api-1'
    generateUrl ='.../api/api-1'   
}else if(title == '人員管理'){
    ajaxUrl ='.../api/api-2'
    generateUrl ='.../api/api-2'
}else if(title == '部門管理'){
    ajaxUrl ='.../api/api-3'
    generateUrl ='.../api/api-3'
}else if(title == '廠商管理'){
    ajaxUrl ='.../api/api-4'
    generateUrl ='.../api/api-4'
}else if(title == '報價單'){
    ajaxUrl ='.../api/api-5'
    generateUrl ='.../api/api-5'
}else if(title == '班表設定'){
    ajaxUrl ='.../api/api-6'
    generateUrl ='.../api/api-6'
}else if(title == '證照管理'){
    ajaxUrl ='.../api/api-7'
    generateUrl ='.../api/api-7'
}else if(title == '刷卡紀錄'){
    ajaxUrl ='.../api/api-8'
    generateUrl ='.../api/api-8'
}else if(title == '匯出計價單'){
    ajaxUrl ='.../api/api-9'
    generateUrl ='.../api/api-9'
}

 

//navBar & indexBTN 資料格式範例 
var navData = [
    { 'name': '人員管理', 'url': '/UserManagement/Index', 'commonly_used':'1'},
    { 'name': '部門管理', 'url': '部門管理.html', 'commonly_used':'1' },
    { 'name': '廠商管理', 'url': '廠商管理.html', 'commonly_used':'1'},
    { 'name': '報價單', 'url': '報價單.html' ,'commonly_used':'1'},
    { 'name': '班表設定', 'url': '班表設定.html', 'commonly_used':'1' },
    { 'name': '證照管理', 'url': '證照管理.html' , 'commonly_used':'1'},
    { 'name': '刷卡紀錄', 'url': '刷卡紀錄.html' , 'commonly_used':'1'},
    { 'name': '匯出計價單', 'url': '匯出計價單.html' , 'commonly_used':'0'}
]

//動態生成input 資料形式範例  Remarks為描述欄位  Generate為是否啟用動態生成 有一才生成
var inputGenerate = [
    { "COLUMN_NAME": "DepartmentNo", "Remarks": "部門代號", "Generate": "1" ,"PopGenerate":"1"},
    { 'COLUMN_NAME': 'DepartmentName', 'Remarks': '部門名稱', "Generate": "1" ,"PopGenerate":"1"},
    { 'COLUMN_NAME': 'UserName', 'Remarks': '人員姓名', 'Generate': "1" ,"PopGenerate":"1"},
    { 'COLUMN_NAME': 'UserNO', 'Remarks': '人員編號', 'Generate': "1" ,"PopGenerate":"1"},
    { 'COLUMN_NAME': 'Admin', 'Remarks': 'Admin', 'Generate': "0" ,"PopGenerate":"1"},
    { 'COLUMN_NAME': 'PassWord', 'Remarks': '密碼', 'Generate': "0" ,"PopGenerate":"1"}
];
//跑馬燈資料格式範例
var marqueeContant = [
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
// $.ajax({
//     async: false,
//     cache: false,
//     type: "post",
//     datatype: "json",
//     url: navUrl,
//     error: function () {
//         alert('Navber異常，目前為範例資料')
//     },
//     success: function (data) {
//         navData = [];
//         navData = data;
//     }
// })

//搜尋欄位生成 更改ajax url屬性與篩選條件
// $.ajax({
//     async: false,
//     cache: false,
//     type: "post",
//     datatype: "json",
//     url: generateUrl,
//     error: function () {
//         alert('欄位生成異常，目前為範例資料')
//     },
//     success: function (data) {
//         inputGenerate = [];
//         inputGenerate = data;
//     }
// })

//表格內容 更改ajax url屬性與篩選條件
// $.ajax({
//     async: false,
//     cache: false,
//     type: "post",
//     datatype: "json",
//     url: ajaxUrl,
//     data: {AjaxSelect},
//     error: function () {
//         alert('表單查詢失敗，目前為範例資料')
//     },
//     success: function (data) {
//         mydata = [];
//         mydata = data;
//     }
// })

//取得查詢AJAX查詢條件
var AjaxSelect = $.map(inputGenerate, function (item, index) {
    if (item.Generate == '1') {
        return item.COLUMN_NAME
    }
})

//取得應動態生成INPUT資料
var GenerateResult = $.map(inputGenerate, function (item, index) {
    if (item.Generate == '1') {
        return item.Remarks
    }
})

//取得Pop應動態生成INPUT資料
var PopGenerateResult = $.map(inputGenerate, function (item, index) {
    if (item.PopGenerate == '1') {
        return item.Remarks
    }
})


$(document).ready(function () {
   
    navbarItem(navData);
    iconMenu(navData);
    marquee(marqueeContant);
});

//navbar 導覽列自動生成排列
function navbarItem(e) {
    var arrLength = e.length;
    var menuWidth = $('.navber').width();
    var menuBTN = (100 / arrLength)
    for (i = 0; i < arrLength; i++) {
        $('.navber > ul').append('<li><a href="http://localhost:58627/' + e[i].url + '">' + e[i].name + '</a></li>')
    }
    if (arrLength <= 8) {
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
        if(i == 3){
            $('.indexBTN').append('<div class="btn"><div class="btnBackground link-container"><a class="link" href="./' + e[i].url + '">' + e[i].name + '</a></div></div><br>')
        }else{
            $('.indexBTN').append('<div class="btn"><div class="btnBackground link-container"><a class="link" href="./' + e[i].url + '">' + e[i].name + '</a></div></div>')
        }
    }
}

var marqueeTop = 0;
//跑馬燈 按鈕向下
function marqueeDown() {
    if (marqueeTop <= -72) {
        marqueeTop = -72;
    } else {
        marqueeTop -= 18
    }
    $('.marqueeContant').css('transform', 'translateY(' + marqueeTop + 'px)')
}
//跑馬燈 按鈕向上
function marqueeUp() {
    if (marqueeTop < 0) {
        marqueeTop += 18;
    } else {
        marqueeTop = 0;
    }
    $('.marqueeContant').css('transform', 'translateY(' + marqueeTop + 'px)')
}
//跑馬燈 內容
function marquee(e) {
    for (i = 0; i <= e.length; i++) {
        if (e.length <= 5) {
            $('.marqueeContant').append('<div style="display:flex; height:18px;"><p style="color:red; margin-right:10px">' + e[i].type + '</p><p style="line-height: 18px;">' + e[i].contant + '</p></div>')
        }
    }
}


//開啟"新增"彈窗
function insert() {
    $('.insertBox').fadeIn(700);
    $('.cover').removeClass('blur-out').addClass('blur-in')
}
// //開啟"刪除"彈窗
// function delect() {
//     $('.delectBox').fadeIn(700);
//     $('.cover').removeClass('blur-out').addClass('blur-in')
// }
//關閉彈窗
function cancel() {
    $('.popUp').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    //清空POP視窗 輸入框數值
    $('.popUpContant > input').val('');
}
//確認表單按鈕 請自行定義傳遞陣列方式

