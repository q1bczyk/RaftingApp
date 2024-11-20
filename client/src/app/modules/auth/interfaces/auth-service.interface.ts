import { Observable } from "rxjs";
import { RegisterType } from "../types/register.type";
import { ApiSuccessResponse } from "../../core/types/api-success-response.type";
import { LoginType } from "../types/login.type";
import { LoggedInUserType } from "../types/logged-in-user.type";
import { ConfirmAccountType } from "../types/confirm-account.type";
import { BaseAuthType } from "../types/base-auth.type";
import { NewPasswordType } from "../types/new-password.type";

export interface AuthServiceInterface{
    register(data : RegisterType) : Observable<ApiSuccessResponse>
    login(data : LoginType) : Observable<LoggedInUserType>
    confirmAccount(data : ConfirmAccountType) : Observable<ApiSuccessResponse>
    passwordReset(data : BaseAuthType) : Observable<ApiSuccessResponse>
    resendConfirmationToken(data : BaseAuthType) : Observable<ApiSuccessResponse>
    setNewPassword(data : NewPasswordType) : Observable<ApiSuccessResponse> 
}