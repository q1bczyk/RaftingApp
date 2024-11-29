import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { CrudService } from "../../../core/services/crud.service";
import { GetEquipmentType } from "../../types/api/equipment-types/get-equipment.type";

@Injectable({
    providedIn: 'root'
})

export class EquipmentService extends CrudService<GetEquipmentType, any, any>
{
    constructor(protected override http : HttpClient){
        super(http, 'equipmentType');
    }
}