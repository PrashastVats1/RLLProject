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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
