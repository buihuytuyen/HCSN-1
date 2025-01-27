﻿namespace Domain.Entity
{
    public class Department : BaseEntity
    {
        #region Fields
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string DepartmentCode { get; set; } 
        #endregion
    }
}
