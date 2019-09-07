import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RambleComponent } from './ramble.component';
import { WallComponent } from './pages/wall/wall.component';


const routes: Routes = [
  {
    path: '',
    component: RambleComponent,
    children: [
      { path: '', component: WallComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RambleRoutingModule { }
