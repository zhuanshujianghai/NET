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
    
    public partial class view_oa_member_positionfunctionlist
    {
        public long id { get; set; }
        public long companyid { get; set; }
        public long positionid { get; set; }
        public long functionid { get; set; }
        public string positionname { get; set; }
        public string positioninfo { get; set; }
        public int status { get; set; }
        public string functionname { get; set; }
        public string functioninfo { get; set; }
        public long parentid { get; set; }
    }
}
