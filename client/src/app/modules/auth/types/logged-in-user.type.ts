import { BaseAuthType } from "./base-auth.type";

export interface LoggedInUserType extends BaseAuthType{
    id : string,
    token : string,
    role : string
}