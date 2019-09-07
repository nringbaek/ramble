import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

interface Configuration {
  showFrontpage: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {
  configuration: Configuration;

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchConfiguration(): Observable<Configuration> {
    return of({
      showFrontpage: true
    } as Configuration);

    // return this.httpClient.get<Configuration>('/api/v1/configuration');
  }
}
