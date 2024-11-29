import { Observable } from "rxjs";

export interface ICrudService<TGet, TPost, TPut>{
    fetchAll() : Observable<TGet[]>
}