import { Directive, EventEmitter, Inject, Output } from "@angular/core";
import { FormSettingType } from "../../shared/types/ui/form.type";
import { ApiManager } from "../api/api-manager";
import { FormGroup } from "@angular/forms";
import { mapFormToModel } from "../utils/mapper/mapper";
import { ModalFormService } from "../../admin/services/ui/modal-form.service";
import { CrudService } from "../services/crud.service";
import { ToastService } from "../../shared/services/ui/toasts/toast.service";

@Directive()
export abstract class BaseCreateComponent<ResponseDataType, RequestDataType, TService extends CrudService<ResponseDataType, RequestDataType, any>>{
    
    @Output() createdDataEvent: EventEmitter<ResponseDataType> = new EventEmitter<ResponseDataType>();
    form : FormSettingType;

    constructor(
        protected modalFormService : ModalFormService,
        protected service: TService, 
        @Inject(ApiManager) public apiManager: ApiManager<ResponseDataType>, 
        form : FormSettingType){
        this.form = form;
    }

    onFormSubmit(form : FormGroup) : void{
        const mappedData : RequestDataType = mapFormToModel(form);
        this.apiManager.exeApiRequest(this.service.create(mappedData), () => this.onSuccess())
    };

    protected onSuccess() : void{
        this.createdDataEvent.emit(this.apiManager.data());
    }
}