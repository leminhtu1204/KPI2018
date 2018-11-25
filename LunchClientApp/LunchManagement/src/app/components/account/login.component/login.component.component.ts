import { Component, OnInit, OnDestroy } from '@angular/core';


@Component({
  selector: 'app-login.component',
  templateUrl: './login.component.component.html',
  styleUrls: ['./login.component.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {



  constructor() { }

  ngOnInit() {
  }


  ngOnDestroy(): void {
    throw new Error("Method not implemented.");
  }
}
