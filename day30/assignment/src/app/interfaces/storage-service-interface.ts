import { Token } from "../models/token";

export interface ITokenStorage
{
    
    getToken() : Token;
    setToken(token:Token) : void;
    clearToken() : void;
};