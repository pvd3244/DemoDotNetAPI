
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public string PeopleID { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        public string? AddressRange { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public DateTime? DateRange { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string? SingerCode { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public decimal? Salary { get; set; }

        /// <summary>
        /// Ngày gia nhập
        /// </summary>
        public DateTime? DateOfJoin { get; set; }

        /// <summary>
        /// Tình trạng làm việc
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// ID vị trí
        /// </summary>
        public int? PositionCode { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public int? DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
