//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OA_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class view_oa_notice_noticecomment
    {
        public long id { get; set; }
        public long noticeid { get; set; }
        public long companyid { get; set; }
        public string comment { get; set; }
        public long staffid { get; set; }
        public string username { get; set; }
        public System.DateTime addtime { get; set; }
        public int status { get; set; }
    }
}
