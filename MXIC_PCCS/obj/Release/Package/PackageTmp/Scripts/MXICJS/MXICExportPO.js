//頁面變動依據資料庫生成 無須更改前端架構 樣式如需修改僅需修改MXIC.css檔
//該頁面表格需使用之api請先定義

var dataList = [];
var ajaxUrl = [];
var seachobj = '';
generateUrl = '/Webpage/PageGenerate';
var tablename = ""
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
} else if (title == '部門管理') {
    //搜尋url這個要先寫
    ajaxUrl = '/DepartmentManagement/SearchDepToVen'
    //刪除url
    deleteurl = "/DepartmentManagement/DeleteDepToVen"
    //table名稱
    tablename = "MXIC_DepartmentManagement"
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
} else if (title == '報價單') {


    //搜尋url這個要先寫
    ajaxUrl = "/Quotation/SearchQuotation"
    //table名稱
    tablename = "MXIC_Quotation"


} else if (title == '班表設定') {
    //搜尋url這個要先寫
    ajaxUrl = "/ScheduleSetting/ScheduleList"
    //table名稱
    tablename = "MXIC_ScheduleSetting"
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
} else if (title == '刷卡紀錄') {
    tablename = "MXIC_View_Swipe"
    ajaxUrl = "/SwipeInfo/CheckinList"
    editDetailUrl = "/SwipeInfo/SwipeInfoDetail"
    editUrl = "/SwipeInfo/EditSwipe"
} else if (title == '匯出計價單') {
    ajaxUrl="/ExportPO/DownloadQuotation";
    tablename="MXIC_ExportPO";
   
}

//navBar & indexBTN 資料格式範例 
var navData = [
    { 'name': '人員管理', 'url': '../UserManagement/', 'commonly_used': '1' },
    { 'name': '部門管理', 'url': '../DepartmentManagement/', 'commonly_used': '1' },
    { 'name': '廠商管理', 'url': '../VendorManagement/', 'commonly_used': '1' },
    { 'name': '報價單', 'url': '../Quotation/', 'commonly_used': '1' },
    { 'name': '班表設定', 'url': '../ScheduleSetting/', 'commonly_used': '1' },
    { 'name': '證照管理', 'url': '../LisenceManagement/', 'commonly_used': '1' },
    { 'name': '刷卡紀錄', 'url': '../SwipeInfo/', 'commonly_used': '1' },
    { 'name': '匯出報表', 'url': '../ExportPO/', 'commonly_used': '1' }
]





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



// 取得查詢AJAX查詢條件
// var AjaxSelect = $.map(inputGenerate, function (item, index) {
//     if (item.Generate == '1') {
//         return item.COLUMN_NAME
//     }
// })




// //------------------------動態生成---------------------


// //取得頁面應動態生成INPUT資料
// var GenerateResult = $.map(inputGenerate, function (item, index) {
//     if (item.Generate == '1') {
//         return item.Remarks
//     }
// })
// console.log(GenerateResult)
// //取得頁面應動態生成INPUT格式
// var GenerateTypeResult = $.map(inputGenerate, function (item, index) {
//     if (item.GenerateType !== '0' && item.Generate == '1') {
//         return item.GenerateType
//     }
// })
// console.log(GenerateTypeResult)
// //取得Pop應動態生成INPUT資料
// var PopGenerateResult = $.map(inputGenerate, function (item, index) {
//     if (item.PopGenerate == '1') {
//         return item.Remarks
//     }
// })
// //取得Pop應動態生成INPUT 格式
// var PopGenerateTypeResult = $.map(inputGenerate, function (item, index) {
//     if (item.AddPopGenerate !== '0' && item.PopGenerate == '1') {
//         return item.AddPopGenerate
//     }
// })

// //取得描述應動態生成表頭資料
// var RemarksResult = $.map(inputGenerate, function (item, index) {
//     if (item.Remarks !== '' && item.GridTitleGenerate == 1) {
//         return item.Remarks
//     }
// })

// //取得修改Pop應動態生成INPUT資料
// var editPopGenerateResult = $.map(inputGenerate, function (item, index) {
//     if (item.PopGenerate == '1' && item.EditPopGenerate !== '0') {
//         return item.Remarks
//     }
// })

// //取得Pop應動態生成INPUT 格式
// var editPopGenerateTypeResult = $.map(inputGenerate, function (item, index) {
//     if (item.PopGenerate == '1' && item.EditPopGenerate !== '0') {
//         return item.EditPopGenerate
//     }
// })




// console.log(RemarksResult)
//------------------------動態生成---------------------------------

//---------------------------------------------------------------------------------------------------------------------
$(document).ready(function () {

    UserID = $('#UserID').val();
    Admin="";

    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: '/Webpage/Admin',
        data: { UserID: UserID },
        // error: function () {
        //     alert('欄位生成異常，目前為範例資料')
        // },
        success: function (data) {
            Admin="";

            Admin=data;
            if(Admin=="false"){

                $('.Manager').attr("style", "display:none;");

            }
           
        }
    })
    $('.title').html(title);


    //-----------------------------------------------------以下新版動態生成
    // for (i = 0; i < GenerateResult.length; i++) {
    //     if (GenerateTypeResult[i] == "date") {
    //         $('.inputBox').append(GenerateResult[i] + '<input type="' + GenerateTypeResult[i] + '" name="seachTextInput" placeholder="' + GenerateResult[i] + '" required="required" />')
    //     } else {
    //         $('.inputBox').append('<input type="' + GenerateTypeResult[i] + '" name="seachTextInput" placeholder="' + GenerateResult[i] + '" required="required" />')
    //         $('input[type=select]').replaceWith(GenerateResult[i] + '<input  onfocus=this.value="" type="text" name="seachTextInput" id="" class="editInputOption" list="seachInputOption' + [i] + '"><datalist id="seachInputOption' + [i] + '"></datalist>')
            
    //     }
    // };
 





    


  

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
        $('.navber > ul').append('<li><a href="./' + e[i].url + '">' + e[i].name + '</a></li>')
    }
    if (arrLength <= 9) {
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
    ExportPO()

}


///匯出方法
function ExportPO() {

    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "text",
        url: ajaxUrl,
        data: seachobj,
        traditional: true,
        success: function (data) {
            alert(data)
           
        }
    })
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












