import { BaseService } from "./base-service";
import { environment } from "src/environments/environment.prod";
import { BehaviorSubject, Observable } from "rxjs/Rx";
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { UserRegistration } from "../models/user-registration";
import { Injectable } from "@angular/core";

@Injectable()
export class UserService extends BaseService {

    private baseUrl: string = environment.apiEndPoint

    private authNavStatusSource = new BehaviorSubject<boolean>(false);

    private authNavStatus = this.authNavStatusSource.asObservable();

    private loggedIn = false;

    constructor(private http: HttpClient){
        super();

        this.loggedIn = !!localStorage.getItem('auth_token');

        this.authNavStatusSource.next(this.loggedIn);
    }

    public register(email: string, password: string, firstName: string, lastName: string, userName: string): Observable<UserRegistration> {
        let body = JSON.stringify({ email, password, firstName, lastName,location });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let registerData: UserRegistration = {
            email: email,
            password: password,
            firstName: firstName,
            lastName: lastName,
            userName: userName
        };
        return this.http.post<UserRegistration>(this.baseUrl + 'accounts/register', registerData)
    }

    // private login(userName, password) {
    //     let headers = new Headers();
    //     headers.append('Content-Type', 'application/json');
    
    //     return this.http
    //       .post(
    //       this.baseUrl + '/auth/login',
    //       JSON.stringify({ userName, password }),{ headers }
    //       )
    //       .map(res => res.json())
    //       .map(res => {
    //         localStorage.setItem('auth_token', res.auth_token);
    //         this.loggedIn = true;
    //         this.authNavStatusSource.next(true);
    //         return true;
    //       })
    //       .catch(this.handleError);
    //   }

      public logout() {
        localStorage.removeItem('auth_token');
        this.loggedIn = false;
        this.authNavStatusSource.next(false);
      }

      public isLoggedIn() {
        return this.loggedIn;
      }

}
