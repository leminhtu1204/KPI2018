import { BaseService } from "./base-service";
import { environment } from "src/environments/environment.prod";
import { BehaviorSubject, Observable } from "rxjs/Rx";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { UserRegistration } from "../models/user-registration";
import { Injectable } from "@angular/core";
import { Credentials } from "../models/credentials";

@Injectable()
export class UserService extends BaseService {

    private baseUrl: string = environment.apiEndPoint

    public authNavStatusSource = new BehaviorSubject<boolean>(false);

    public authNavStatus = this.authNavStatusSource.asObservable();

    private loggedIn = false;

    constructor(private http: Http){
        super();

        this.loggedIn = !!localStorage.getItem('auth_token');

        this.authNavStatusSource.next(this.loggedIn);
    }

    public register(email: string, password: string, firstName: string, lastName: string,userName: string): Observable<boolean> {
        let registerData: UserRegistration = {
            email: email,
            password: password,
            firstName: firstName,
            lastName: lastName,
            userName: userName
        };
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
    
        return this.http.post(this.baseUrl + "/accounts/register", registerData, options)
          .map(res => true)
          .catch(this.handleError);
      }  

    public login(credentials: Credentials) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
    
        return this.http
          .post(this.baseUrl + 'accounts/login', credentials)
          .map(res => res.json())
          .map(res => {
            localStorage.setItem('auth_token', res.value.token);
            this.loggedIn = true;
            this.authNavStatusSource.next(true);
            return true;
          })
          .catch(this.handleError);
      }
      public logout() {
        localStorage.removeItem('auth_token');
        this.loggedIn = false;
        this.authNavStatusSource.next(false);
      }
    
      public isLoggedIn() {
        return this.loggedIn;
      }
}
