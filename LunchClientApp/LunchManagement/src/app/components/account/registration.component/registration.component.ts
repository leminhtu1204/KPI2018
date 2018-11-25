import { Component, OnInit } from '@angular/core';
import { UserRegistration } from 'src/app/shared/models/user-registration';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user-service';

@Component({
  selector: 'app-registration.component',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [UserService]
})
export class RegistrationComponent implements OnInit {
  private errors: string;  
  private isRequesting: boolean;
  private submitted: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
  }

  public registerUser({ value, valid }: { value: UserRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors='';
    if(valid)
    {
        this.userService.register(value.email,value.password,value.firstName,value.lastName,value.userName)
                  .subscribe(
                    result  => {if(result){
                        this.router.navigate(['/']);                         
                    }},
                    errors =>  this.errors = errors);
    }
  }      
}
