import { FormGroup } from "@angular/forms";
import { ApiManager } from "../../core/api/api-manager";
import { AuthService } from "../services/auth.service";
import { FormSettingType } from "../../shared/types/ui/form.type";
import { mapFormToModel } from "../../core/utils/mapper/mapper";
import { Directive, Inject } from "@angular/core";

@Directive()
export abstract class BaseAuthComponent<ResponseDataType, RequestDataType>{
    form : FormSettingType;

    constructor(
        @Inject(AuthService) protected authService: AuthService, 
        @Inject(ApiManager) public apiManager: ApiManager<ResponseDataType>, 
        form : FormSettingType){
        this.form = form;
    }

    abstract onFormSubmit(form : FormGroup) : void;

    protected convertForm(form : FormGroup) : RequestDataType
    {
        const mappedForm : RequestDataType = mapFormToModel(form);
        return mappedForm;
    }
}