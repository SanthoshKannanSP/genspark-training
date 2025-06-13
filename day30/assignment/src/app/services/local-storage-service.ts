import { Injectable } from "@angular/core";
import { Token } from "../models/token";
import { ITokenStorage } from "../interfaces/storage-service-interface";

@Injectable()
export class LocalStorageService implements ITokenStorage
{
    getToken(): Token {
        return JSON.parse(localStorage.getItem("token") ?? "{}");
    }
    setToken(token: Token): void {
        localStorage.setItem("token", JSON.stringify(token));
    }
    clearToken(): void {
        localStorage.removeItem("token");
    }
    
}