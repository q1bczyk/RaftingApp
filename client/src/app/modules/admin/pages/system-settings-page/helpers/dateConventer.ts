import { NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap"
import { dateFormat } from "../../../../core/date-format/date-format";

export function convertTime(time : Date) : NgbTimeStruct{
    const formatedTime : NgbTimeStruct = {
        hour : time.getHours(),
        minute : time.getMinutes(),
        second : time.getSeconds()
    }
    return formatedTime;
}

export function dateToString(date : Date) : string{
    return format(date, dateFormat);
}