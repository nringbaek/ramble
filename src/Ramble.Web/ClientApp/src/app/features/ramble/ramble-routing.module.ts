import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RambleComponent } from './ramble.component';
import { WallComponent } from './pages/wall/wall.component';
import { WallOverviewComponent } from './pages/wall-overview/wall-overview.component';


const routes: Routes = [
  {
    path: '',
    component: RambleComponent,
    children: [
      { path: '', component: WallOverviewComponent },
      { path: ':wid', component: WallComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RambleRoutingModule { }
