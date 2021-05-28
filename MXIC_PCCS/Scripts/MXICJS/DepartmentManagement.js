$(document).ready(function () {
   //下拉式選單
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: "/Webpage/DepartmentManagementSelect",
    
        success: function (data) {
            dataList = [];
            dataList = JSON.parse(data);
            console.log(dataList);
           
            
        }})

        $.ajax({
            async: false,
            cache: false,
            type: "post",
            datatype: "json",
            url: "/DepartmentManagement/Venderdata",
        
            success: function (data) {
                Venderdata = [];
                Venderdata = JSON.parse(data);
                console.log(Venderdata);
               
               
            }})
    
    
         
      
        var GridHeight =100;
            $("#gridAdd").jqGrid({
                colModel: [
                   
                    {
                       
                        label: '',
                        name: 'VendorName',
                        width:50,
                        align: 'center',
                        formatter: VendorCheckBox 
                    }, 
                    {
                        label: '廠商名稱',
                        name: 'VendorName',
                        width:250,
                        align: 'center'
                    },
        
                  
        
                ],
                data: Venderdata,
                height: GridHeight,
                //guiStyle: "bootstrap4",
                iconSet: "fontAwesome",
                pageSize: "10",
                idPrefix: "gb1_",
                rownumbers: true,
                sortname: "invdate",
                sortorder: "desc",
                 pager: true,
                // rowNum: pageRow,
        });
      
                
        
    
    })
    //ready結束........


//客製新增按鈕
function checkadd() {

   

    var DepName=$('#editPopInput0').val();

    VendorName='';
    $(' input[type=checkbox]').each(function(){
        
        if($(this).is(':checked')){
            VendorName+=$(this).attr('value')+",";
            
        }
    });


    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "text",
        url: "/DepartmentManagement/AddDepToVen",
        data: { DepName: DepName,VendorName:VendorName },
        success: function (data) {
            alert(data)
           
            GridData()
            // $("#DepartmentManagementAdd input").val("");
           $('.popUp').fadeOut(1000);
        $('.cover').removeClass('blur-in').addClass('blur-out');
        ResetInput();
       
       

  
  
        }})
        
       
   
}

function insertDepartmentManagement()
{
    $('#editPopInput0').val("");

    $(' input[type=checkbox]').attr('checked', false)
    $('.insertBox').fadeIn(700);

$('.cover').removeClass('blur-out').addClass('blur-in')

inputOption('editInputOption0', dataList, '', '');
Venderdata=[]
    $.ajax({
        async: false,
        cache: false,
        type: "post",
        datatype: "json",
        url: "/DepartmentManagement/Venderdata",
    
        success: function (data) {
            Venderdata = [];
            Venderdata = JSON.parse(data);
            console.log(Venderdata);
            $("#gridAdd").jqGrid('clearGridData');
            mydata = [];
            mydata = JSON.parse(data);
            $("#gridAdd").jqGrid('setGridParam', { data: mydata });
            // refresh the grid
            $("#gridAdd").trigger('reloadGrid');
           
           
        }})


     
  
    var GridHeight =100;
        $("#gridAdd").jqGrid({
            colModel: [
               
                {
                   
                    label: '',
                    name: 'VendorName',
                    width:50,
                    align: 'center',
                    formatter: VendorCheckBox 
                }, 
                {
                    label: '廠商名稱',
                    name: 'VendorName',
                    width:250,
                    align: 'center'
                },
    
              
    
            ],
            data: Venderdata,
            height: GridHeight,
            //guiStyle: "bootstrap4",
            iconSet: "fontAwesome",
            pageSize: "10",
            idPrefix: "gb1_",
            rownumbers: true,
            sortname: "invdate",
            sortorder: "desc",
             pager: true,
            // rowNum: pageRow,
    });
    


}

function VendorCheckBox(cellvalue, options, rowObject) 
{if(1!==0){
    return '<input type="checkbox" value="'+cellvalue+'" style="width:20px">';
}
else{
    return''
}

    
    
}