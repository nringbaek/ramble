import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AkitaNgDevtools } from '@datorama/akita-ngdevtools';
import { AkitaNgRouterStoreModule } from '@datorama/akita-ng-router-store';
import { environment } from '../environments/environment';
import { MarkdownModule } from 'ngx-markdown';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CoreModule.forRoot(),
    MarkdownModule.forRoot(),
    environment.production ? [] : [ AkitaNgDevtools.forRoot(), AkitaNgRouterStoreModule ],
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
