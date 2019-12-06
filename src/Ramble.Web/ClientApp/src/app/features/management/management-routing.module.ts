import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManagementComponent } from './management.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { WallsComponent } from './pages/walls/walls.component';
import { EditWallComponent } from './pages/edit-wall/edit-wall.component';
import { EditWallEntryComponent } from './pages/edit-wall-entry/edit-wall-entry.component';

const routes: Routes = [
  {
    path: '',
    component: ManagementComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', pathMatch: 'full', component: DashboardComponent },
      { path: 'walls', component: WallsComponent },
      { path: 'walls/:wid', component: EditWallComponent },
      { path: 'walls/:wid/entries/create', component: EditWallEntryComponent },
      { path: 'walls/:wid/entries/:eid', component: EditWallEntryComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagementRoutingModule { }
