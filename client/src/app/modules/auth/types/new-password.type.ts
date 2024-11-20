import { ConfirmAccountType } from "./confirm-account.type";

export interface NewPasswordType extends ConfirmAccountType{
    password : string,
    confirmPassword : string
}