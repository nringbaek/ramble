import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ManagementRoutingModule } from './management-routing.module';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ViewRamblesComponent } from './pages/view-rambles/view-rambles.component';
import { ManagementComponent } from './management.component';
import { LoginComponent } from './pages/anonymous/login/login.component';
import { ManagementServicesModule } from './management-services.module';
import { ForgotPasswordComponent } from './pages/anonymous/forgot-password/forgot-password.component';
import { AnonymousLayoutComponent } from './pages/anonymous/anonymous-layout/anonymous-layout.component';

@NgModule({
  declarations: [
    ManagementComponent,
    DashboardComponent,
    ViewRamblesComponent,
    LoginComponent,
    ForgotPasswordComponent,
    AnonymousLayoutComponent
  ],
  imports: [
    SharedModule,
    ManagementRoutingModule,
    ManagementServicesModule
  ]
})
export class ManagementModule { }
