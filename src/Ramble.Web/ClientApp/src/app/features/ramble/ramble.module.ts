import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { RambleRoutingModule } from './ramble-routing.module';

import { RambleComponent } from './ramble.component';
import { ImageComponent } from './components/image/image.component';
import { TextComponent } from './components/text/text.component';
import { VideoComponent } from './components/video/video.component';
import { WallComponent } from './pages/wall/wall.component';
import { WallOverviewComponent } from './pages/wall-overview/wall-overview.component';

@NgModule({
  declarations: [
    RambleComponent,
    ImageComponent,
    TextComponent,
    VideoComponent,
    WallComponent,
    WallOverviewComponent],
  imports: [
    SharedModule,
    RambleRoutingModule
  ]
})
export class RambleModule { }
