import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public menus :any[];
  private base = 'http://comchui.azurewebsites.net/api/';
  
  constructor(http: HttpClient) {
    http.get<any[]>(this.base + 'menus').subscribe(result => {
      this.menus = result;
    });
  }
}