import { Component } from '@angular/core';
import { PageWrapperComponent } from "../../components/page-wrapper/page-wrapper.component";
import { BaseReadDirective } from '../../../core/crud/base-read.directive';
import { GetAccountType } from '../../../shared/types/api/account-types/get-account.type';
import { AccountService } from '../../../shared/services/api/account.service';
import { ModalFormService } from '../../services/ui/modal-form.service';
import { ApiManager } from '../../../core/api/api-manager';
import { ConfirmationModalService } from '../../../shared/services/confiramtion-modal.service';
import { ApiSuccessResponse } from '../../../core/types/api-success-response.type';
import { ToastService } from '../../../shared/services/ui/toasts/toast.service';
import { LoadingService } from '../../../shared/services/loading.service';
import { AccountItemComponent } from "./components/account-item/account-item.component";
import { AddAccountComponent } from "./components/add-account/add-account.component";
import { FormModalComponent } from '../../components/form-modal/form-modal.component';

@Component({
  selector: 'app-accounts-page',
  standalone: true,
  imports: [PageWrapperComponent, AccountItemComponent, AddAccountComponent, FormModalComponent],
  templateUrl: './accounts-page.component.html',
  styleUrl: './accounts-page.component.scss'
})
export class AccountsPageComponent extends BaseReadDirective<GetAccountType, AccountService>{
  constructor(
    modalFormSerivce : ModalFormService,
    service : AccountService, 
    apiManager : ApiManager<GetAccountType[]>, 
    confirmationModalService : ConfirmationModalService,
    protected apiDeletemanager : ApiManager<ApiSuccessResponse>,
    toastService : ToastService,
    loadingService : LoadingService){
    super(modalFormSerivce, service, apiManager, confirmationModalService, apiDeletemanager, toastService, loadingService);
  }

  deleteData(email : string) : void{
    this.confirmationModalService.openModal(() => this.deleteAccount(email));
  }

  private deleteAccount(email : string) : void{
    this.service.delete(email).pipe(
    ).subscribe(res => {
        this.toastService.showToast("Pomyślnie usunięto konto", 'success');
        this.apiManager.exeApiRequest(this.service.fetchAll());
    });
  }

  onSuccessAdded() : void{
    this.apiManager.exeApiRequest(this.service.fetchAll())
  }
}
