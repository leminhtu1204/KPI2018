import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Credentials } from 'src/app/shared/models/credentials';
import { UserService } from 'src/app/shared/services/user-service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login.component',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [UserService]
})
export class LoginComponent implements OnInit, OnDestroy {
  private subcription: Subscription;

  private brandNew: boolean;
  private erros: string;
  private isRequesting: boolean;
  private submitted: boolean;
  private credentials: Credentials = { userName: '', password: '' };
  private errors: string;
  
  constructor(private userService: UserService, private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.subcription = this.activeRoute.queryParams.subscribe((param: any )=>{
      this.brandNew = param['brandNew'];
      this.credentials.userName = param['userName'];
    });
  }


  ngOnDestroy(): void {
    this.subcription.unsubscribe();
  }

  private login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors='';
    this.credentials.userName = value.userName,
    this.credentials.password = value.password
    if (valid) {
      this.userService.login(this.credentials)
        .finally(() => this.isRequesting = false)
        .subscribe(
        result => {         
          if (result) {
             this.router.navigate(['/']);             
          }
        },
        error => this.errors = error);
    }
  }
}
