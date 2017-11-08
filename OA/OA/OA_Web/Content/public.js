//获取时间日期部分,格式：2015-10-1
function ChangeDateFormat(val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    }
    return "";
}
//获取时间时间部分，格式：2015-10-1 08:22:33
function ChangeTimeFormat(val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var hour = date.gethour
        return date.getFullYear() + "-" + month + "-" + currentDate + " " + (date.getHours() < 10 ? "0" + date.getHours() : date.getHours() + "") + ":" + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes() + "") + ":" + (date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds() + "");
    }
    return "";
}