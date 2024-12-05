import { Observable } from "rxjs";
import { ApiSuccessResponse } from "../types/api-success-response.type";

export interface ICrudService<TGet, TPost, TPut>{
    create(data : TPost): Observable<TGet>
    fetchAll() : Observable<TGet[]>
    delete(id : string) : Observable<ApiSuccessResponse>
}