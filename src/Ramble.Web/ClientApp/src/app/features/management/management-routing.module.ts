import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManagementComponent } from './management.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ViewRamblesComponent } from './pages/view-rambles/view-rambles.component';
import { LoginComponent } from './pages/anonymous/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AnonymousLayoutComponent } from './pages/anonymous/anonymous-layout/anonymous-layout.component';
import { ForgotPasswordComponent } from './pages/anonymous/forgot-password/forgot-password.component';


const routes: Routes = [
  {
    path: 'anonymous',
    component: AnonymousLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
    ]
  },
  {
    path: '',
    component: ManagementComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', pathMatch: 'full', component: DashboardComponent },
      { path: 'rambles', component: ViewRamblesComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagementRoutingModule { }
