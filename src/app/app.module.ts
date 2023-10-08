import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { InventoryComponent } from './components/inventory/inventory.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ResetComponent } from './components/reset/reset.component';
import { AddVaccineComponent } from './components/inventory/add-vaccine/add-vaccine.component';
import { EditVaccineComponent } from './components/inventory/edit-vaccine/edit-vaccine.component';
import { DistributionComponent } from './components/distribution/distribution.component';
import { AddCenterComponent } from './components/distribution/add-center/add-center.component';
import { EditCenterComponent } from './components/distribution/edit-center/edit-center.component';
import { ReportsComponent } from './components/reports/reports.component';
import { NgChartsModule } from 'ng2-charts';
import { AboutComponent } from './components/dashboard/about/about.component';
import { ContactComponent } from './components/dashboard/contact/contact.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { BookingslotComponent } from './components/bookingslot/bookingslot.component';
import { EditSlotComponent } from './components/bookingslot/edit-slot/edit-slot.component';
import { AddSlotComponent } from './components/bookingslot/add-slot/add-slot.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    InventoryComponent,
    ResetComponent,
    AddVaccineComponent,
    EditVaccineComponent,
    DistributionComponent,
    AddCenterComponent,
    EditCenterComponent,
    ReportsComponent,
    AboutComponent,
    ContactComponent,
    NavbarComponent,
    BookingslotComponent,
    EditSlotComponent,
    AddSlotComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    NgChartsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule, // required for ngx-toastr
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    NgbModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
