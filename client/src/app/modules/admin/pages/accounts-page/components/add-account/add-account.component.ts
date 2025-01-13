import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ModalFormService } from '../../../../services/ui/modal-form.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { GetAccountType } from '../../../../../shared/types/api/account-types/get-account.type';
import { AccountService } from '../../../../../shared/services/api/account.service';
import { mapFormToModel } from '../../../../../core/utils/mapper/mapper';
import { CreateAccountType } from '../../../../../shared/types/api/account-types/create-account.type';
import { LoadingService } from '../../../../../shared/services/loading.service';

@Component({
  selector: 'app-add-account',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, InputTextModule],
  templateUrl: './add-account.component.html',
  styleUrl: './add-account.component.scss'
})
export class AddAccountComponent {
  @Output() addAccountEvent : EventEmitter<GetAccountType> = new EventEmitter<GetAccountType>(); 

  addAccountForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    isAdmin: new FormControl(false),
  });

  constructor(public modalService : ModalFormService, private apiManager : ApiManager<GetAccountType>, private service : AccountService, public loadingService: LoadingService) {}

  onSubmit(): void {
    if (!this.addAccountForm.valid) return;
    const mappedData : CreateAccountType = mapFormToModel(this.addAccountForm);
    this.loadingService.loadingOn();
    this.apiManager.exeApiRequest(this.service.create(mappedData), () => this.onSuccess(mappedData))
  }

  private onSuccess(mappedData : CreateAccountType) : void{
    const createdAccount : GetAccountType = {
      email : mappedData.email,
      isAdmin : mappedData.isAdmin,
      isAccountActive : false
    }
    this.loadingService.loadingOff();
    this.modalService.closeModal();
    this.addAccountEvent.emit(createdAccount);
  }

}
