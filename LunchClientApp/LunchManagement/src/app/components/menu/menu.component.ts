import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
})
export class MenuComponent {
  public meals :Meal[];
  private base = 'https://localhost:44334/';
  
  constructor(http: HttpClient) {
    http.get<Meal[]>(this.base + 'api/meals').subscribe(result => {
      this.meals = result;
    });
  }
}

interface Meal  {
  MealName: string;
  Price: number;
}