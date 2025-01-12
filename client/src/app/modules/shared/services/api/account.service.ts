import { Injectable } from "@angular/core";
import { CrudService } from "../../../core/services/crud.service";
import { GetAccountType } from "../../types/api/account-types/get-account.type";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})

export class AccountService extends CrudService<GetAccountType, any, any>
{
    constructor(http : HttpClient){
        super(http, 'account');
    }
}