import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public menus :any[];
  
  constructor(http: HttpClient) {
    http.get<any[]>(environment.apiEndPoint + 'menus').subscribe(result => {
      this.menus = result;
    });
  }
}