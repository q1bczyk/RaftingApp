import { NgbDateStruct, NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap"
import { dateFormat } from "../../../../core/date-format/date-format";
import { format } from 'date-fns'

export function convertTime(time: Date): NgbTimeStruct {
    const formatedTime: NgbTimeStruct = {
        hour: time.getUTCHours(),
        minute: time.getUTCMinutes(),
        second: time.getUTCSeconds()
    }
    return formatedTime;
}

export function dateToString(date: Date): string {
    return format(date, dateFormat);
}

export function dateToNgbStruct(date: Date): NgbDateStruct {
    return {
        year: date.getFullYear(),
        month: date.getMonth() + 1,
        day: date.getDate(),
    };
}

export function structToDate(date : NgbDateStruct, time : NgbTimeStruct) : Date{
    return new Date(Date.UTC(date.year, date.month - 1, date.day, time.hour, time.minute, time.second));
}