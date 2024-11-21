export interface ApiErrorResponse{
    error : ApiErrorDetails
}

interface ApiErrorDetails{
    statusCode : number,
    message : string,
    details : string,
}