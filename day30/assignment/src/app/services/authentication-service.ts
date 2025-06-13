import { Injectable } from "@angular/core";
import { LoginRequestModel } from "../models/login-request-model";
import { UserModel } from "../models/user-model";
import { Token } from "../models/token";

@Injectable()
export class AuthenticationService{
    loginData:UserModel[] = [
        {username: "varun@gmail.com", password: "varun123", role: "Student"},
        {username: "ram@gmail.com", password: "ram123", role: "Teacher"}
    ];

    public ValidateUser(request: LoginRequestModel)
    {
        let token:Token|null = null;
        for(let i=0; i<this.loginData.length;i++)
        {
            if (this.loginData[i].username == request.username && this.loginData[i].password == request.password)
            {
                token = {username: this.loginData[i].username, role: this.loginData[i].role}
            }
        }
        
        return token;
    }
}