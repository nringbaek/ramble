import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManagementComponent } from './management.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ViewRamblesComponent } from './pages/view-rambles/view-rambles.component';
import { LoginComponent } from './pages/login/login.component';


const routes: Routes = [
  {
    path: '',
    component: ManagementComponent,
    children: [
      { path: '', pathMatch: 'full', component: DashboardComponent },
      { path: 'rambles', component: ViewRamblesComponent },
      { path: 'login', component: LoginComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagementRoutingModule { }
