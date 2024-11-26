import { Injectable } from "@angular/core";
import { BaseApiService } from "../../core/services/base-api.service";
import { HttpClient } from "@angular/common/http";
import { AuthServiceInterface } from "../interfaces/auth-service.interface";
import { Observable, map } from "rxjs";
import { ApiSuccessResponse } from "../../core/types/api-success-response.type";
import { BaseAuthType } from "../types/base-auth.type";
import { ConfirmAccountType } from "../types/confirm-account.type";
import { LoggedInUserType } from "../types/logged-in-user.type";
import { LoginType } from "../types/login.type";
import { NewPasswordType } from "../types/new-password.type";
import { RegisterType } from "../types/register.type";

@Injectable({
    providedIn: 'root'
})

export class AuthService extends BaseApiService implements AuthServiceInterface
{
    constructor(http : HttpClient)
    {
        super(http, 'auth');
    }

    register(data: RegisterType): Observable<ApiSuccessResponse> {
        throw new Error("Method not implemented.");
    }
    login(data: LoginType): Observable<LoggedInUserType> {
        return this.http.post<LoggedInUserType>(`${this.url}/login`, data)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
    
    confirmAccount(data: ConfirmAccountType): Observable<ApiSuccessResponse> {
        throw new Error("Method not implemented.");
    }

    passwordReset(data: BaseAuthType): Observable<ApiSuccessResponse> {
        return this.http.post<ApiSuccessResponse>(`${this.url}/passwordReset`, data)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
    resendConfirmationToken(data: BaseAuthType): Observable<ApiSuccessResponse> {
        throw new Error("Method not implemented.");
    }
    setNewPassword(data: NewPasswordType): Observable<ApiSuccessResponse> {
        return this.http.post<ApiSuccessResponse>(`${this.url}/SetNewPassword`, data)
        .pipe(
            map(res => {
                return res;
            })
        )
    }
}
