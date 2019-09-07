import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ManagementRoutingModule } from './management-routing.module';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ViewRamblesComponent } from './pages/view-rambles/view-rambles.component';
import { ManagementComponent } from './management.component';
import { LoginComponent } from './pages/login/login.component';

@NgModule({
  declarations: [
    ManagementComponent,
    DashboardComponent,
    ViewRamblesComponent,
    LoginComponent],
  imports: [
    SharedModule,
    ManagementRoutingModule
  ]
})
export class ManagementModule { }
