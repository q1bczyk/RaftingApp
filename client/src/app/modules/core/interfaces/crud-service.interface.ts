import { Observable } from "rxjs";
import { ApiSuccessResponse } from "../types/api-success-response.type";

export interface ICrudService<TGet, TPost, TPut>{
    fetchAll() : Observable<TGet[]>
    delete(id : string) : Observable<ApiSuccessResponse>
}