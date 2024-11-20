import { LoginType } from "./login.type";

export interface RegisterType extends LoginType{
    confirmPassword : string;
}