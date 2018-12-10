import { Component, Inject, DoCheck, IterableDiffer, IterableDiffers } from '@angular/core';

@Component({
  selector: 'app-order',
  styleUrls: ['./order.component.css'],
  templateUrl: './order.component.html'
})
export class OrderComponent implements DoCheck {
  private submeals: any[] = [];
  private total: any;
  private mainDish: Meal;
  private differ: any;
  constructor(differs: IterableDiffers) {
    this.mainDish = {price: 24000, mealName: '', isPrimary: true};
    this.submeals.push(this.mainDish);
  }

  ngDoCheck() {
    this.getTotal();
  }

  public addSubMeal() {
    this.submeals.push({mealName: '', price: 8000, isPrimary: false});
  }

  public removeSubMeal(item: any) {
    this.submeals = this.submeals.filter (x => x !== item);
  }

  public getTotal() {
    this.total = 0;
    if (this.submeals.length === 0) {
      return;
    }
    this.submeals.forEach(element => {
      this.total += element.price;
    });
  }

  public submitOrder() {
    
  }
}

interface Meal {
  mealName: string;
  price: number;
  isPrimary: boolean;
}
