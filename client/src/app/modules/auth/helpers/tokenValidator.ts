import { jwtDecode } from "jwt-decode";

function getTokenExpirationDate(token : string) : Date | null
{
    const decodedToken = jwtDecode(token);
    
    if(decodedToken.exp)
        return new Date(decodedToken.exp * 1000);

    return null;
}

export function isTokenExpired(token : string) : boolean
{
    const expirationDate = getTokenExpirationDate(token);
    return expirationDate === null || expirationDate.valueOf() < new Date().valueOf();
}