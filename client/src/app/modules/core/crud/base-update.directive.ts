import { Directive, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CrudService } from '../services/crud.service';
import { ApiManager } from '../api/api-manager';
import { ToastService } from '../../shared/services/ui/toasts/toast.service';
import { LoadingService } from '../../shared/services/loading.service';
import { FormSettingType } from '../../shared/types/ui/form.type';
import { mapFormToModel, mapModelToForm } from '../utils/mapper/mapper';
import { FormGroup } from '@angular/forms';

@Directive({
  standalone: true,
})
export class BaseUpdateComponent<TGet, TPut, TService extends CrudService<TGet, any, TPut>> implements OnInit
{
    @Input() id! : string;
    @Output() editedDataEvent : EventEmitter<void> = new EventEmitter;

    constructor(
        protected apiManager : ApiManager<TGet>,
        protected updateApiManager : ApiManager<TGet>, 
        protected service : TService,
        private toastService : ToastService,
        private loadingService : LoadingService,
        public form : FormSettingType)
        {
            
        }

    ngOnInit(): void 
    {
        this.apiManager.exeApiRequest(this.service.fetchSingle(this.id), () => this.completeForm())
    }

    private completeForm() : void{
        this.form.formGroup = mapModelToForm(this.apiManager.data(), this.form.formGroup)
    }

    onFormSubmit(form : FormGroup) : void{
        const mappedData : TPut = mapFormToModel(form);
        this.updateApiManager.exeApiRequest(this.service.update(this.id, mappedData), () => this.onSuccessEdit());
    }

    protected onSuccessEdit() : void{
        this.editedDataEvent.emit();
    }

}
