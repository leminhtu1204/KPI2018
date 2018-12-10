import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { OrderComponent } from './components/order/order.component';
import { MenuComponent } from './components/menu/menu.component';
import { RegistrationComponent } from './components/account/registration.component/registration.component';
import { LoginComponent } from './components/account/login.component/login.component';
import { AuthGuard } from './auth-guard';
import { UserService } from './shared/services/user-service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    OrderComponent,
    MenuComponent,
    RegistrationComponent,
    LoginComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    HttpModule
  ],
  providers: [AuthGuard, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
