import { NgbDateStruct, NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import { dateToString } from "../../helpers/dateConventer";

export interface DateSettingsType {
    openTime: NgbTimeStruct,
    closeTime: NgbTimeStruct,
    openDate: NgbDateStruct,
    closeDate: NgbDateStruct
}

export function dateSettingsInit(): DateSettingsType {
    const currentDate = new Date();

    return {
        openTime: { hour: 13, minute: 30, second: 0 },
        closeTime: { hour: 13, minute: 30, second: 0 },
        openDate: { year: currentDate.getFullYear(), month: currentDate.getMonth(), day: currentDate.getDay() },
        closeDate: { year: currentDate.getFullYear(), month: currentDate.getMonth(), day: currentDate.getDay() }
    }
}