import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { OrderComponent } from './components/order/order.component';
import { MenuComponent } from './components/menu/menu.component';
import { RegistrationComponent } from './components/account/registration.component/registration.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'order', component: OrderComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'register', component: RegistrationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
