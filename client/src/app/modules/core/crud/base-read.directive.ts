import { Directive, OnInit } from '@angular/core';
import { CrudService } from '../services/crud.service';
import { ApiManager } from '../api/api-manager';
import { ApiSuccessResponse } from '../types/api-success-response.type';
import { ConfirmationModalService } from '../../shared/services/confiramtion-modal.service';
import { ToastService } from '../../shared/services/ui/toasts/toast.service';
import { LoadingService } from '../../shared/services/loading.service';
import { ModalFormService } from '../../admin/services/ui/modal-form.service';

@Directive({
  standalone: true
})
export class BaseReadDirective<TGet, TService extends CrudService<TGet, any, any>> implements OnInit
{
  constructor(
    public modalFormService : ModalFormService,
    protected service : TService, 
    public apiManager : ApiManager<TGet[]>, 
    public confirmationModalService : ConfirmationModalService,
    protected apiDeleteManager : ApiManager<ApiSuccessResponse>,
    private toastService : ToastService,
    public loadingService : LoadingService
    )
    {}

  ngOnInit(): void {
    this.apiManager.exeApiRequest(this.service.fetchAll());
  }

  deleteApiRequest(dataId : string) : void{
    this.apiDeleteManager.exeApiRequest(this.service.delete(dataId), () => this.onSuccessDelete());
  }

  private onSuccessDelete() : void {
    this.apiManager.exeApiRequest(this.service.fetchAll());
    this.toastService.showToast('Pomyślnie usunięto dane', 'success');
  }

  openModal() : void{
    this.modalFormService.openModal();
  }

  onSuccessEquipmentAdd(data : TGet) : void{
    this.apiManager.exeApiRequest(this.service.fetchAll());
    this.toastService.showToast('Pomyślnie dodano sprzęt', 'success');
    this.modalFormService.closeModal();
  }
}
