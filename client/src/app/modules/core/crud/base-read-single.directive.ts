import { Directive, Input, OnInit } from '@angular/core';
import { CrudService } from '../services/crud.service';
import { ApiManager } from '../api/api-manager';
import { ConfirmationModalService } from '../../shared/services/confiramtion-modal.service';
import { LoadingService } from '../../shared/services/loading.service';
import { ModalFormService } from '../../admin/services/ui/modal-form.service';

@Directive({
  standalone: true,
})
export class BaseReadSingleComponent<TGet, TPut, TService extends CrudService<TGet, any, TPut>> implements OnInit
{
    @Input() id! : string;

  constructor(
    public modalFormService : ModalFormService,
    protected service : TService, 
    public apiManager : ApiManager<TGet>, 
    public confirmationModalService : ConfirmationModalService,
    public loadingService : LoadingService,
    ){}

  ngOnInit(): void {
    this.apiManager.exeApiRequest(this.service.fetchSingle(this.id), () => {console.log(this.apiManager.data())});
  }

}
