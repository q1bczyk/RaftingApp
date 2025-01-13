import { CreateAccountType } from "./create-account.type";

export interface GetAccountType extends CreateAccountType{
    isAccountActive : boolean,
}