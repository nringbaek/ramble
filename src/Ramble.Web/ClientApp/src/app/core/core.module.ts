import { NgModule, Optional, SkipSelf, ModuleWithProviders, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedModule } from '../shared/shared.module';
import { CoreRoutingModule } from './core-routing.module';
import { FrontpageComponent } from './pages/frontpage/frontpage.component';
import { ConfigurationService } from './services/configuration.service';
import { GraphQLModule } from './graphql.module';

export function initializeApp(configurationService: ConfigurationService) {
  return async (): Promise<any> => {
    configurationService.fetchConfiguration();
  };
}

@NgModule({
  declarations: [
    FrontpageComponent
  ],
  imports: [
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreRoutingModule,
    GraphQLModule,
  ],
  exports: [
    SharedModule,
    CoreRoutingModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: initializeApp, deps: [ConfigurationService], multi: true}
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: CoreModule,
      providers: [
      ]
    };
  }
}
