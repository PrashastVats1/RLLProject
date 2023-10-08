import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { InventoryComponent } from './components/inventory/inventory.component';
import { ResetComponent } from './components/reset/reset.component';
import { AddVaccineComponent } from './components/inventory/add-vaccine/add-vaccine.component';
import { EditVaccineComponent } from './components/inventory/edit-vaccine/edit-vaccine.component';
import { DistributionComponent } from './components/distribution/distribution.component';
import { AddCenterComponent } from './components/distribution/add-center/add-center.component';
import { EditCenterComponent } from './components/distribution/edit-center/edit-center.component';
import { ReportsComponent } from './components/reports/reports.component';
import { AboutComponent } from './components/dashboard/about/about.component';
import { ContactComponent } from './components/dashboard/contact/contact.component';
import { BookingslotComponent } from './components/bookingslot/bookingslot.component';
import { AddSlotComponent } from './components/bookingslot/add-slot/add-slot.component';
import { EditSlotComponent } from './components/bookingslot/edit-slot/edit-slot.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
  },
  { path: 'reset', component: ResetComponent },
  { path: 'inventory', component: InventoryComponent },
  { path: 'inventory/add-vaccine', component: AddVaccineComponent },
  { path: 'inventory/edit-vaccine/:id', component: EditVaccineComponent },
  { path: 'distribution', component: DistributionComponent },
  { path: 'distribution/add-center', component: AddCenterComponent },
  { path: 'distribution/edit-center/:id', component: EditCenterComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'dashboard/about', component: AboutComponent },
  { path: 'dashboard/contact', component: ContactComponent },
  { path: 'bookingslot', component: BookingslotComponent },
  { path: 'bookingslot/add-slot', component: AddSlotComponent },
  { path: 'bookingslot/edit-slot/:id', component: EditSlotComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
