import { Injectable } from "@angular/core";
import { Token } from "../models/token";
import { ITokenStorage } from "../interfaces/storage-service-interface";

@Injectable()
export class SessionStorageService implements ITokenStorage
{
    getToken(): Token {
        return JSON.parse(sessionStorage.getItem("token") ?? "{}");
    }
    setToken(token: Token): void {
        sessionStorage.setItem("token", JSON.stringify(token));
    }
    clearToken(): void {
        sessionStorage.removeItem("token");
    }
    
}