import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FrontpageComponent } from './pages/frontpage/frontpage.component';


const routes: Routes = [
  { path: '', component: FrontpageComponent, pathMatch: 'full' },
  { path: 'ramble', loadChildren: () => import('../features/ramble/ramble.module').then(e => e.RambleModule) },
  { path: 'management', loadChildren: () => import('../features/management/management.module').then(e => e.ManagementModule) },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    paramsInheritanceStrategy: 'always'
  })],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
