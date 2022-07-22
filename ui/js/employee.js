class Employee{
    //PhamDinh(13/7/2022): Tạo hàm khởi tạo nhân viên
    constructor(){
        let me = this;
        //Hàm khởi tạo sự kiện
        me.initEvents();
    }
    //PhamDinh(13/7/2022): Tạo hàm khởi tạo sự kiện
    initEvents(){
        let me = this;
        //Hàm khởi tạo sự kiện của bảng thông tin nhân viên
        me.initEventsTable();
        //Hàm khởi tạo sự kiện của các chức năng Thêm, Xóa, Nhân bản, Tải lại
        me.initEvensFunction();
        //Hàm khởi tạo phân trang
        me.initEventsPage();
    }
    
    //PhamDinh(13/7/2022): Tạo hàm khởi tạo sự kiện của bảng thông tin nhân viên
    initEventsTable(){
        let me = this;
        //Xử lý khi click vào dòng thông tin nhân viên
        $(`#EmployeeData tr`).off("click");
        $(`#EmployeeData`).on("click","tr", function(){
            //Đánh dấu dòng dữ liệu đã chọn
            $(`#EmployeeData`).find(".selected-tr").removeClass("selected-tr");
            $(this).addClass("selected-tr");
        })
        //Xử lý khi double click vào dòng thông tin nhân viên
        $(`#EmployeeData tr`).off("dblclick");
        $(`#EmployeeData`).on("dblclick","tr",function(){
            let me = this;
            let trSelected = $("#EmployeeData tr.selected-tr");
            let employeeCode = $(trSelected).children()[0].textContent;
            StartForm();
            DataForm(employeeCode);
            //Lưu trữ dữ liệu
            $('#ConfirmAdd').off("click");
            $('#ConfirmAdd').on("click", function(){
                UpdateData(employeeCode);
            });
        });
    }
    //PhamDinh(13/7/2022): Tạo hàm khởi tạo sự kiện của các chức năng Thêm, Xóa, Nhân bản, Tải lại
    initEvensFunction(){
        let me = this;
        //Chức năng xóa
        $('#Delete').click(me.DeleteForm);
        //Chức năng thêm
        $('#Add').click(me.AddForm);
        //Chức năng nhân bản
        $('#Replication').click(me.Replication);
        //Chức năng làm tươi
        $('#Refresh').click(me.Refresh);
    }
    //PhamDinh(13/7/2022): Tạo hàm xử lý các thao tác trên delete form
    DeleteForm(){
        let me = this;
        let trSelected = $("#EmployeeData tr.selected-tr");
        if(trSelected.length > 0){
            let employeeCode = $(trSelected).children()[0].textContent;
            $('#DeleteCode').text(employeeCode);
            //Ẩn form còn lại
            $('#AddForm').hide();
            //Hiện form xóa
            $('#DeleteForm').show();
            //Ẩn form hiện tại
            $('#XDeleteForm').off("click");
            $('#XDeleteForm').on("click", function(){
                $('#DeleteForm').hide();
            });
            //Ẩn form hiện tại
            $('#CancelDelete').off("click");
            $('#CancelDelete').on("click", function(){
                $('#DeleteForm').hide();
            });
            //Xác nhận xóa dữ liệu
            $('#ConfirmDelete').off("click");
            $('#ConfirmDelete').on("click", function(){
                let Url = "http://localhost:54578/api/v1/Employees/"+employeeCode;
                $.ajax({
                    url: Url,
                    method: "DELETE"
                })
                .done(function(status){
                    getData();
                    $('#DeleteForm').hide();
                })
                .fail(function(status){
                    alert("Error");
                })
            });
        }
        else{
            alert("Bạn chưa chọn dòng dữ liệu nào.");
        }
        
    }
    //PhamDinh(13/7/2022): Tạo hàm xử lý các thao tác trên form thêm mới nhân viên
    AddForm(){
        let me = this;
        StartForm();
        //Lưu trữ dữ liệu
        $('#ConfirmAdd').off("click");
        $('#ConfirmAdd').on("click", function(){
            ConfirmAdd();
        });
    }
    
    //PhamDinh(13/7/2022): Tạo hàm xử lý các thao tác trên form nhân bản dữ liệu
    Replication(){
        let me = this;
        let trSelected = $("#EmployeeData tr.selected-tr");
        if(trSelected.length > 0){
            let employeeCode = $(trSelected).children()[0].textContent;
            StartForm();
            DataForm(employeeCode);
            //Lưu trữ dữ liệu
            $('#ConfirmAdd').off("click");
            $('#ConfirmAdd').on("click", function(){
                ConfirmAdd();
            });
        }
        else{
            alert("Bạn chưa chọn dòng dữ liệu nào.");
        }
    }
    //PhamDinh(13/7/2022): Tạo hàm xử lý làm tươi bảng dữ liệu nhân viên
    Refresh(){
        let me = this;
        //Lấy dữ liệu
        getData();
    }
    initEventsPage(){
        let me = this;
    }
}
var employee = new Employee();