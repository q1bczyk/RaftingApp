import { Directive, Inject } from "@angular/core";
import { FormSettingType } from "../../shared/types/ui/form.type";
import { ApiManager } from "../api/api-manager";
import { BaseApiService } from "../services/base-api.service";
import { FormGroup } from "@angular/forms";
import { mapFormToModel } from "../utils/mapper/mapper";
import { ModalFormService } from "../../admin/services/ui/modal-form.service";

@Directive()
export abstract class BaseCreateComponent<ResponseDataType, RequestDataType, TService extends BaseApiService>{
    form : FormSettingType;

    constructor(
        private modalFormService : ModalFormService,
        protected service: TService, 
        @Inject(ApiManager) public apiManager: ApiManager<ResponseDataType>, 
        form : FormSettingType){
        this.form = form;
    }

    onFormSubmit(form : FormGroup) : void{
        this.modalFormService.closeModal();
    };

    private convertForm(form : FormGroup) : RequestDataType
    {
        const mappedForm : RequestDataType = mapFormToModel(form);
        return mappedForm;
    }
}