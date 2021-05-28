//頁面變動依據資料庫生成 無須更改前端架構 樣式如需修改僅需修改MXIC.css檔
//該頁面表格需使用之api請先定義

var marqueeContant=[];
var marqueeUrl="/Webpage/marqueeUrl";
//navBar & indexBTN 資料格式範例 
var navData = [
    { 'name': '人員管理', 'url': '../UserManagement/', 'commonly_used': '1' ,'Img':'人員權限管理1x'},
    { 'name': '部門管理', 'url': '../DepartmentManagement/', 'commonly_used': '1','Img':'部門權限管理1x' },
    { 'name': '廠商管理', 'url': '../VendorManagement/', 'commonly_used': '1','Img':'廠商權限管理1x' },
    { 'name': '報價單', 'url': '../Quotation/', 'commonly_used': '1','Img':'報價單1x' },
    { 'name': '班表設定', 'url': '../ScheduleSetting/', 'commonly_used': '1','Img':'班表1x' },
    { 'name': '證照管理', 'url': '../LisenceManagement/', 'commonly_used': '1','Img':'證照管理1x' },
    { 'name': '刷卡紀錄', 'url': '../SwipeInfo/', 'commonly_used': '1', 'Img': '刷卡紀錄1x' },
    { 'name': '刷卡警報', 'url': '../SwipeInfo/Alarm', 'commonly_used': '1', 'Img': '刷卡警報1x' },
    { 'name': '班別時間設定', 'url': '../SwipeSet/', 'commonly_used': '1', 'Img': '班別時間設定1x' },
    { 'name': '匯出報表', 'url': '../ExportPO/', 'commonly_used': '1','Img':'計價單1x' }
]

//跑馬燈資料格式範例
// var marqueeContant = [
//     { 'type': 'test', 'contant': '這是一個測試' },
//     { 'type': 'alert', 'contant': '可能會爆炸' },
//     { 'type': 'test', 'contant': '不要再點下去了' },
//     { 'type': 'test', 'contant': '我覺得已經很完美' },
//     { 'type': 'test', 'contant': '只能給你放五個公告' }
// ]
$.ajax({
    async: false,
    cache: false,
    type: "post",
    datatype: "json",
    url: marqueeUrl,
    success: function (data) {
        marqueeContant = [];
        marqueeContant = JSON.parse(data);
        console.log(marqueeContant)
        
    }
})


$(document).ready(function () {
    UserID = $('#UserID').val();
 
    navbarItem(navData);
    iconMenu(navData);
    marquee(marqueeContant);
});
//表格返回自訂物件

//navbar 導覽列自動生成排列
function navbarItem(e) {
    var arrLength = e.length;
    var menuWidth = $('.navber').width();
    var menuBTN = (100 / arrLength)
    for (i = 0; i < arrLength; i++) {
        $('.navber > ul').append('<li><a href="./' + e[i].url + '">' + e[i].name + '</a></li>')
    }
    if (arrLength <= 8) {
        $('ul > li').css('width', '' + menuBTN + '%');
    } else {
        $('.navber > ul').css('flex-wrap', 'wrap')
    }
}

//index導覽按鈕生成
function iconMenu2(e) {
    var arrLength = e.length;
    var menuWidth = $('.indexBTN').width();
    for (i = 0; i < arrLength; i++) {
        if(i == 3){
            $('.indexBTN').append('<div class="btn"><div class="btnBackground link-container"><a class="link" href="./' + e[i].url + '"><img src="../Img/ICON/'+e[i].Img+'.png"  title="'+e[i].name+'"/></a></div></div><br>')
        }else{
            $('.indexBTN').append('<div class="btn"><div class="btnBackground link-container"><a class="link" href="./' + e[i].url + '"><img src="../Img/ICON/'+e[i].Img+'.png"   title="'+e[i].name+'" /></a></div></div>')
        }
    }
}

// 

//index導覽按鈕生成
function iconMenu(e) {
    var arrLength = e.length;
    var menuWidth = $('.indexBTN').width();
    for (i = 0; i < arrLength; i++) {
        if (i == 2) {
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
    $('.marqueeContant').append('<div style="display:flex; height:20px; margin:0 20px 0 0"><p style="color:red; margin-right:10px">員工姓名：' + data[num].EmpName + '<p style="color:red; margin-right:10px">證照名稱：' + data[num].LicName + '</p><p style="color:red; margin-right:10px">到期日：' + data[num].EndDate + '</p></div>')
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
        data: { UserID: UserID ,Password:Password},
        success: function (result) {
            alert(result)
           
        }
    })

}

function cancel() {
    $('.popUp').fadeOut(1000);
    $('.cover').removeClass('blur-in').addClass('blur-out');
    ResetInput();

}