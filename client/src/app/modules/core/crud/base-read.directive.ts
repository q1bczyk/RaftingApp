import { Directive, OnInit } from '@angular/core';
import { CrudService } from '../services/crud.service';
import { ApiManager } from '../api/api-manager';

@Directive({
  standalone: true
})
export class BaseReadDirective<TGet, TService extends CrudService<TGet, any, any>> implements OnInit
{
  constructor(protected service : TService, public apiManager : ApiManager<TGet[]>){ }

  ngOnInit(): void {
    this.apiManager.exeApiRequest(this.service.fetchAll());
  }
}
