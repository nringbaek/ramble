import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ManagementRoutingModule } from './management-routing.module';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ManagementComponent } from './management.component';
import { ManagementServicesModule } from './management-services.module';
import { TopBarComponent } from './components/top-bar/top-bar.component';
import { LeftMenuComponent } from './components/left-menu/left-menu.component';
import { WallsComponent } from './pages/walls/walls.component';
import { EditWallComponent } from './pages/edit-wall/edit-wall.component';
import { RambleSettingsComponent } from './pages/ramble-settings/ramble-settings.component';
import { UserSettingsComponent } from './pages/user-settings/user-settings.component';
import { EditWallEntryComponent } from './pages/edit-wall-entry/edit-wall-entry.component';

@NgModule({
  declarations: [
    ManagementComponent,
    DashboardComponent,
    TopBarComponent,
    LeftMenuComponent,
    WallsComponent,
    EditWallComponent,
    RambleSettingsComponent,
    UserSettingsComponent,
    EditWallEntryComponent
  ],
  imports: [
    SharedModule,
    ManagementRoutingModule,
    ManagementServicesModule
  ]
})
export class ManagementModule {
  constructor(
  ) {
  }
}
